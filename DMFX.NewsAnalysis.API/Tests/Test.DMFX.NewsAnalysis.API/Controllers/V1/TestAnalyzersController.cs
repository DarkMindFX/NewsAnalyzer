


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
    public class TestAnalyzersController : E2ETestBase, IClassFixture<WebApplicationFactory<DMFX.NewsAnalysis.API.Startup>>
    {
        public TestAnalyzersController(WebApplicationFactory<DMFX.NewsAnalysis.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Analyzer_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                var respGetAll = client.GetAsync($"/api/v1/analyzers");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Analyzer> dtos = ExtractContentJson<List<Analyzer>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Analyzer_Get_Success()
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = AddTestEntity();
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
                    var respGet = client.GetAsync($"/api/v1/analyzers/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Analyzer dto = ExtractContentJson<Analyzer>(respGet.Result.Content);

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
        public void Analyzer_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/analyzers/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Analyzer_Delete_Success()
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

                    var respDel = client.DeleteAsync($"/api/v1/analyzers/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Analyzer_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/analyzers/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Analyzer_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = CreateTestEntity();
                DMFX.NewsAnalysis.Interfaces.Entities.Analyzer respEntity = null;
                try
                {
                    var reqDto = AnalyzerConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/analyzers/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Analyzer respDto = ExtractContentJson<Analyzer>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.IsActive, respDto.IsActive);

                    respEntity = AnalyzerConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Analyzer_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = AddTestEntity();
                try
                {
                    testEntity.Name = "Name 0f72b1bf940a4ba0a473fe98b1fcca96";
                    testEntity.IsActive = true;

                    var reqDto = AnalyzerConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/analyzers/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Analyzer respDto = ExtractContentJson<Analyzer>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);
                    Assert.Equal(reqDto.IsActive, respDto.IsActive);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Analyzer_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                if (!string.IsNullOrEmpty(_testParams.Settings["test_user_login"].ToString()))
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);
                }

                DMFX.NewsAnalysis.Interfaces.Entities.Analyzer testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.Name = "Name 0f72b1bf940a4ba0a473fe98b1fcca96";
                    testEntity.IsActive = true;

                    var reqDto = AnalyzerConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/analyzers/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity)
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

        protected DMFX.NewsAnalysis.Interfaces.Entities.Analyzer CreateTestEntity()
        {
            var entity = new DMFX.NewsAnalysis.Interfaces.Entities.Analyzer();
            entity.Name = "Name a804e716073840acbdad6e793ee85a01";
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
