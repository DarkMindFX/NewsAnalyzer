

using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.MCP.Helpers;
using DMFX.NewsAnalysis.MCP.Resources;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace DMFX.NewsAnalysis.MCP
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Initialize(args);
        }

        public void Initialize(string[] args)
        {
            var builder = CreateHostBuilder(args);

            if (builder != null)
            {
                var app = builder.Build();

                app.MapMcp();

                app.Run();
            }
            else
            {
                Console.WriteLine("Failed to create host builder.");
            }

        }

        public WebApplicationBuilder CreateHostBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMcpServer()
                    .WithHttpTransport()
                    .WithResources<ArticleResourceType>();

            PrepareComposition();

            var serviceConfig = builder.Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();

            AddInjections(builder.Services, serviceConfig);

            return builder;

        }

        #region Support methods

        private void PrepareComposition()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(AssemblyDirectory);
            catalog.Catalogs.Add(directoryCatalog);
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private CompositionContainer Container
        {
            get;
            set;
        }

        private void AddInjections(IServiceCollection services, ServiceConfig serviceCfg)
        {

            var dalArticleDal = InitDal<IArticleDal>(serviceCfg);
            services.AddSingleton<IArticleDal>(dalArticleDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IArticleDal, DMFX.NewsAnalysis.Services.Dal.ArticleDal>();
        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;

        }

        #endregion

    }
}

