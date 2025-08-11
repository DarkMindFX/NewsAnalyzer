using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using DMFX.NewsAnalysis.Functions.Common;

[assembly: FunctionsStartup(typeof(DMFX.NewsAnalysis.Functions.Analyzer.Startup))]
namespace DMFX.NewsAnalysis.Functions.Analyzer
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalAnalyzerDal = InitDal<Interfaces.IAnalyzerDal>();
            builder.Services.AddSingleton<Interfaces.IAnalyzerDal>(dalAnalyzerDal);
            builder.Services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IAnalyzerDal, DMFX.NewsAnalysis.Services.Dal.AnalyzerDal>();            
        }
    }
}