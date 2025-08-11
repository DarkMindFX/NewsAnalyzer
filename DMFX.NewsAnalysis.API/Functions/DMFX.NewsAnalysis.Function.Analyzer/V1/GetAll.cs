using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DMFX.NewsAnalysis.Services.Dal;
using System.Collections.Generic;
using DMFX.NewsAnalysis.Utils.Convertors;
using System;
using DMFX.NewsAnalysis.Functions.Common;

namespace DMFX.NewsAnalysis.Functions.Analyzer.V1
{
    public class GetAll : FunctionBase
    {
        private readonly IAnalyzerDal _dalAnalyzer;
        public GetAll(IHttpContextAccessor httpContextAccessor, IAnalyzerDal dalAnalyzer) : base(httpContextAccessor)
        {
            _dalAnalyzer = dalAnalyzer;
        }

        [Authorize]
        [FunctionName("AnalyzersGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/analyzers")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var entities = _dalAnalyzer.GetAll();
                var dtos = new List<DMFX.NewsAnalysis.DTO.Analyzer>();
                foreach (var e in entities)
                {
                    dtos.Add(AnalyzerConvertor.Convert(e, null));
                }

                result = new OkObjectResult(funHelper.ToJosn(dtos));
            }
            catch(Exception ex)
            {
                log.LogError(ex.ToString());
            }

            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Ended");

            return result;
        }
    }
}

