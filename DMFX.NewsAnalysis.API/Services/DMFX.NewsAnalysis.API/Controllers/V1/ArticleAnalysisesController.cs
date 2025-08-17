

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DMFX.NewsAnalysis.Services.Common.Filters;
using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Utils.Convertors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using DMFX.NewsAnalysis.API.Helpers;
using DMFX.NewsAnalysis.Services.Dal;


namespace DMFX.NewsAnalysis.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [UnhandledExceptionFilter]
    public class ArticleAnalysisesController : BaseController
    {
        private readonly IArticleAnalysisDal _dalArticleAnalysis;
        private readonly ILogger<ArticleAnalysisesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public ArticleAnalysisesController( IArticleAnalysisDal dalArticleAnalysis,
                                    ILogger<ArticleAnalysisesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalArticleAnalysis = dalArticleAnalysis; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticleAnalysis.GetAll();

            IList<DTO.ArticleAnalysis> dtos = new List<DTO.ArticleAnalysis>();

            foreach (var p in entities)
            {
                var dto = ArticleAnalysisConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetArticleAnalysis")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalArticleAnalysis.Get(id);
            if (entity != null)
            {
                var dto = ArticleAnalysisConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"ArticleAnalysis was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("byarticleid/{articleid}")]
        public IActionResult GetByArticleID(System.Int64 articleid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticleAnalysis.GetByArticleID(articleid);

            IList<DTO.ArticleAnalysis> dtos = new List<DTO.ArticleAnalysis>();

            foreach (var p in entities)
            {
                var dto = ArticleAnalysisConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("bysentimentid/{sentimentid}")]
        public IActionResult GetBySentimentID(System.Int64 sentimentid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticleAnalysis.GetBySentimentID(sentimentid);

            IList<DTO.ArticleAnalysis> dtos = new List<DTO.ArticleAnalysis>();

            foreach (var p in entities)
            {
                var dto = ArticleAnalysisConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        //[Authorize]
        [HttpGet("byanalyzerid/{analyzerid}")]
        public IActionResult GetByAnalyzerID(System.Int64 analyzerid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticleAnalysis.GetByAnalyzerID(analyzerid);

            IList<DTO.ArticleAnalysis> dtos = new List<DTO.ArticleAnalysis>();

            foreach (var p in entities)
            {
                var dto = ArticleAnalysisConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteArticleAnalysis")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalArticleAnalysis.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalArticleAnalysis.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete ArticleAnalysis [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"ArticleAnalysis not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertArticleAnalysis")]
        public IActionResult Insert(DTO.ArticleAnalysis dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ArticleAnalysisConvertor.Convert(dto);           

            
            ArticleAnalysis newEntity = _dalArticleAnalysis.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ArticleAnalysisConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateArticleAnalysis")]
        public IActionResult Update(DTO.ArticleAnalysis dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ArticleAnalysisConvertor.Convert(dto);

            var existingEntity = _dalArticleAnalysis.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    ArticleAnalysis entity = _dalArticleAnalysis.Update(newEntity);

                response = Ok(ArticleAnalysisConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"ArticleAnalysis not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

