using Azure.Storage.Queues;
using DMFX.NewsAnalysis.Functions.Common;
using DMFX.NewsAnalysis.Functions.DTO;
using DMFX.NewsAnalysis.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace DMFX.NewsAnalysis.CrawlerTimer;

public class CrawlersTriggerFn
{
    private readonly ILogger _logger;
    private readonly INewsSourceDal _newsSourceDal;

    public CrawlersTriggerFn(
            INewsSourceDal newsSourceDal,
            ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CrawlersTriggerFn>();
        _newsSourceDal = newsSourceDal;
    }

#if DEBUG
        const bool IS_RUN_ON_STARTUP = true;
#else
        const bool IS_RUN_ON_STARTUP = false;
#endif

    [Function(nameof(CrawlersTriggerFn))]
    public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = IS_RUN_ON_STARTUP)] TimerInfo myTimer)
    {
        _logger.LogInformation($"Starting sending extraction messages");

        var monitoredSources = _newsSourceDal.GetAll().Where(ns => ns.IsActive);

        _logger.LogInformation($"Found monitored sources: {monitoredSources.Count()}");

        var funHelper = new FunctionHelper();

        var dtoRequest = PrepareRequestDto(funHelper);
        dtoRequest.SkipExisting = funHelper.GetEnvironmentVariable<bool>(Constants.ENV_CRAWLER_SKIP_EXISTING);
        dtoRequest.EndDate = null;
        dtoRequest.StartDate = DateTime.UtcNow - TimeSpan.FromDays(funHelper.GetEnvironmentVariable<int>(Constants.ENV_CRAWLER_TIME_LOOKBACK_MINS));

        var queueClient = PrepareQueueClient(funHelper);

        foreach (var s in monitoredSources)
        {
            dtoRequest.Source = s.Name;

            // put message to queue
            SendExtractRequest(queueClient, dtoRequest);
        }

        _logger.LogInformation($"Done - all requests sent.");
    }

    #region Support methods

    private StartCrawlerDto PrepareRequestDto(FunctionHelper funHelper)
    {
        var dtoRequest = new StartCrawlerDto();
        dtoRequest.Header = new MessageHeader();
        dtoRequest.Header.Sender = nameof(CrawlersTriggerFn);
        dtoRequest.Header.Timestamp = DateTime.UtcNow;

        return dtoRequest;
    }

    private QueueClient PrepareQueueClient(FunctionHelper funHelper)
    {
        string queueName = funHelper.GetEnvironmentVariable<string>(Constants.ENV_QUEUE_EXTRACT_REQUESTS);
        string azStorageConnString = funHelper.GetEnvironmentVariable<string>(Constants.ENV_STORAGE_CONNECTION_STRING);

        var queueClient = new QueueClient(azStorageConnString,
                                            queueName,
                                            new QueueClientOptions
                                            {
                                                MessageEncoding = QueueMessageEncoding.Base64
                                            });

        return queueClient;
    }

    private async Task SendExtractRequest(QueueClient client, StartCrawlerDto dto)
    {
        string message = JsonSerializer.Serialize(dto);
        string msgBase64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(message));

        _logger.LogInformation($"Sending:\r\n{message}\r\n");

        var response = client.SendMessage(message);
    }
    #endregion
}