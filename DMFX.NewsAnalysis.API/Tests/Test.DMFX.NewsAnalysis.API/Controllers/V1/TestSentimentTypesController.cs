


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
    public class TestSentimentTypesController : E2ETestBase, IClassFixture<WebApplicationFactory<DMFX.NewsAnalysis.API.Startup>>
    {
        public TestSentimentTypesController(WebApplicationFactory<DMFX.NewsAnalysis.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void SentimentType_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }


                var respGetAll = client.GetAsync($"/api/v1/sentimenttypes");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<SentimentType> dtos = ExtractContentJson<List<SentimentType>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void SentimentType_Get_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = AddTestEntity();
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
                    var respGet = client.GetAsync($"/api/v1/sentimenttypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    SentimentType dto = ExtractContentJson<SentimentType>(respGet.Result.Content);

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
        public void SentimentType_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/sentimenttypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void SentimentType_Delete_Success()
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

                    var respDel = client.DeleteAsync($"/api/v1/sentimenttypes/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void SentimentType_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/sentimenttypes/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void SentimentType_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = CreateTestEntity();
                DMFX.NewsAnalysis.Interfaces.Entities.SentimentType respEntity = null;
                try
                {
                    var reqDto = SentimentTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/sentimenttypes/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    SentimentType respDto = ExtractContentJson<SentimentType>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);

                    respEntity = SentimentTypeConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void SentimentType_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = AddTestEntity();
                try
                {
                    testEntity.Name = "Name 4cb40435dc344e4e819ea2e4cfd8016e";

                    var reqDto = SentimentTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/sentimenttypes/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    SentimentType respDto = ExtractContentJson<SentimentType>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void SentimentType_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.SentimentType testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Name = "Name 4cb40435dc344e4e819ea2e4cfd8016e";

                    var reqDto = SentimentTypeConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/sentimenttypes/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.SentimentType entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();



                return dal.Delete(entity.ID
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
            entity.Name = "Name 66503fb7bb974381a44017be7492ee95";

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
