using Azure.Storage.Queues.Models;
using DMFX.NewsAnalysis.Functions.DTO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Text.Json;

namespace DMFX.NewsAnalysis.API.SourcesCrawler;

public class StartCrawlerFn
{
    private readonly ILogger<StartCrawlerFn> _logger;
    const string INPUT_QEUEUE = "dmfx-newsanalysis-crawler";
    const string OUTPUT_QEUEUE = "dmfx-newsanalysis-articles-new";
    private CloudStorageAccount _cloudStorageAccount;

    public StartCrawlerFn(ILogger<StartCrawlerFn> logger)
    {
        _logger = logger;
    }

    [Function(nameof(StartCrawlerFn))]
    //[QueueOutput("dmfx-newsanalysis-articles-new", Connection = "AzureWebJobsStorage")]
    public void Run([QueueTrigger(INPUT_QEUEUE, Connection = "AzureWebJobsStorage")] string message)
    {

        _logger.LogInformation($"Start processing message:\r\n {message}");

        var request = JsonSerializer.Deserialize<StartCrawlerDto>(message);

        ValidateRequest(request);

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