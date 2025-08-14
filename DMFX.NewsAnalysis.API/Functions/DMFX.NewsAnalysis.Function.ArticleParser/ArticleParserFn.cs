using Azure.Core;
using Azure.Storage.Queues.Models;
using DMFX.NewsAnalysis.Functions.DTO;
using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Parser.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Net;
using System.Text.Json;
using static Grpc.Core.Metadata;

namespace DMFX.NewsAnalysis.Function.ArticleParser;

public class ArticleParserFn
{
    const string INPUT_QEUEUE = "dmfx-newsanalysis-articles-new";

    private readonly ILogger<ArticleParserFn> _logger;
    private readonly IArticleDal _articlaDal;
    private readonly INewsSourceDal _newsSourceDal;
    private readonly ExportProvider _exortProvder;

    public ArticleParserFn(ILogger<ArticleParserFn> logger,
                            IArticleDal articleDal,
                            INewsSourceDal newsSourceDal,
                            ExportProvider exortProvder)
    {
        _logger = logger;
        _articlaDal = articleDal;
        _newsSourceDal = newsSourceDal;
        _exortProvder = exortProvder;
    }

    [Function(nameof(ArticleParserFn))]
    [QueueOutput("dmfx-newsanalysis-articles-parsed", Connection = "AzureWebJobsStorage")]
    public ArticleParsedDto Run([QueueTrigger(INPUT_QEUEUE, Connection = "AzureWebJobsStorage")] string message)
    {
        _logger.LogInformation($"Start processing message:\r\n {message}");

        var request = JsonSerializer.Deserialize<NewArticleDto>(message);

        ValidateRequest(request);

        using (var webClient = new WebClient())
        {
            var content = webClient.DownloadString(request.URL);
            if (!string.IsNullOrEmpty(content))
            {
                var article = Parse(content, request.Source);
                var entity = _articlaDal.Insert(article);
                _logger.LogInformation($"Article successfully parsed and saved with ID: {entity.ID}");

                var articleParsedDto = PrepareDto(newEntity: entity);

                return articleParsedDto;
            }
            else
            {
                string errorMessage = $"Failed to load content from {request.URL} - null or empty string returned";
                _logger.LogInformation(errorMessage);
                throw new ApplicationException(errorMessage);
            }
        }


    }

    private ArticleParsedDto PrepareDto(Article newEntity)
    {
        var articleParsedDto = new ArticleParsedDto()
        {
            Header = new MessageHeader()
            {
                Sender = nameof(ArticleParserFn),
                Timestamp = DateTime.UtcNow
            },
            ArticleID = (long)newEntity.ID
        };

        return articleParsedDto;

    }

    private Article Parse(string content, string sourceName)
    {
        var parser = _exortProvder.GetExportedValue<IArticleParser>(sourceName);
        if (parser != null)
        {
            var article = parser.Parse(content);
            var newsSourceID = _newsSourceDal.GetAll()
                .FirstOrDefault(x => x.Name.Equals(sourceName, StringComparison.OrdinalIgnoreCase))?.ID;

            if (newsSourceID != null)
            {
                article.NewsSourceID = newsSourceID.Value;
            }
            else
            {
                throw new ArgumentException($"No news source found for name: {sourceName}");
            }

            return article;
        }

        else
        {
            throw new ArgumentException($"No parser found for source: {sourceName}");
        }
    }

    private void ValidateRequest(NewArticleDto request)
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

        if (string.IsNullOrEmpty(request.URL))
        {
            throw new ArgumentNullException(nameof(request), "Invalid request: URL is null or empty.");
        }

    }
}