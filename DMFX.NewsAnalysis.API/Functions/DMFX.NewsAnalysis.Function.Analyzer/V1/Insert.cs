using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DMFX.NewsAnalysis.Utils.Convertors;
using DMFX.NewsAnalysis.Services.Common.Helpers;
using DMFX.NewsAnalysis.Services.Dal;
using System.Net;
using DMFX.NewsAnalysis.Functions.Common;

namespace DMFX.NewsAnalysis.Functions.Analyzer.V1
{
    public class Insert : FunctionBase
    {
        private readonly IAnalyzerDal _dalAnalyzer;

        public Insert(IHttpContextAccessor httpContextAccessor, IAnalyzerDal dalAnalyzer) : base(httpContextAccessor)
        {
            _dalAnalyzer = dalAnalyzer;
        }

        [Authorize]
        [FunctionName("AnalyzersInsert")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/analyzers")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<DMFX.NewsAnalysis.DTO.Analyzer>(content);

                var entity = AnalyzerConvertor.Convert(dto);

				
                DMFX.NewsAnalysis.Interfaces.Entities.Analyzer newEntity = _dalAnalyzer.Insert(entity);

                if (newEntity != null)
                {
                    result = new ObjectResult(funHelper.ToJosn(AnalyzerConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.Created
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Message = $"Something went wrong. Analyzer was not inserted."
                    }))
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }

            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return result;
        }
    }
}