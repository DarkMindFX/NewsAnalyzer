


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using Microsoft.Data.SqlClient;
using DMFX.NewsAnalysis.Common;
using DMFX.NewsAnalysis.DAL.MSSQL;
using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.Interfaces.Entities;

namespace DMFX.NewsAnalysis.DAL.MSSQL 
{
    class NewsSourceDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(INewsSourceDal))]
    public class NewsSourceDal: SQLDal, INewsSourceDal
    {
        public IInitParams CreateInitParams()
        {
            return new NewsSourceDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public NewsSource Get(System.Int64? ID)
        {
            NewsSource result = default(NewsSource);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_NewsSource_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = NewsSourceFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_NewsSource_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<NewsSource> GetAll()
        {
            IList<NewsSource> result = base.GetAll<NewsSource>("p_NewsSource_GetAll", NewsSourceFromRow);

            return result;
        }

        public NewsSource Insert(NewsSource entity) 
        {
            NewsSource entityOut = base.Upsert<NewsSource>("p_NewsSource_Insert", entity, AddUpsertParameters, NewsSourceFromRow);

            return entityOut;
        }

        public NewsSource Update(NewsSource entity) 
        {
            NewsSource entityOut = base.Upsert<NewsSource>("p_NewsSource_Update", entity, AddUpsertParameters, NewsSourceFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, NewsSource entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
                SqlParameter pUrl = new SqlParameter("@Url", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Url", DataRowVersion.Current, (object)entity.Url != null ? (object)entity.Url : DBNull.Value);   cmd.Parameters.Add(pUrl); 
                SqlParameter pIsActive = new SqlParameter("@IsActive", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Current, (object)entity.IsActive != null ? (object)entity.IsActive : DBNull.Value);   cmd.Parameters.Add(pIsActive); 
        
            return cmd;
        }

        protected NewsSource NewsSourceFromRow(DataRow row)
        {
            var entity = new NewsSource();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
                    entity.Url = !DBNull.Value.Equals(row["Url"]) ? (System.String)row["Url"] : default(System.String);
                    entity.IsActive = !DBNull.Value.Equals(row["IsActive"]) ? (System.Boolean)row["IsActive"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
