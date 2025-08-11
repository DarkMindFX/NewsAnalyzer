
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DMFX.NewsAnalysis.Services.Dal;
using DMFX.NewsAnalysis.Utils.Convertors;
using System.Net;
using DMFX.NewsAnalysis.Functions.Common;

namespace DMFX.NewsAnalysis.Functions.Analyzer.V1
{
    public class GetDetails : FunctionBase
    {
        private readonly IAnalyzerDal _dalAnalyzer;

        public GetDetails(IHttpContextAccessor httpContextAccessor, IAnalyzerDal dalAnalyzer) : base(httpContextAccessor)
        {
            _dalAnalyzer = dalAnalyzer;
        }

        [Authorize]    
        [FunctionName("AnalyzersGetDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/analyzers/{id}")] HttpRequest req,
            System.Int64? id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var e = _dalAnalyzer.Get(id);
                if (e != null)
                {
                    var dtos = AnalyzerConvertor.Convert(e, null);

                    result = new OkObjectResult(funHelper.ToJosn(dtos));
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"Analyzer was found, but item was not deleted [ids:{id}]"
                    }))
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