using System;
using Azure.Storage.Queues.Models;
using DMFX.NewsAnalysis.Functions.DTO;
using DMFX.NewsAnalysis.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace DMFX.NewsAnalysis.Function.ArticleParser;

public class ArticleParserFn
{
    const string INPUT_QEUEUE = "dmfx-newsanalysis-articles-new";

    private readonly ILogger<ArticleParserFn> _logger;
    private readonly IArticleDal _articlaDal;

    public ArticleParserFn( ILogger<ArticleParserFn> logger,
                            IArticleDal articleDal)
    {
        _logger = logger;
        _articlaDal = articleDal;
    }

    [Function(nameof(ArticleParserFn))]
    public void Run([QueueTrigger(INPUT_QEUEUE, Connection = "AzureWebJobsStorage")] string message)
    {
        _logger.LogInformation($"Start processing message:\r\n {message}");

        var request = JsonSerializer.Deserialize<NewArticleDto>(message);

        ValidateRequest(request);

    }

    private void ValidateRequest(NewArticleDto request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");
        }

        if(request.Header == null)
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: Header cannot be null.");
        }

        if (string.IsNullOrEmpty(request.Source))
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: Source is null or empty.");
        }

        if (string.IsNullOrEmpty(request.URL))
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: URL is null or empty.");
        }

    }
}