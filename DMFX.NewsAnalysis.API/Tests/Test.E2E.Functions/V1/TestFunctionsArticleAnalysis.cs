

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
    public class TestArticleAnalysisFunctions : FunctionTestBase
    {
        private readonly ILogger _logger = TestFactory.CreateLogger();
        private DMFX.NewsAnalysis.Functions.ArticleAnalysis.Startup _startup;
        private IHost _host;


        public TestArticleAnalysisFunctions()
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

            _startup = new DMFX.NewsAnalysis.Functions.ArticleAnalysis.Startup();
            _host = new HostBuilder()
                .ConfigureWebJobs(_startup.Configure)                
                .Build();
        }

        [Test]
        public async Task ArticleAnalysisesGetAll_Success()
        {
            var request = TestFactory.CreateHttpRequest();

            var function = GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.GetAll>(_host);

            var response = (ObjectResult)await function.Run(request, _logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var dtos = JsonSerializer.Deserialize<List<DMFX.NewsAnalysis.DTO.ArticleAnalysis>>(response.Value.ToString());

            Assert.NotNull(dtos);
            Assert.IsNotEmpty(dtos);
        }

        [Test]
        public async Task ArticleAnalysisesGetDetails_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.GetDetails>(_host)).Run(request,
					testEntity.ID,
					_logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.ArticleAnalysis>(response.Value.ToString());

                Assert.NotNull(dto);
				Assert.AreEqual(testEntity.ID, dto.ID);
                
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }

        }

        [Test]
        public async Task ArticleAnalysisesGetDetails_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.GetDetails>(_host)).Run(request, 
				paramID,
			_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ArticleAnalysisesDelete_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = AddTestEntity();

            try
            {
                var request = TestFactory.CreateHttpRequest();
                var response = (StatusCodeResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.Delete>(_host)).Run(request, 
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
        public async Task ArticleAnalysisesDelete_NotFound()
        {
			var paramID = Int64.MaxValue;
            var request = TestFactory.CreateHttpRequest();
            var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.Delete>(_host)).Run(request, 
				paramID,
				_logger);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ArticleAnalysisesInsert_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = CreateTestEntity();

            try
            {
                var dtoReq = DMFX.NewsAnalysis.Utils.Convertors.ArticleAnalysisConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(dtoReq);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.Insert>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);

                var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.ArticleAnalysis>(response.Value.ToString());

                Assert.NotNull(dto);

						testEntity.ID = dto.ID;
		
		                    Assert.NotNull(dto.ID);
                            Assert.AreEqual(dtoReq.Timestamp, dto.Timestamp);
		                    Assert.AreEqual(dtoReq.ArticleID, dto.ArticleID);
		                    Assert.AreEqual(dtoReq.SentimentID, dto.SentimentID);
		                    Assert.AreEqual(dtoReq.AnalyzerID, dto.AnalyzerID);
		            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task ArticleAnalysisesUpdate_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = AddTestEntity();

            try
            {
                            testEntity.Timestamp = DateTime.Parse("10/30/2025 8:10:32 PM");
                            testEntity.ArticleID = 34 ;
                            testEntity.SentimentID = 7 ;
                            testEntity.AnalyzerID = 10 ;
              
                var reqDto = ArticleAnalysisConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

				var dto = JsonSerializer.Deserialize<DMFX.NewsAnalysis.DTO.ArticleAnalysis>(response.Value.ToString());

				                    Assert.NotNull(dto.ID);
                                    Assert.AreEqual(reqDto.Timestamp, dto.Timestamp);
                                    Assert.AreEqual(reqDto.ArticleID, dto.ArticleID);
                                    Assert.AreEqual(reqDto.SentimentID, dto.SentimentID);
                                    Assert.AreEqual(reqDto.AnalyzerID, dto.AnalyzerID);
                            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

        [Test]
        public async Task ArticleAnalysisesUpdate_NotFound()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = CreateTestEntity();

            try
            {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Timestamp = DateTime.Parse("10/30/2025 8:10:32 PM");
                            testEntity.ArticleID = 34;
                            testEntity.SentimentID = 7;
                            testEntity.AnalyzerID = 10;
              
                var reqDto = ArticleAnalysisConvertor.Convert(testEntity, null);

                var request = TestFactory.CreateHttpRequest(reqDto);
                var response = (ObjectResult)await(GetFunction<DMFX.NewsAnalysis.Functions.ArticleAnalysis.V1.Update>(_host)).Run(request, _logger);

                Assert.IsNotNull(response);
                Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            }
            finally
            {
                RemoveTestEntity(testEntity);
            }
        }

		#region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis entity)
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

        protected DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis();
                          entity.Timestamp = DateTime.Parse("12/20/2022 10:23:32 AM");
                            entity.ArticleID = 22;
                            entity.SentimentID = 3;
                            entity.AnalyzerID = 7;
              
            return entity;
        }

        protected DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis AddTestEntity()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private DMFX.NewsAnalysis.Interfaces.IArticleAnalysisDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            DMFX.NewsAnalysis.Interfaces.IArticleAnalysisDal dal = new DMFX.NewsAnalysis.DAL.MSSQL.ArticleAnalysisDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}