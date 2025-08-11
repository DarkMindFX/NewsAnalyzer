

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;
using DMFX.NewsAnalysis.API.Helpers;
using DMFX.NewsAnalysis.API.MiddleWare;
using DMFX.NewsAnalysis.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DMFX.NewsAnalysis.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            PrepareComposition();

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            AddInjections(services, serviceConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

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
            var dalAnalyzerDal = InitDal<IAnalyzerDal>(serviceCfg);
            services.AddSingleton<IAnalyzerDal>(dalAnalyzerDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IAnalyzerDal, DMFX.NewsAnalysis.Services.Dal.AnalyzerDal>();

            var dalArticleDal = InitDal<IArticleDal>(serviceCfg);
            services.AddSingleton<IArticleDal>(dalArticleDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IArticleDal, DMFX.NewsAnalysis.Services.Dal.ArticleDal>();

            var dalArticleAnalysisDal = InitDal<IArticleAnalysisDal>(serviceCfg);
            services.AddSingleton<IArticleAnalysisDal>(dalArticleAnalysisDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.IArticleAnalysisDal, DMFX.NewsAnalysis.Services.Dal.ArticleAnalysisDal>();

            var dalNewsSourceDal = InitDal<INewsSourceDal>(serviceCfg);
            services.AddSingleton<INewsSourceDal>(dalNewsSourceDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.INewsSourceDal, DMFX.NewsAnalysis.Services.Dal.NewsSourceDal>();

            var dalSentimentTypeDal = InitDal<ISentimentTypeDal>(serviceCfg);
            services.AddSingleton<ISentimentTypeDal>(dalSentimentTypeDal);
            services.AddSingleton<DMFX.NewsAnalysis.Services.Dal.ISentimentTypeDal, DMFX.NewsAnalysis.Services.Dal.SentimentTypeDal>();


            /** Connection Tester for health endpoint **/
            var dalConnTest = InitDal<IConnectionTestDal>(serviceCfg);
            services.AddSingleton<IConnectionTestDal>(dalConnTest);
        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;

        }
    }
}
