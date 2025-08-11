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
using System.Net;
using DMFX.NewsAnalysis.Services.Dal;
using DMFX.NewsAnalysis.Services.Common.Helpers;
using DMFX.NewsAnalysis.Functions.Common;

namespace DMFX.NewsAnalysis.Functions.Analyzer.V1
{
    public class Update : FunctionBase
    {
        private readonly IAnalyzerDal _dalAnalyzer;

        public Update(IHttpContextAccessor httpContextAccessor,
            IAnalyzerDal dalAnalyzer) : base(httpContextAccessor)
        {
            _dalAnalyzer = dalAnalyzer;
        }

        [Authorize]
        [FunctionName("AnalyzersUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/analyzers")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<DMFX.NewsAnalysis.DTO.Analyzer>(content);

                var newEntity = AnalyzerConvertor.Convert(dto);

                var existingEntity = _dalAnalyzer.Get(        newEntity.ID );

                if (existingEntity != null)
                {
                    										                   
                    DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity = _dalAnalyzer.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(AnalyzerConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"Analyzer was not found [ids:{newEntity.ID }]"}
                    ))
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
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
