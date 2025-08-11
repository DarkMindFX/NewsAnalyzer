


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
    public class TestArticlesController : E2ETestBase, IClassFixture<WebApplicationFactory<DMFX.NewsAnalysis.API.Startup>>
    {
        public TestArticlesController(WebApplicationFactory<DMFX.NewsAnalysis.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Article_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/articles");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Article> dtos = ExtractContentJson<List<Article>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Article_Get_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/articles/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Article dto = ExtractContentJson<Article>(respGet.Result.Content);

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
        public void Article_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/articles/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Article_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/articles/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Article_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/articles/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Article_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = CreateTestEntity();
                DMFX.NewsAnalysis.Interfaces.Entities.Article respEntity = null;
                try
                {
                    var reqDto = ArticleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/articles/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Article respDto = ExtractContentJson<Article>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Title, respDto.Title);
                                    Assert.Equal(reqDto.Content, respDto.Content);
                                    Assert.Equal(reqDto.Timestamp, respDto.Timestamp);
                                    Assert.Equal(reqDto.NewsSourceID, respDto.NewsSourceID);
                                    Assert.Equal(reqDto.Url, respDto.Url);
                                    Assert.Equal(reqDto.NewsTime, respDto.NewsTime);
                
                    respEntity = ArticleConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Article_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = AddTestEntity();
                try
                {
                          testEntity.Title = "Title bda8ca490c924721b2b72aa5a9670918";
                            testEntity.Content = "Content bda8ca490c924721b2b72aa5a9670918";
                            testEntity.Timestamp = DateTime.Parse("5/6/2027 6:50:04 AM");
                            testEntity.NewsSourceID = 7 ;
                            testEntity.Url = "Url bda8ca490c924721b2b72aa5a9670918";
                            testEntity.NewsTime = DateTime.Parse("9/23/2024 4:37:04 PM");
              
                    var reqDto = ArticleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/articles/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Article respDto = ExtractContentJson<Article>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Title, respDto.Title);
                                    Assert.Equal(reqDto.Content, respDto.Content);
                                    Assert.Equal(reqDto.Timestamp, respDto.Timestamp);
                                    Assert.Equal(reqDto.NewsSourceID, respDto.NewsSourceID);
                                    Assert.Equal(reqDto.Url, respDto.Url);
                                    Assert.Equal(reqDto.NewsTime, respDto.NewsTime);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Article_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                DMFX.NewsAnalysis.Interfaces.Entities.Article testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Title = "Title bda8ca490c924721b2b72aa5a9670918";
                            testEntity.Content = "Content bda8ca490c924721b2b72aa5a9670918";
                            testEntity.Timestamp = DateTime.Parse("5/6/2027 6:50:04 AM");
                            testEntity.NewsSourceID = 7;
                            testEntity.Url = "Url bda8ca490c924721b2b72aa5a9670918";
                            testEntity.NewsTime = DateTime.Parse("9/23/2024 4:37:04 PM");
              
                    var reqDto = ArticleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/articles/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
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
                          entity.Title = "Title c404b2710eeb4b8ab0aef390d5f8553e";
                            entity.Content = "Content c404b2710eeb4b8ab0aef390d5f8553e";
                            entity.Timestamp = DateTime.Parse("6/26/2024 6:23:04 AM");
                            entity.NewsSourceID = 1;
                            entity.Url = "Url c404b2710eeb4b8ab0aef390d5f8553e";
                            entity.NewsTime = DateTime.Parse("5/6/2027 6:50:04 AM");
              
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
