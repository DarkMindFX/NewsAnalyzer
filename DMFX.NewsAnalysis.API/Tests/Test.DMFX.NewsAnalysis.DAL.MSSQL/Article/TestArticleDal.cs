


using DMFX.NewsAnalysis.DAL.MSSQL;
using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.Interfaces.Entities;
using Test.DMFX.NewsAnalysis.DAL.MSSQL;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace DMFX.NewsAnalysis.Test.DAL.MSSQL
{
    public class TestArticleDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IArticleDal dal = new ArticleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Article_GetAll_Success()
        {
            var dal = PrepareArticleDal("DALInitParams");

            IList<Article> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Article\\000.GetDetails.Success")]
        public void Article_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Article entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Title eee2d5a03ab44af38ba23b396ae0cfcb", entity.Title);
            Assert.AreEqual("Content eee2d5a03ab44af38ba23b396ae0cfcb", entity.Content);
            Assert.AreEqual(DateTime.Parse("4/1/2023 9:42:32 AM"), entity.Timestamp);
            Assert.AreEqual(9, entity.NewsSourceID);
        }

        [Test]
        public void Article_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareArticleDal("DALInitParams");

            Article entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Article\\010.Delete.Success")]
        public void Article_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Article_Delete_InvalidId()
        {
            var dal = PrepareArticleDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Article\\020.Insert.Success")]
        public void Article_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareArticleDal("DALInitParams");

            var entity = new Article();
            entity.Title = "Title cae04eb615cc49f7a1615cae8a6ff4e5";
            entity.Content = "Content cae04eb615cc49f7a1615cae8a6ff4e5";
            entity.Timestamp = DateTime.Parse("5/10/2026 5:42:32 AM");
            entity.NewsSourceID = 8;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Title cae04eb615cc49f7a1615cae8a6ff4e5", entity.Title);
            Assert.AreEqual("Content cae04eb615cc49f7a1615cae8a6ff4e5", entity.Content);
            Assert.AreEqual(DateTime.Parse("5/10/2026 5:42:32 AM"), entity.Timestamp);
            Assert.AreEqual(8, entity.NewsSourceID);

        }

        [TestCase("Article\\030.Update.Success")]
        public void Article_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            Article entity = dal.Get(paramID);

            entity.Title = "Title a8c0d7c2ea9d4299b9f377bdceeb2a14";
            entity.Content = "Content a8c0d7c2ea9d4299b9f377bdceeb2a14";
            entity.Timestamp = DateTime.Parse("9/27/2023 6:09:32 AM");
            entity.NewsSourceID = 5;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Title a8c0d7c2ea9d4299b9f377bdceeb2a14", entity.Title);
            Assert.AreEqual("Content a8c0d7c2ea9d4299b9f377bdceeb2a14", entity.Content);
            Assert.AreEqual(DateTime.Parse("9/27/2023 6:09:32 AM"), entity.Timestamp);
            Assert.AreEqual(5, entity.NewsSourceID);

        }

        [Test]
        public void Article_Update_InvalidId()
        {
            var dal = PrepareArticleDal("DALInitParams");

            var entity = new Article();
            entity.Title = "Title a8c0d7c2ea9d4299b9f377bdceeb2a14";
            entity.Content = "Content a8c0d7c2ea9d4299b9f377bdceeb2a14";
            entity.Timestamp = DateTime.Parse("9/27/2023 6:09:32 AM");
            entity.NewsSourceID = 5;

            try
            {
                entity = dal.Update(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }


        protected IArticleDal PrepareArticleDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IArticleDal dal = new ArticleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
