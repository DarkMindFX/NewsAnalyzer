


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
    public class TestAnalyzerDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IAnalyzerDal dal = new AnalyzerDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Analyzer_GetAll_Success()
        {
            var dal = PrepareAnalyzerDal("DALInitParams");

            IList<Analyzer> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Analyzer\\000.GetDetails.Success")]
        public void Analyzer_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAnalyzerDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Analyzer entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name e054dbf73c584a90b80a3d64def5cdd7", entity.Name);
                            Assert.AreEqual(true, entity.IsActive);
                      }

        [Test]
        public void Analyzer_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareAnalyzerDal("DALInitParams");

            Analyzer entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Analyzer\\010.Delete.Success")]
        public void Analyzer_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAnalyzerDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Analyzer_Delete_InvalidId()
        {
            var dal = PrepareAnalyzerDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Analyzer\\020.Insert.Success")]
        public void Analyzer_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareAnalyzerDal("DALInitParams");

            var entity = new Analyzer();
                          entity.Name = "Name b384a32ba71245d1b1cfbeb9cb2c01b2";
                            entity.IsActive = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name b384a32ba71245d1b1cfbeb9cb2c01b2", entity.Name);
                            Assert.AreEqual(true, entity.IsActive);
              
        }

        [TestCase("Analyzer\\030.Update.Success")]
        public void Analyzer_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareAnalyzerDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Analyzer entity = dal.Get(paramID);

                          entity.Name = "Name 6dcd350f08ef485396c1a1256ed3ff76";
                            entity.IsActive = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 6dcd350f08ef485396c1a1256ed3ff76", entity.Name);
                            Assert.AreEqual(true, entity.IsActive);
              
        }

        [Test]
        public void Analyzer_Update_InvalidId()
        {
            var dal = PrepareAnalyzerDal("DALInitParams");

            var entity = new Analyzer();
                          entity.Name = "Name 6dcd350f08ef485396c1a1256ed3ff76";
                            entity.IsActive = true;              
              
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


        protected IAnalyzerDal PrepareAnalyzerDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IAnalyzerDal dal = new AnalyzerDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
