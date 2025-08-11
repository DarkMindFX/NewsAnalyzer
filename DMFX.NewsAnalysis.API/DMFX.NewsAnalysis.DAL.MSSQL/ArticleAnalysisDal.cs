


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
    class ArticleAnalysisDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IArticleAnalysisDal))]
    public class ArticleAnalysisDal: SQLDal, IArticleAnalysisDal
    {
        public IInitParams CreateInitParams()
        {
            return new ArticleAnalysisDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ArticleAnalysis Get(System.Int64? ID)
        {
            ArticleAnalysis result = default(ArticleAnalysis);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ArticleAnalysis_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ArticleAnalysisFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ArticleAnalysis_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<ArticleAnalysis> GetByArticleID(System.Int64 ArticleID)
        {
            var entitiesOut = base.GetBy<ArticleAnalysis, System.Int64>("p_ArticleAnalysis_GetByArticleID", ArticleID, "@ArticleID", SqlDbType.BigInt, 0, ArticleAnalysisFromRow);

            return entitiesOut;
        }
                public IList<ArticleAnalysis> GetBySentimentID(System.Int64 SentimentID)
        {
            var entitiesOut = base.GetBy<ArticleAnalysis, System.Int64>("p_ArticleAnalysis_GetBySentimentID", SentimentID, "@SentimentID", SqlDbType.BigInt, 0, ArticleAnalysisFromRow);

            return entitiesOut;
        }
                public IList<ArticleAnalysis> GetByAnalyzerID(System.Int64 AnalyzerID)
        {
            var entitiesOut = base.GetBy<ArticleAnalysis, System.Int64>("p_ArticleAnalysis_GetByAnalyzerID", AnalyzerID, "@AnalyzerID", SqlDbType.BigInt, 0, ArticleAnalysisFromRow);

            return entitiesOut;
        }
        
        public IList<ArticleAnalysis> GetAll()
        {
            IList<ArticleAnalysis> result = base.GetAll<ArticleAnalysis>("p_ArticleAnalysis_GetAll", ArticleAnalysisFromRow);

            return result;
        }

        public ArticleAnalysis Insert(ArticleAnalysis entity) 
        {
            ArticleAnalysis entityOut = base.Upsert<ArticleAnalysis>("p_ArticleAnalysis_Insert", entity, AddUpsertParameters, ArticleAnalysisFromRow);

            return entityOut;
        }

        public ArticleAnalysis Update(ArticleAnalysis entity) 
        {
            ArticleAnalysis entityOut = base.Upsert<ArticleAnalysis>("p_ArticleAnalysis_Update", entity, AddUpsertParameters, ArticleAnalysisFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ArticleAnalysis entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pTimestamp = new SqlParameter("@Timestamp", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "Timestamp", DataRowVersion.Current, (object)entity.Timestamp != null ? (object)entity.Timestamp : DBNull.Value);   cmd.Parameters.Add(pTimestamp); 
                SqlParameter pArticleID = new SqlParameter("@ArticleID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ArticleID", DataRowVersion.Current, (object)entity.ArticleID != null ? (object)entity.ArticleID : DBNull.Value);   cmd.Parameters.Add(pArticleID); 
                SqlParameter pSentimentID = new SqlParameter("@SentimentID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SentimentID", DataRowVersion.Current, (object)entity.SentimentID != null ? (object)entity.SentimentID : DBNull.Value);   cmd.Parameters.Add(pSentimentID); 
                SqlParameter pAnalyzerID = new SqlParameter("@AnalyzerID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AnalyzerID", DataRowVersion.Current, (object)entity.AnalyzerID != null ? (object)entity.AnalyzerID : DBNull.Value);   cmd.Parameters.Add(pAnalyzerID); 
        
            return cmd;
        }

        protected ArticleAnalysis ArticleAnalysisFromRow(DataRow row)
        {
            var entity = new ArticleAnalysis();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Timestamp = !DBNull.Value.Equals(row["Timestamp"]) ? (System.DateTime)row["Timestamp"] : default(System.DateTime);
                    entity.ArticleID = !DBNull.Value.Equals(row["ArticleID"]) ? (System.Int64)row["ArticleID"] : default(System.Int64);
                    entity.SentimentID = !DBNull.Value.Equals(row["SentimentID"]) ? (System.Int64)row["SentimentID"] : default(System.Int64);
                    entity.AnalyzerID = !DBNull.Value.Equals(row["AnalyzerID"]) ? (System.Int64)row["AnalyzerID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
