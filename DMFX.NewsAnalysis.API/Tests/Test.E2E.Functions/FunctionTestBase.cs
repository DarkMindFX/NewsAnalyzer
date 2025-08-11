using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DMFX.NewsAnalysis.Test.Functions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Test.E2E.Functions
{
    public abstract class FunctionTestBase
    {
        public class TestParams
        {
            public TestParams()
            {
                Settings = new Dictionary<string, object>();
            }

            public Dictionary<string, object> Settings
            {
                get;
                set;
            }
        }

        protected TestParams _testParams;

        protected TestParams GetTestParams(string name)
        {
            TestParams testParams = new TestParams();

            var config = GetConfiguration();

            testParams.Settings = config.GetSection(name).GetChildren().ToDictionary(x => x.Key, x => (object)x.Value);

            return testParams;
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }

        protected TFunc GetFunction<TFunc>(IHost host)
        {
            Type type = typeof(TFunc);

            ConstructorInfo contructorInfo = type.GetConstructors().FirstOrDefault();

            ParameterInfo[] parametersInfo = contructorInfo.GetParameters();

            object[] parameters = LookupServiceInstances(host, parametersInfo);

            return (TFunc)Activator.CreateInstance(type, parameters);
        }

        private object[] LookupServiceInstances(IHost host, IReadOnlyList<ParameterInfo> parametersInfo)
        {
            return parametersInfo.Select(p => host.Services.GetService(p.ParameterType))
                                 .ToArray();
        }

        protected async Task<DMFX.NewsAnalysis.DTO.LoginResponse> Login(string login, string password, IHost host, ILogger logger)
        {

            var dtoLogin = new DMFX.NewsAnalysis.DTO.LoginRequest()
            {
                Login = login,
                Password = password
            };

            DMFX.NewsAnalysis.DTO.LoginResponse dtoResp = null;

            // TODO: Implement call to Login function
            /*
            var request = TestFactory.CreateHttpRequest(dtoLogin, null);
            var response = (ObjectResult)await (GetFunction<DMFX.NewsAnalysis.Functions.User.V1.Login>(host)).Run(request, logger);
            
            var dtoResp = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.LoginResponse>(response.Value.ToString());
            */
            return dtoResp;

        }
    }
}
