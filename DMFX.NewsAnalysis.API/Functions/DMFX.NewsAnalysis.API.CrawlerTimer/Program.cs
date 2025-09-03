using DMFX.NewsAnalysis.Functions.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Composition.Hosting;

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
        var dalNewsSources = InitDal<DMFX.NewsAnalysis.Interfaces.INewsSourceDal>();
        services.AddSingleton<DMFX.NewsAnalysis.Interfaces.INewsSourceDal>(dalNewsSources);
        services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.INewsSourceDal, DMFX.NewsAnalysis.Services.Dal.NewsSourceDal>();

        services.AddSingleton<ExportProvider>(this.Container);
    }

    public static void Main(string[] args)
    {
        var function = new Program();
        function.Startup();
    }
}