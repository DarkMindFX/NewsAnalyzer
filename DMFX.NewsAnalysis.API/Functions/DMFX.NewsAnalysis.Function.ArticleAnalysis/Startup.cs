using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DMFX.NewsAnalysis.Functions.Common;

[assembly: FunctionsStartup(typeof(DMFX.NewsAnalysis.Functions.ArticleAnalysis.Startup))]
namespace DMFX.NewsAnalysis.Functions.ArticleAnalysis
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalArticleAnalysisDal = InitDal<Interfaces.IArticleAnalysisDal>();
            builder.Services.AddSingleton<Interfaces.IArticleAnalysisDal>(dalArticleAnalysisDal);
            builder.Services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IArticleAnalysisDal, DMFX.NewsAnalysis.Services.Dal.ArticleAnalysisDal>();            
        }
    }
}