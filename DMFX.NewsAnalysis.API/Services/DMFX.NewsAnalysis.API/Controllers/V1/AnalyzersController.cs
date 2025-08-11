

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DMFX.NewsAnalysis.API.Filters;
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
    public class AnalyzersController : BaseController
    {
        private readonly IAnalyzerDal _dalAnalyzer;
        private readonly ILogger<AnalyzersController> _logger;
        private readonly IOptions<AppSettings> _appSettings;


        public AnalyzersController( IAnalyzerDal dalAnalyzer,
                                    ILogger<AnalyzersController> logger,
                                    IOptions<AppSettings> appSettings)
        {
            _dalAnalyzer = dalAnalyzer; 
            _logger = logger;
            _appSettings = appSettings;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");
            IActionResult response = null;

            var entities = _dalAnalyzer.GetAll();

            IList<DTO.Analyzer> dtos = new List<DTO.Analyzer>();

            foreach (var p in entities)
            {
                var dto = AnalyzerConvertor.Convert(p, this.Url);

                dtos.Add(dto);
            }

            response = Ok(dtos);

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpGet("{id}"), ActionName("GetAnalyzer")]
        public IActionResult Get(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = _dalAnalyzer.Get(id);
            if (entity != null)
            {
                var dto = AnalyzerConvertor.Convert(entity, this.Url);
                response = Ok(dto);
            }
            else
            {
                response = StatusCode((int)HttpStatusCode.NotFound, $"Analyzer was not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        
        //[Authorize]
        [HttpDelete("{id}"), ActionName("DeleteAnalyzer")]
        public IActionResult Delete(System.Int64? id)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var existingEntity = _dalAnalyzer.Get(id);

            if (existingEntity != null)
            {
                bool removed = _dalAnalyzer.Delete(id);
                if (removed)
                {
                    response = Ok();
                }
                else
                {
                    response = StatusCode((int)HttpStatusCode.InternalServerError, $"Failed to delete Analyzer [ids:{id}]");
                }
            }
            else
            {
                response = NotFound($"Analyzer not found [ids:{id}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }

        //[Authorize]
        [HttpPost, ActionName("InsertAnalyzer")]
        public IActionResult Insert(DTO.Analyzer dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var entity = AnalyzerConvertor.Convert(dto);           

            
            Analyzer newEntity = _dalAnalyzer.Insert(entity);

            response = StatusCode((int)HttpStatusCode.Created, AnalyzerConvertor.Convert(newEntity, this.Url));

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }


        //[Authorize]
        [HttpPut, ActionName("UpdateAnalyzer")]
        public IActionResult Update(DTO.Analyzer dto)
        {
            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            IActionResult response = null;

            var newEntity = AnalyzerConvertor.Convert(dto);

            var existingEntity = _dalAnalyzer.Get(newEntity.ID);           

            if (existingEntity != null)
            {
                                                    Analyzer entity = _dalAnalyzer.Update(newEntity);

                response = Ok(AnalyzerConvertor.Convert(entity, this.Url));
            }
            else
            {
                response = NotFound($"Analyzer not found [ids:{newEntity.ID}]");
            }

            _logger.LogTrace($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return response;
        }
    }
}

