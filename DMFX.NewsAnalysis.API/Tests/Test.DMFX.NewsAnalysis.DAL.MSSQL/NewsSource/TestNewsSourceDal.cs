


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
    public class TestNewsSourceDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            INewsSourceDal dal = new NewsSourceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void NewsSource_GetAll_Success()
        {
            var dal = PrepareNewsSourceDal("DALInitParams");

            IList<NewsSource> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("NewsSource\\000.GetDetails.Success")]
        public void NewsSource_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNewsSourceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            NewsSource entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name a2d1f8b6f455476895b89f13998e30e2", entity.Name);
                            Assert.AreEqual("Url a2d1f8b6f455476895b89f13998e30e2", entity.Url);
                            Assert.AreEqual(true, entity.IsActive);
                      }

        [Test]
        public void NewsSource_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareNewsSourceDal("DALInitParams");

            NewsSource entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("NewsSource\\010.Delete.Success")]
        public void NewsSource_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNewsSourceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void NewsSource_Delete_InvalidId()
        {
            var dal = PrepareNewsSourceDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("NewsSource\\020.Insert.Success")]
        public void NewsSource_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareNewsSourceDal("DALInitParams");

            var entity = new NewsSource();
                          entity.Name = "Name 387f6143c36e42d69679036f873fc610";
                            entity.Url = "Url 387f6143c36e42d69679036f873fc610";
                            entity.IsActive = true;              
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 387f6143c36e42d69679036f873fc610", entity.Name);
                            Assert.AreEqual("Url 387f6143c36e42d69679036f873fc610", entity.Url);
                            Assert.AreEqual(true, entity.IsActive);
              
        }

        [TestCase("NewsSource\\030.Update.Success")]
        public void NewsSource_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareNewsSourceDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            NewsSource entity = dal.Get(paramID);

                          entity.Name = "Name 969db972a797442388be46b2adeaaac6";
                            entity.Url = "Url 969db972a797442388be46b2adeaaac6";
                            entity.IsActive = true;              
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 969db972a797442388be46b2adeaaac6", entity.Name);
                            Assert.AreEqual("Url 969db972a797442388be46b2adeaaac6", entity.Url);
                            Assert.AreEqual(true, entity.IsActive);
              
        }

        [Test]
        public void NewsSource_Update_InvalidId()
        {
            var dal = PrepareNewsSourceDal("DALInitParams");

            var entity = new NewsSource();
                          entity.Name = "Name 969db972a797442388be46b2adeaaac6";
                            entity.Url = "Url 969db972a797442388be46b2adeaaac6";
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


        protected INewsSourceDal PrepareNewsSourceDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            INewsSourceDal dal = new NewsSourceDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
