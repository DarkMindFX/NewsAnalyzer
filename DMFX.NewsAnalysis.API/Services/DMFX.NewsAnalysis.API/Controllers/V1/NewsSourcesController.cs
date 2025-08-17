

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
    public class NewsSourcesController : BaseController
    {
        private readonly INewsSourceDal _dalNewsSource;
        private readonly ILogger<NewsSourcesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public NewsSourcesController( INewsSourceDal dalNewsSource,
                                    ILogger<NewsSourcesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalNewsSource = dalNewsSource; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalNewsSource.GetAll();

            IList<DTO.NewsSource> dtos = new List<DTO.NewsSource>();

            foreach (var p in entities)
            {
                var dto = NewsSourceConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetNewsSource")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalNewsSource.Get(id);
            if (entity != null)
            {
                var dto = NewsSourceConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"NewsSource was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteNewsSource")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalNewsSource.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalNewsSource.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete NewsSource [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"NewsSource not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertNewsSource")]
        public IActionResult Insert(DTO.NewsSource dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = NewsSourceConvertor.Convert(dto);           

            
            NewsSource newEntity = _dalNewsSource.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, NewsSourceConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateNewsSource")]
        public IActionResult Update(DTO.NewsSource dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = NewsSourceConvertor.Convert(dto);

            var existingEntity = _dalNewsSource.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    NewsSource entity = _dalNewsSource.Update(newEntity);

                response = Ok(NewsSourceConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"NewsSource not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

