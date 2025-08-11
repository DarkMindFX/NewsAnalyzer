


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
            
                          Assert.AreEqual(DateTime.Parse("2/2/2027 9:43:32 PM"), entity.Timestamp);
                            Assert.AreEqual(39, entity.ArticleID);
                            Assert.AreEqual(5, entity.SentimentID);
                            Assert.AreEqual(4, entity.AnalyzerID);
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
                          entity.Timestamp = DateTime.Parse("10/29/2027 1:44:32 PM");
                            entity.ArticleID = 13;
                            entity.SentimentID = 5;
                            entity.AnalyzerID = 2;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(DateTime.Parse("10/29/2027 1:44:32 PM"), entity.Timestamp);
                            Assert.AreEqual(13, entity.ArticleID);
                            Assert.AreEqual(5, entity.SentimentID);
                            Assert.AreEqual(2, entity.AnalyzerID);
              
        }

        [TestCase("ArticleAnalysis\\030.Update.Success")]
        public void ArticleAnalysis_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ArticleAnalysis entity = dal.Get(paramID);

                          entity.Timestamp = DateTime.Parse("1/28/2028 9:18:32 AM");
                            entity.ArticleID = 24;
                            entity.SentimentID = 5;
                            entity.AnalyzerID = 5;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(DateTime.Parse("1/28/2028 9:18:32 AM"), entity.Timestamp);
                            Assert.AreEqual(24, entity.ArticleID);
                            Assert.AreEqual(5, entity.SentimentID);
                            Assert.AreEqual(5, entity.AnalyzerID);
              
        }

        [Test]
        public void ArticleAnalysis_Update_InvalidId()
        {
            var dal = PrepareArticleAnalysisDal("DALInitParams");

            var entity = new ArticleAnalysis();
                          entity.Timestamp = DateTime.Parse("1/28/2028 9:18:32 AM");
                            entity.ArticleID = 24;
                            entity.SentimentID = 5;
                            entity.AnalyzerID = 5;
              
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
