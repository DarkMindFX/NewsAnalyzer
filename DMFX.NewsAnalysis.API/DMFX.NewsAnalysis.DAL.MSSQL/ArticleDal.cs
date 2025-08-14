
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
    class ArticleDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IArticleDal))]
    public class ArticleDal: SQLDal, IArticleDal
    {
        public IInitParams CreateInitParams()
        {
            return new ArticleDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Article Get(System.Int64? ID)
        {
            Article result = default(Article);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Article_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ArticleFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Article_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<Article> GetByNewsSourceID(System.Int64 NewsSourceID)
        {
            var entitiesOut = base.GetBy<Article, System.Int64>("p_Article_GetByNewsSourceID", NewsSourceID, "@NewsSourceID", SqlDbType.BigInt, 0, ArticleFromRow);

            return entitiesOut;
        }
        
        public IList<Article> GetAll()
        {
            IList<Article> result = base.GetAll<Article>("p_Article_GetAll", ArticleFromRow);

            return result;
        }

        public Article Insert(Article entity) 
        {
            Article entityOut = base.Upsert<Article>("p_Article_Insert", entity, AddUpsertParameters, ArticleFromRow);

            return entityOut;
        }

        public Article Update(Article entity) 
        {
            Article entityOut = base.Upsert<Article>("p_Article_Update", entity, AddUpsertParameters, ArticleFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Article entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pTitle = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Current, (object)entity.Title != null ? (object)entity.Title : DBNull.Value);   cmd.Parameters.Add(pTitle); 
                SqlParameter pContent = new SqlParameter("@Content", System.Data.SqlDbType.NVarChar, 4000, ParameterDirection.Input, false, 0, 0, "Content", DataRowVersion.Current, (object)entity.Content != null ? (object)entity.Content : DBNull.Value);   cmd.Parameters.Add(pContent); 
                SqlParameter pTimestamp = new SqlParameter("@Timestamp", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "Timestamp", DataRowVersion.Current, (object)entity.Timestamp != null ? (object)entity.Timestamp : DBNull.Value);   cmd.Parameters.Add(pTimestamp); 
                SqlParameter pNewsSourceID = new SqlParameter("@NewsSourceID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "NewsSourceID", DataRowVersion.Current, (object)entity.NewsSourceID != null ? (object)entity.NewsSourceID : DBNull.Value);   cmd.Parameters.Add(pNewsSourceID); 
                SqlParameter pUrl = new SqlParameter("@Url", System.Data.SqlDbType.NVarChar, 512, ParameterDirection.Input, false, 0, 0, "Url", DataRowVersion.Current, (object)entity.Url != null ? (object)entity.Url : DBNull.Value);   cmd.Parameters.Add(pUrl); 
                SqlParameter pNewsTime = new SqlParameter("@NewsTime", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "NewsTime", DataRowVersion.Current, (object)entity.NewsTime != null ? (object)entity.NewsTime : DBNull.Value);   cmd.Parameters.Add(pNewsTime); 
        
            return cmd;
        }

        protected Article ArticleFromRow(DataRow row)
        {
            var entity = new Article();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Title = !DBNull.Value.Equals(row["Title"]) ? (System.String)row["Title"] : default(System.String);
                    entity.Content = !DBNull.Value.Equals(row["Content"]) ? (System.String)row["Content"] : default(System.String);
                    entity.Timestamp = !DBNull.Value.Equals(row["Timestamp"]) ? (System.DateTime)row["Timestamp"] : default(System.DateTime);
                    entity.NewsSourceID = !DBNull.Value.Equals(row["NewsSourceID"]) ? (System.Int64)row["NewsSourceID"] : default(System.Int64);
                    entity.Url = !DBNull.Value.Equals(row["Url"]) ? (System.String)row["Url"] : default(System.String);
                    entity.NewsTime = !DBNull.Value.Equals(row["NewsTime"]) ? (System.DateTime)row["NewsTime"] : default(System.DateTime);
        
            return entity;
        }
        
    }
}
