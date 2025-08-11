
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

namespace DMFX.NewsAnalysis.Functions.SentimentType.V1
{
    public class GetDetails : FunctionBase
    {
        private readonly ISentimentTypeDal _dalSentimentType;

        public GetDetails(IHttpContextAccessor httpContextAccessor, ISentimentTypeDal dalSentimentType) : base(httpContextAccessor)
        {
            _dalSentimentType = dalSentimentType;
        }

        [Authorize]    
        [FunctionName("SentimentTypesGetDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/sentimenttypes/{id}")] HttpRequest req,
            System.Int64? id,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var e = _dalSentimentType.Get(id);
                if (e != null)
                {
                    var dtos = SentimentTypeConvertor.Convert(e, null);

                    result = new OkObjectResult(funHelper.ToJosn(dtos));
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"SentimentType was found, but item was not deleted [ids:{id}]"
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