using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DMFX.NewsAnalysis.Functions.Common;

[assembly: FunctionsStartup(typeof(DMFX.NewsAnalysis.Functions.NewsSource.Startup))]
namespace DMFX.NewsAnalysis.Functions.NewsSource
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalNewsSourceDal = InitDal<Interfaces.INewsSourceDal>();
            builder.Services.AddSingleton<Interfaces.INewsSourceDal>(dalNewsSourceDal);
            builder.Services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.INewsSourceDal, DMFX.NewsAnalysis.Services.Dal.NewsSourceDal>();            
        }
    }
}