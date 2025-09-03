

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
    public class ArticlesController : BaseController
    {
        private readonly IArticleDal _dalArticle;
        private readonly ILogger<ArticlesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public ArticlesController( IArticleDal dalArticle,
                                    ILogger<ArticlesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalArticle = dalArticle; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticle.GetAll();

            IList<DTO.Article> dtos = new List<DTO.Article>();

            foreach (var p in entities)
            {
                var dto = ArticleConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetArticle")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalArticle.Get(id);
            if (entity != null)
            {
                var dto = ArticleConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Article was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bynewssourceid/{newssourceid}")]
        public IActionResult GetByNewsSourceID(System.Int64 newssourceid)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticle.GetByNewsSourceID(newssourceid);

            IList<DTO.Article> dtos = new List<DTO.Article>();

            foreach (var p in entities)
            {
                var dto = ArticleConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("bydates/{datestart}/{dateend}")]
        public IActionResult GetByDates(DateTime datestart, DateTime dateend)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalArticle.GetAll().Where( a => a.NewsTime >= datestart && a.NewsTime <= dateend);

            IList<DTO.Article> dtos = new List<DTO.Article>();

            foreach (var p in entities)
            {
                var dto = ArticleConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteArticle")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalArticle.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalArticle.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Article [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Article not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertArticle")]
        public IActionResult Insert(DTO.Article dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = ArticleConvertor.Convert(dto);           

            
            Article newEntity = _dalArticle.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, ArticleConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateArticle")]
        public IActionResult Update(DTO.Article dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = ArticleConvertor.Convert(dto);

            var existingEntity = _dalArticle.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Article entity = _dalArticle.Update(newEntity);

                response = Ok(ArticleConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Article not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

