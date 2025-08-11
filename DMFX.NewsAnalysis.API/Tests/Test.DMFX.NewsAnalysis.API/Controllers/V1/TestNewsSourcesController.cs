


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
    public class TestNewsSourcesController : E2ETestBase, IClassFixture<WebApplicationFactory<DMFX.NewsAnalysis.API.Startup>>
    {
        public TestNewsSourcesController(WebApplicationFactory<DMFX.NewsAnalysis.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void NewsSource_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }


                var respGetAll = client.GetAsync($"/api/v1/newssources");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<NewsSource> dtos = ExtractContentJson<List<NewsSource>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void NewsSource_Get_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = AddTestEntity();
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
                    var respGet = client.GetAsync($"/api/v1/newssources/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    NewsSource dto = ExtractContentJson<NewsSource>(respGet.Result.Content);

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
        public void NewsSource_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/newssources/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void NewsSource_Delete_Success()
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

                    var respDel = client.DeleteAsync($"/api/v1/newssources/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void NewsSource_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/newssources/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void NewsSource_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }


                DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = CreateTestEntity();
                DMFX.NewsAnalysis.Interfaces.Entities.NewsSource respEntity = null;
                try
                {
                    var reqDto = NewsSourceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/newssources/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    NewsSource respDto = ExtractContentJson<NewsSource>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.Url, respDto.Url);
                                    Assert.Equal(reqDto.IsActive, respDto.IsActive);
                
                    respEntity = NewsSourceConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void NewsSource_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }


                DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = AddTestEntity();
                try
                {
                          testEntity.Name = "Name acb5134de8e64fc1b2ff9a0fb12ecf27";
                            testEntity.Url = "Url acb5134de8e64fc1b2ff9a0fb12ecf27";
                            testEntity.IsActive = true;              
              
                    var reqDto = NewsSourceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/newssources/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    NewsSource respDto = ExtractContentJson<NewsSource>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.Url, respDto.Url);
                                    Assert.Equal(reqDto.IsActive, respDto.IsActive);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void NewsSource_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.NewsSource testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name acb5134de8e64fc1b2ff9a0fb12ecf27";
                            testEntity.Url = "Url acb5134de8e64fc1b2ff9a0fb12ecf27";
                            testEntity.IsActive = true;              
              
                    var reqDto = NewsSourceConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/newssources/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
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
                          entity.Name = "Name 5380771b0076416e87654d0316ca35c8";
                            entity.Url = "Url 5380771b0076416e87654d0316ca35c8";
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
