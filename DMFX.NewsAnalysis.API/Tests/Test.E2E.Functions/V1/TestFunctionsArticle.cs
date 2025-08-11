

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
    public class TestArticleFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private DMFX.NewsAnalysis.Functions.Article.Startup _startup;
        private IHost _host;


        public TestArticleFunctions()
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

            _startup = new DMFX.NewsAnalysis.Functions.Article.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)                
                .Build();
        }

        [Test]
        public async Task ArticlesGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<DMFX.NewsAnalysis.DTO.Article>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task ArticlesGetDetails_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.GetDetails>(_host)).Run(request,
					testEntity.ID,
					_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Article>(response.Value.ToString());

                Assert.NotNull(dto);
				Assert.AreEqual(testEntity.ID, dto.ID);
                
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task ArticlesGetDetails_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.GetDetails>(_host)).Run(request, 
				paramID,
			_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ArticlesDelete_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.Delete>(_host)).Run(request, 
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
        public async Task ArticlesDelete_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.Delete>(_host)).Run(request, 
				paramID,
				_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ArticlesInsert_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = CreateTestEntity();

            try
            {
                var dtoReq = DMFX.NewsAnalysis.Utils.Convertors.ArticleConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Article>(response.Value.ToString());

                Assert.NotNull(dto);

						testEntity.ID = dto.ID;
		
		                    Assert.NotNull(dto.ID);
                            Assert.AreEqual(dtoReq.Title, dto.Title);
		                    Assert.AreEqual(dtoReq.Content, dto.Content);
		                    Assert.AreEqual(dtoReq.Timestamp, dto.Timestamp);
		                    Assert.AreEqual(dtoReq.NewsSourceID, dto.NewsSourceID);
		            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task ArticlesUpdate_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = AddTestEntity();

            try
            {
                            testEntity.Title = "Title d6d556ac78fd4ace8bbfcbb6f8f5d0d9";
                            testEntity.Content = "Content d6d556ac78fd4ace8bbfcbb6f8f5d0d9";
                            testEntity.Timestamp = DateTime.Parse("5/4/2025 2:23:32 PM");
                            testEntity.NewsSourceID = 3 ;
              
                var reqDto = ArticleConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

				var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.Article>(response.Value.ToString());

				                    Assert.NotNull(dto.ID);
                                    Assert.AreEqual(reqDto.Title, dto.Title);
                                    Assert.AreEqual(reqDto.Content, dto.Content);
                                    Assert.AreEqual(reqDto.Timestamp, dto.Timestamp);
                                    Assert.AreEqual(reqDto.NewsSourceID, dto.NewsSourceID);
                            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task ArticlesUpdate_NotFound()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = CreateTestEntity();

            try
            {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Title = "Title d6d556ac78fd4ace8bbfcbb6f8f5d0d9";
                            testEntity.Content = "Content d6d556ac78fd4ace8bbfcbb6f8f5d0d9";
                            testEntity.Timestamp = DateTime.Parse("5/4/2025 2:23:32 PM");
                            testEntity.NewsSourceID = 3;
              
                var reqDto = ArticleConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.Article.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

		#region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.Article entity)
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

        protected DMFX.NewsAnalysis.Interfaces.Entities.Article CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.Article();
                          entity.Title = "Title 468b149affce4d6793a534549909040e";
                            entity.Content = "Content 468b149affce4d6793a534549909040e";
                            entity.Timestamp = DateTime.Parse("12/15/2027 4:36:32 AM");
                            entity.NewsSourceID = 10;
              
            return entity;
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.Article AddTestEntity()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private DMFX.NewsAnalysis.Interfaces.IArticleDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            DMFX.NewsAnalysis.Interfaces.IArticleDal dal = new DMFX.NewsAnalysis.DAL.MSSQL.ArticleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}