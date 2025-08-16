using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using DMFX.NewsAnalysis.Functions.Common;
using DMFX.NewsAnalysis.Functions.DTO;
using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.Parser.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Text.Json;
using static Azure.Core.HttpHeader;

namespace DMFX.NewsAnalysis.API.SourcesCrawler;

public class StartCrawlerFn
{
    const string INPUT_QEUEUE = "dmfx-newsanalysis-crawler";

    private readonly ILogger<StartCrawlerFn> _logger;
    private readonly IArticleDal _articlaDal;
    private readonly ExportProvider _exortProvder;

    private CloudStorageAccount _cloudStorageAccount;
    private CloudQueue _cloudQueue;


    public StartCrawlerFn(ILogger<StartCrawlerFn> logger,
                            IArticleDal newsSourceDal,
                            ExportProvider exortProvder)
    {
        _logger = logger;
        _articlaDal = newsSourceDal;
        _exortProvder = exortProvder;
    }

    [Function(nameof(StartCrawlerFn))]
    //[QueueOutput("dmfx-newsanalysis-articles-new", Connection = "AzureWebJobsStorage")]
    public void Run([QueueTrigger(INPUT_QEUEUE, Connection = "AzureWebJobsStorage")] string message)
    {

        _logger.LogInformation($"Start processing message:\r\n {message}");

        var request = JsonSerializer.Deserialize<StartCrawlerDto>(message);

        ValidateRequest(request);

        if(_cloudStorageAccount == null)
        {
            var funHelper = new FunctionHelper();
            _cloudStorageAccount = GetStorageAccount(funHelper.GetEnvironmentVariable<string>("ServiceConfig__StorageInitParams__StorageConnectionString"));
            if(_cloudStorageAccount != null)
            {
                _cloudQueue = PrepareQueueClient(_cloudStorageAccount,
                                                funHelper.GetEnvironmentVariable<string>("ServiceConfig__OutputQueue"));
            }
        }

        Crawl(request);

    }

    private void Crawl(StartCrawlerDto request)
    {
        var crawler = _exortProvder.GetExportedValue<ISourceCrawler>(request.Source);
        var paginator = _exortProvder.GetExportedValue<ISourcePaginator>(request.Source);

        if (crawler != null && paginator != null)
        {
            crawler.OnArticleAvailable += OnArticleAvailable;

            crawler.StartCrawling(new SourceCrawlerParams()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Paginator = paginator
            });
        }
        else
        {
            if (crawler == null) throw new Exception($"Crawler not found for source '{request.Source}'");
            if (paginator == null) throw new Exception($"Paginator not found for source '{request.Source}'");
        }
    }

    private void OnArticleAvailable(object sender, ArticleDetails e)
    {
        _logger.LogInformation($"{e.Source}: Artifcle found - [{e.PublishedDate}] {e.URL}");
        if(_cloudQueue != null)
        {
            var message = new NewArticleDto()
            {
                Header = new MessageHeader()
                {
                    Timestamp = DateTime.UtcNow,
                    Sender = nameof(StartCrawlerFn)
                },
                Source = e.Source,
                URL = e.URL,
            };

            _cloudQueue.AddMessageAsync(
                new CloudQueueMessage(
                    JsonSerializer.Serialize(message)
                   )
            );
        }
    }

    
    private CloudStorageAccount GetStorageAccount(string storageConnectionString)
    {
        CloudStorageAccount storageAccount;
        try
        {
            storageAccount = CloudStorageAccount.Parse(storageConnectionString);
        }
        catch (FormatException)
        {
            _logger.LogError("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
              throw;
        }
        catch (ArgumentException)
        {
            _logger.LogError("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            throw;
        }

        return storageAccount;
    }

    private CloudQueue PrepareQueueClient(CloudStorageAccount storageAccount, string queueName)
    {
        var queueClient = storageAccount.CreateCloudQueueClient();
        var queue = queueClient.GetQueueReference(queueName);

        queue.CreateIfNotExistsAsync();

        return queue;
    }

    private void ValidateRequest(StartCrawlerDto request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");
        }

        if (request.Header == null)
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: Header cannot be null.");
        }

        if (string.IsNullOrEmpty(request.Source))
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: Source is null or empty.");
        }

    }

}