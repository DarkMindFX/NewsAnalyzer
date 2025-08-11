


using DMFX.NewsAnalysis.DTO;
using DMFX.NewsAnalysis.Utils.Convertors;
using Test.E2E.API;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;
using Xunit; 

namespace Test.E2E.API.Controllers.V1
{
    public class TestArticleAnalysisesController : E2ETestBase, IClassFixture<WebApplicationFactory<DMFX.NewsAnalysis.API.Startup>>
    {
        public TestArticleAnalysisesController(WebApplicationFactory<DMFX.NewsAnalysis.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void ArticleAnalysis_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var respGetAll = client.GetAsync($"/api/v1/articleanalysises");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<ArticleAnalysis> dtos = ExtractContentJson<List<ArticleAnalysis>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void ArticleAnalysis_Get_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/articleanalysises/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    ArticleAnalysis dto = ExtractContentJson<ArticleAnalysis>(respGet.Result.Content);

                    Assert.NotNull(dto);
                    Assert.NotNull(dto.Links);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ArticleAnalysis_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/articleanalysises/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void ArticleAnalysis_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/articleanalysises/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ArticleAnalysis_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/articleanalysises/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void ArticleAnalysis_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = CreateTestEntity();
                DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis respEntity = null;
                try
                {
                    var reqDto = ArticleAnalysisConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/articleanalysises/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    ArticleAnalysis respDto = ExtractContentJson<ArticleAnalysis>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Timestamp, respDto.Timestamp);
                                    Assert.Equal(reqDto.ArticleID, respDto.ArticleID);
                                    Assert.Equal(reqDto.SentimentID, respDto.SentimentID);
                                    Assert.Equal(reqDto.AnalyzerID, respDto.AnalyzerID);
                
                    respEntity = ArticleAnalysisConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void ArticleAnalysis_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = AddTestEntity();
                try
                {
                          testEntity.Timestamp = DateTime.Parse("8/10/2027 4:47:32 AM");
                            testEntity.ArticleID = 35 ;
                            testEntity.SentimentID = 8 ;
                            testEntity.AnalyzerID = 10 ;
              
                    var reqDto = ArticleAnalysisConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/articleanalysises/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    ArticleAnalysis respDto = ExtractContentJson<ArticleAnalysis>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Timestamp, respDto.Timestamp);
                                    Assert.Equal(reqDto.ArticleID, respDto.ArticleID);
                                    Assert.Equal(reqDto.SentimentID, respDto.SentimentID);
                                    Assert.Equal(reqDto.AnalyzerID, respDto.AnalyzerID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ArticleAnalysis_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Timestamp = DateTime.Parse("8/10/2027 4:47:32 AM");
                            testEntity.ArticleID = 35;
                            testEntity.SentimentID = 8;
                            testEntity.AnalyzerID = 10;
              
                    var reqDto = ArticleAnalysisConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/articleanalysises/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
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
                          entity.Timestamp = DateTime.Parse("5/13/2027 6:33:32 PM");
                            entity.ArticleID = 19;
                            entity.SentimentID = 5;
                            entity.AnalyzerID = 3;
              
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
