

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
    public class SentimentTypesController : BaseController
    {
        private readonly ISentimentTypeDal _dalSentimentType;
        private readonly ILogger<SentimentTypesController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public SentimentTypesController( ISentimentTypeDal dalSentimentType,
                                    ILogger<SentimentTypesController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalSentimentType = dalSentimentType; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalSentimentType.GetAll();

            IList<DTO.SentimentType> dtos = new List<DTO.SentimentType>();

            foreach (var p in entities)
            {
                var dto = SentimentTypeConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetSentimentType")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalSentimentType.Get(id);
            if (entity != null)
            {
                var dto = SentimentTypeConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"SentimentType was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteSentimentType")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalSentimentType.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalSentimentType.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete SentimentType [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"SentimentType not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertSentimentType")]
        public IActionResult Insert(DTO.SentimentType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = SentimentTypeConvertor.Convert(dto);           

            
            SentimentType newEntity = _dalSentimentType.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, SentimentTypeConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateSentimentType")]
        public IActionResult Update(DTO.SentimentType dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = SentimentTypeConvertor.Convert(dto);

            var existingEntity = _dalSentimentType.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    SentimentType entity = _dalSentimentType.Update(newEntity);

                response = Ok(SentimentTypeConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"SentimentType not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

