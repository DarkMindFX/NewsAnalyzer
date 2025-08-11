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

namespace DMFX.NewsAnalysis.Functions.NewsSource.V1
{
    public class Update : FunctionBase
    {
        private readonly INewsSourceDal _dalNewsSource;

        public Update(IHttpContextAccessor httpContextAccessor,
            INewsSourceDal dalNewsSource) : base(httpContextAccessor)
        {
            _dalNewsSource = dalNewsSource;
        }

        [Authorize]
        [FunctionName("NewsSourcesUpdate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "v1/newssources")] HttpRequest req,
            ILogger log)
        {
            IActionResult result = null;
            var funHelper = new DMFX.NewsAnalysis.Functions.Common.FunctionHelper();
            log.LogInformation($"{System.Reflection.MethodInfo.GetCurrentMethod()} Started");

            try
            {
                var content = await new StreamReader(req.Body).ReadToEndAsync();

                var dto = JsonConvert.DeserializeObject<DMFX.NewsAnalysis.DTO.NewsSource>(content);

                var newEntity = NewsSourceConvertor.Convert(dto);

                var existingEntity = _dalNewsSource.Get(        newEntity.ID );

                if (existingEntity != null)
                {
                    										                   
                    DMFX.NewsAnalysis.Interfaces.Entities.NewsSource entity = _dalNewsSource.Update(newEntity);

                    result = new ObjectResult(funHelper.ToJosn(NewsSourceConvertor.Convert(newEntity, null)))
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    result = new ObjectResult(funHelper.ToJosn(new DMFX.NewsAnalysis.DTO.Error()
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        Message = $"NewsSource was not found [ids:{newEntity.ID }]"}
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
