using DMFX.NewsAnalysis.Functions.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DMFX.NewsAnalysis.Function.ArticleParser;
{
    public class Program : FunctionStartupBase
{
    public void Startup()
    {

        var host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            base.Configure();

            InitServices(services);
        })
        .Build();

        host.Run();
    }

    protected void InitServices(IServiceCollection services)
    {
        var dalArticles = InitDal<DMFX.NewsAnalysis.Interfaces.IArticleDal>();
        services.AddSingleton<DMFX.NewsAnalysis.Interfaces.IArticleDal>(dalArticles);
        services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IAnalyzerDal, DMFX.NewsAnalysis.Services.Dal.AnalyzerDal>();
    }

    public static void Main(string[] args)
    {
        var function = new Program();
        function.Startup();
    }
}}
