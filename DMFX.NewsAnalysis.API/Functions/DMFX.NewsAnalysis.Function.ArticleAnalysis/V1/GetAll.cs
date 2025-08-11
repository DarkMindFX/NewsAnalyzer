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

namespace DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1
{
    public class GetAll : FunctionBase
    {
        private readonly IArticleAnalysisDal _dalArticleAnalysis;
        public GetAll(IHttpContextAccessor httpContextAccessor, IArticleAnalysisDal dalArticleAnalysis) : base(httpContextAccessor)
        {
            _dalArticleAnalysis = dalArticleAnalysis;
        }

        [Authorize]
        [FunctionName("ArticleAnalysisesGetAll")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/articleanalysises")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var entities = _dalArticleAnalysis.GetAll();
                var dtos = new List<DMFX.NewsAnalysis.DTO.ArticleAnalysis>();
                foreach (var e in entities)
                {
                    dtos.Add(ArticleAnalysisConvertor.Convert(e, null));
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

