


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
    public class TestArticleAnalysisDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IArticleAnalysisDal dal = new ArticleAnalysisDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ArticleAnalysis_GetAll_Success()
        {
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            IList<ArticleAnalysis> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ArticleAnalysis\\000.GetDetails.Success")]
        public void ArticleAnalysis_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ArticleAnalysis entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(DateTime.Parse("8/12/2024 2:24:04 PM"), entity.Timestamp);
                            Assert.AreEqual(9, entity.ArticleID);
                            Assert.AreEqual(10, entity.SentimentID);
                            Assert.AreEqual(1, entity.AnalyzerID);
                      }

        [Test]
        public void ArticleAnalysis_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            ArticleAnalysis entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ArticleAnalysis\\010.Delete.Success")]
        public void ArticleAnalysis_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ArticleAnalysis_Delete_InvalidId()
        {
            var dal = PrepareArticleAnalysisDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ArticleAnalysis\\020.Insert.Success")]
        public void ArticleAnalysis_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareArticleAnalysisDal("DALInitParams");

            var entity = new ArticleAnalysis();
                          entity.Timestamp = DateTime.Parse("12/21/2027 5:58:04 AM");
                            entity.ArticleID = 18;
                            entity.SentimentID = 4;
                            entity.AnalyzerID = 8;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(DateTime.Parse("12/21/2027 5:58:04 AM"), entity.Timestamp);
                            Assert.AreEqual(18, entity.ArticleID);
                            Assert.AreEqual(4, entity.SentimentID);
                            Assert.AreEqual(8, entity.AnalyzerID);
              
        }

        [TestCase("ArticleAnalysis\\030.Update.Success")]
        public void ArticleAnalysis_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ArticleAnalysis entity = dal.Get(paramID);

                          entity.Timestamp = DateTime.Parse("5/9/2025 6:25:04 AM");
                            entity.ArticleID = 35;
                            entity.SentimentID = 1;
                            entity.AnalyzerID = 3;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(DateTime.Parse("5/9/2025 6:25:04 AM"), entity.Timestamp);
                            Assert.AreEqual(35, entity.ArticleID);
                            Assert.AreEqual(1, entity.SentimentID);
                            Assert.AreEqual(3, entity.AnalyzerID);
              
        }

        [Test]
        public void ArticleAnalysis_Update_InvalidId()
        {
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            var entity = new ArticleAnalysis();
                          entity.Timestamp = DateTime.Parse("5/9/2025 6:25:04 AM");
                            entity.ArticleID = 35;
                            entity.SentimentID = 1;
                            entity.AnalyzerID = 3;
              
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


        protected IArticleAnalysisDal PrepareArticleAnalysisDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IArticleAnalysisDal dal = new ArticleAnalysisDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
