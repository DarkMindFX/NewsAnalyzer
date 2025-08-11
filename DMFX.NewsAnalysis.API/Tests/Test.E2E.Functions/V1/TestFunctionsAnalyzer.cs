

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using DMFX.NewsAnalysis.Utils.Convertors;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DMFX.NewsAnalysis.Test.Functions.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DMFX.NewsAnalysis.Test.E2E.Functions
{
    public class TestAnalyzerFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private DMFX.NewsAnalysis.Functions.Analyzer.Startup _startup;
        private IHost _host;


        public TestAnalyzerFunctions()
        {
            _testParams = GetTestParams("GenericFunctionTestSettings");
        }

        [SetUp]
        public void Setup()
        {
            var initParams = GetTestParams("DALInitParams");

            // Function replies on env vars for config
            Environment.SetEnvironmentVariable(DMFX.NewsAnalysis.Functions.Common.Constants.ENV_DAL_TYPE, _testParams.Settings["DALType"].ToString());
            Environment.SetEnvironmentVariable(DMFX.NewsAnalysis.Functions.Common.Constants.ENV_SQL_CONNECTION_STRING, (string)initParams.Settings["ConnectionString"]);
            Environment.SetEnvironmentVariable(DMFX.NewsAnalysis.Functions.Common.Constants.ENV_JWT_SECRET, (string)_testParams.Settings["JWTSecret"]);
            Environment.SetEnvironmentVariable(DMFX.NewsAnalysis.Functions.Common.Constants.ENV_SESSION_TIMEOUT, (string)_testParams.Settings["JWTSessionTimeout"]);

            _startup = new DMFX.NewsAnalysis.Functions.Analyzer.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)                
                .Build();
        }

        [Test]
        public async Task AnalyzersGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<DMFX.NewsAnalysis.DTO.Analyzer>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task AnalyzersGetDetails_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.GetDetails>(_host)).Run(request,
					testEntity.ID,
					_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Analyzer>(response.Value.ToString());

                Assert.NotNull(dto);
				Assert.AreEqual(testEntity.ID, dto.ID);
                
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task AnalyzersGetDetails_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.GetDetails>(_host)).Run(request, 
				paramID,
			_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AnalyzersDelete_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.Delete>(_host)).Run(request, 
									testEntity.ID,
								
				_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AnalyzersDelete_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.Delete>(_host)).Run(request, 
				paramID,
				_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AnalyzersInsert_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = CreateTestEntity();

            try
            {
                var dtoReq = DMFX.NewsAnalysis.Utils.Convertors.AnalyzerConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Analyzer>(response.Value.ToString());

                Assert.NotNull(dto);

						testEntity.ID = dto.ID;
		
		                    Assert.NotNull(dto.ID);
                            Assert.AreEqual(dtoReq.Name, dto.Name);
		                    Assert.AreEqual(dtoReq.IsActive, dto.IsActive);
		            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AnalyzersUpdate_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = AddTestEntity();

            try
            {
                            testEntity.Name = "Name d7f79f9d82b640608c6ebfe74e657694";
                            testEntity.IsActive = true;              
              
                var reqDto = AnalyzerConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

				var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Analyzer>(response.Value.ToString());

				                    Assert.NotNull(dto.ID);
                                    Assert.AreEqual(reqDto.Name, dto.Name);
                                    Assert.AreEqual(reqDto.IsActive, dto.IsActive);
                            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task AnalyzersUpdate_NotFound()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = CreateTestEntity();

            try
            {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name d7f79f9d82b640608c6ebfe74e657694";
                            testEntity.IsActive = true;              
              
                var reqDto = AnalyzerConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Analyzer.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

		#region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(                        entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.Analyzer CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.Analyzer();
                          entity.Name = "Name 08375491b40247efb727d4bc947ffe7f";
                            entity.IsActive = true;              
              
            return entity;
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.Analyzer AddTestEntity()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private DMFX.NewsAnalysis.Interfaces.IAnalyzerDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            DMFX.NewsAnalysis.Interfaces.IAnalyzerDal dal = new DMFX.NewsAnalysis.DAL.MSSQL.AnalyzerDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}