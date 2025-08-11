using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DMFX.NewsAnalysis.Functions.Common;

[assembly: FunctionsStartup(typeof(DMFX.NewsAnalysis.Functions.SentimentType.Startup))]
namespace DMFX.NewsAnalysis.Functions.SentimentType
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalSentimentTypeDal = InitDal<Interfaces.ISentimentTypeDal>();
            builder.Services.AddSingleton<Interfaces.ISentimentTypeDal>(dalSentimentTypeDal);
            builder.Services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.ISentimentTypeDal, DMFX.NewsAnalysis.Services.Dal.SentimentTypeDal>();            
        }
    }
}