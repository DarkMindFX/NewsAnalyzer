

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
    public class TestSentimentTypeFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private DMFX.NewsAnalysis.Functions.SentimentType.Startup _startup;
        private IHost _host;


        public TestSentimentTypeFunctions()
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

            _startup = new DMFX.NewsAnalysis.Functions.SentimentType.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)                
                .Build();
        }

        [Test]
        public async Task SentimentTypesGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<DMFX.NewsAnalysis.DTO.SentimentType>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task SentimentTypesGetDetails_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.GetDetails>(_host)).Run(request,
					testEntity.ID,
					_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.SentimentType>(response.Value.ToString());

                Assert.NotNull(dto);
				Assert.AreEqual(testEntity.ID, dto.ID);
                
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task SentimentTypesGetDetails_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.GetDetails>(_host)).Run(request, 
				paramID,
			_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task SentimentTypesDelete_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.Delete>(_host)).Run(request, 
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
        public async Task SentimentTypesDelete_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.Delete>(_host)).Run(request, 
				paramID,
				_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task SentimentTypesInsert_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = CreateTestEntity();

            try
            {
                var dtoReq = DMFX.NewsAnalysis.Utils.Convertors.SentimentTypeConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.SentimentType>(response.Value.ToString());

                Assert.NotNull(dto);

						testEntity.ID = dto.ID;
		
		                    Assert.NotNull(dto.ID);
                            Assert.AreEqual(dtoReq.Name, dto.Name);
		            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task SentimentTypesUpdate_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = AddTestEntity();

            try
            {
                            testEntity.Name = "Name 1412caa3e4d44364b2624cbda3fa8fed";
              
                var reqDto = SentimentTypeConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

				var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.SentimentType>(response.Value.ToString());

				                    Assert.NotNull(dto.ID);
                                    Assert.AreEqual(reqDto.Name, dto.Name);
                            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task SentimentTypesUpdate_NotFound()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = CreateTestEntity();

            try
            {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name 1412caa3e4d44364b2624cbda3fa8fed";
              
                var reqDto = SentimentTypeConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.SentimentType.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

		#region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.SentimentType entity)
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

        protected DMFX.NewsAnalysis.Interfaces.Entities.SentimentType CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.SentimentType();
                          entity.Name = "Name 5aaa01900a604b59b54f6e02538b42ae";
              
            return entity;
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.SentimentType AddTestEntity()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private DMFX.NewsAnalysis.Interfaces.ISentimentTypeDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            DMFX.NewsAnalysis.Interfaces.ISentimentTypeDal dal = new DMFX.NewsAnalysis.DAL.MSSQL.SentimentTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}