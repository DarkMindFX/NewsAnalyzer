


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
            
                          Assert.AreEqual("Title 086d574f65f842c4831aeb20e8d637fb", entity.Title);
                            Assert.AreEqual("Content 086d574f65f842c4831aeb20e8d637fb", entity.Content);
                            Assert.AreEqual(DateTime.Parse("1/1/2026 6:48:04 AM"), entity.Timestamp);
                            Assert.AreEqual(2, entity.NewsSourceID);
                            Assert.AreEqual("Url 086d574f65f842c4831aeb20e8d637fb", entity.Url);
                            Assert.AreEqual(DateTime.Parse("8/21/2023 12:09:04 PM"), entity.NewsTime);
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
                          entity.Title = "Title 7c45f97983d34cf981642df83c237ef4";
                            entity.Content = "Content 7c45f97983d34cf981642df83c237ef4";
                            entity.Timestamp = DateTime.Parse("6/30/2026 12:36:04 PM");
                            entity.NewsSourceID = 1;
                            entity.Url = "Url 7c45f97983d34cf981642df83c237ef4";
                            entity.NewsTime = DateTime.Parse("6/30/2026 12:36:04 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 7c45f97983d34cf981642df83c237ef4", entity.Title);
                            Assert.AreEqual("Content 7c45f97983d34cf981642df83c237ef4", entity.Content);
                            Assert.AreEqual(DateTime.Parse("6/30/2026 12:36:04 PM"), entity.Timestamp);
                            Assert.AreEqual(1, entity.NewsSourceID);
                            Assert.AreEqual("Url 7c45f97983d34cf981642df83c237ef4", entity.Url);
                            Assert.AreEqual(DateTime.Parse("6/30/2026 12:36:04 PM"), entity.NewsTime);
              
        }

        [TestCase("Article\\030.Update.Success")]
        public void Article_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Article entity = dal.Get(paramID);

                          entity.Title = "Title 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.Content = "Content 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.Timestamp = DateTime.Parse("6/30/2026 12:36:04 PM");
                            entity.NewsSourceID = 5;
                            entity.Url = "Url 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.NewsTime = DateTime.Parse("9/28/2026 8:09:04 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 5767b41dc8364c2b88fdae9d653c9ae2", entity.Title);
                            Assert.AreEqual("Content 5767b41dc8364c2b88fdae9d653c9ae2", entity.Content);
                            Assert.AreEqual(DateTime.Parse("6/30/2026 12:36:04 PM"), entity.Timestamp);
                            Assert.AreEqual(5, entity.NewsSourceID);
                            Assert.AreEqual("Url 5767b41dc8364c2b88fdae9d653c9ae2", entity.Url);
                            Assert.AreEqual(DateTime.Parse("9/28/2026 8:09:04 AM"), entity.NewsTime);
              
        }

        [Test]
        public void Article_Update_InvalidId()
        {
            var dal = PrepareArticleDal("DALInitParams");

            var entity = new Article();
                          entity.Title = "Title 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.Content = "Content 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.Timestamp = DateTime.Parse("6/30/2026 12:36:04 PM");
                            entity.NewsSourceID = 5;
                            entity.Url = "Url 5767b41dc8364c2b88fdae9d653c9ae2";
                            entity.NewsTime = DateTime.Parse("9/28/2026 8:09:04 AM");
              
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
