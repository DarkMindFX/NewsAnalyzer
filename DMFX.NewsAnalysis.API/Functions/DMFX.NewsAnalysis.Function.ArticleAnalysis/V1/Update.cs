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

namespace DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1
{
    public class Update : FunctionBase
    {
        private readonly IArticleAnalysisDal _dalArticleAnalysis;

        public Update(IHttpContextAccessor httpContextAccessor,
            IArticleAnalysisDal dalArticleAnalysis) : base(httpContextAccessor)
        {
            _dalArticleAnalysis = dalArticleAnalysis;
        }

        [Authorize]
        [FunctionName("ArticleAnalysisesUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/articleanalysises")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<DMFX.NewsAnalysis.DTO.ArticleAnalysis>(content);

                var newEntity = ArticleAnalysisConvertor.Convert(dto);

                var existingEntity = _dalArticleAnalysis.Get(        newEntity.ID );

                if (existingEntity != null)
                {
                    										                   
                    DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis entity = _dalArticleAnalysis.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(ArticleAnalysisConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"ArticleAnalysis was not found [ids:{newEntity.ID }]"}
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
