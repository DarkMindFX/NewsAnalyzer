


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
    public class TestSentimentTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISentimentTypeDal dal = new SentimentTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void SentimentType_GetAll_Success()
        {
            var dal = PrepareSentimentTypeDal("DALInitParams");

            IList<SentimentType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("SentimentType\\000.GetDetails.Success")]
        public void SentimentType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSentimentTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            SentimentType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 47aad278965f4dbaa558358e8acc9381", entity.Name);
                      }

        [Test]
        public void SentimentType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareSentimentTypeDal("DALInitParams");

            SentimentType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("SentimentType\\010.Delete.Success")]
        public void SentimentType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSentimentTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void SentimentType_Delete_InvalidId()
        {
            var dal = PrepareSentimentTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("SentimentType\\020.Insert.Success")]
        public void SentimentType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSentimentTypeDal("DALInitParams");

            var entity = new SentimentType();
                          entity.Name = "Name c805cbccbe5a44f6949a29d00d1fdff4";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name c805cbccbe5a44f6949a29d00d1fdff4", entity.Name);
              
        }

        [TestCase("SentimentType\\030.Update.Success")]
        public void SentimentType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSentimentTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            SentimentType entity = dal.Get(paramID);

                          entity.Name = "Name d6b57a42d0664379a518a9ad7f1d8db5";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name d6b57a42d0664379a518a9ad7f1d8db5", entity.Name);
              
        }

        [Test]
        public void SentimentType_Update_InvalidId()
        {
            var dal = PrepareSentimentTypeDal("DALInitParams");

            var entity = new SentimentType();
                          entity.Name = "Name d6b57a42d0664379a518a9ad7f1d8db5";
              
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


        protected ISentimentTypeDal PrepareSentimentTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISentimentTypeDal dal = new SentimentTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
