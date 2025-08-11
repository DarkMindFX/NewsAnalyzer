using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DMFX.NewsAnalysis.Functions.Common;

[assembly: FunctionsStartup(typeof(DMFX.NewsAnalysis.Functions.Article.Startup))]
namespace DMFX.NewsAnalysis.Functions.Article
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalArticleDal = InitDal<Interfaces.IArticleDal>();
            builder.Services.AddSingleton<Interfaces.IArticleDal>(dalArticleDal);
            builder.Services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IArticleDal, DMFX.NewsAnalysis.Services.Dal.ArticleDal>();            
        }
    }
}