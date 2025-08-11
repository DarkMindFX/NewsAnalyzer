

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
    public class TestNewsSourceFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private DMFX.NewsAnalysis.Functions.NewsSource.Startup _startup;
        private IHost _host;


        public TestNewsSourceFunctions()
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

            _startup = new DMFX.NewsAnalysis.Functions.NewsSource.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)                
                .Build();
        }

        [Test]
        public async Task NewsSourcesGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<DMFX.NewsAnalysis.DTO.NewsSource>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task NewsSourcesGetDetails_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.GetDetails>(_host)).Run(request,
					testEntity.ID,
					_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.NewsSource>(response.Value.ToString());

                Assert.NotNull(dto);
				Assert.AreEqual(testEntity.ID, dto.ID);
                
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task NewsSourcesGetDetails_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.GetDetails>(_host)).Run(request, 
				paramID,
			_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task NewsSourcesDelete_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.Delete>(_host)).Run(request, 
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
        public async Task NewsSourcesDelete_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.Delete>(_host)).Run(request, 
				paramID,
				_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task NewsSourcesInsert_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = CreateTestEntity();

            try
            {
                var dtoReq = DMFX.NewsAnalysis.Utils.Convertors.NewsSourceConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.NewsSource>(response.Value.ToString());

                Assert.NotNull(dto);

						testEntity.ID = dto.ID;
		
		                    Assert.NotNull(dto.ID);
                            Assert.AreEqual(dtoReq.Name, dto.Name);
		                    Assert.AreEqual(dtoReq.Url, dto.Url);
		                    Assert.AreEqual(dtoReq.IsActive, dto.IsActive);
		            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task NewsSourcesUpdate_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = AddTestEntity();

            try
            {
                            testEntity.Name = "Name 44f3ceea21494066ae4d6051b4058c83";
                            testEntity.Url = "Url 44f3ceea21494066ae4d6051b4058c83";
                            testEntity.IsActive = true;              
              
                var reqDto = NewsSourceConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

				var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.NewsSource>(response.Value.ToString());

				                    Assert.NotNull(dto.ID);
                                    Assert.AreEqual(reqDto.Name, dto.Name);
                                    Assert.AreEqual(reqDto.Url, dto.Url);
                                    Assert.AreEqual(reqDto.IsActive, dto.IsActive);
                            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task NewsSourcesUpdate_NotFound()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = CreateTestEntity();

            try
            {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name 44f3ceea21494066ae4d6051b4058c83";
                            testEntity.Url = "Url 44f3ceea21494066ae4d6051b4058c83";
                            testEntity.IsActive = true;              
              
                var reqDto = NewsSourceConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.NewsSource.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

		#region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.NewsSource entity)
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

        protected DMFX.NewsAnalysis.Interfaces.Entities.NewsSource CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.NewsSource();
                          entity.Name = "Name 24277829d483479e808cf7c625b39d40";
                            entity.Url = "Url 24277829d483479e808cf7c625b39d40";
                            entity.IsActive = true;              
              
            return entity;
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.NewsSource AddTestEntity()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private DMFX.NewsAnalysis.Interfaces.INewsSourceDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            DMFX.NewsAnalysis.Interfaces.INewsSourceDal dal = new DMFX.NewsAnalysis.DAL.MSSQL.NewsSourceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}