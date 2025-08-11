


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
    class AnalyzerDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IAnalyzerDal))]
    public class AnalyzerDal: SQLDal, IAnalyzerDal
    {
        public IInitParams CreateInitParams()
        {
            return new AnalyzerDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Analyzer Get(System.Int64? ID)
        {
            Analyzer result = default(Analyzer);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Analyzer_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = AnalyzerFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Analyzer_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<Analyzer> GetAll()
        {
            IList<Analyzer> result = base.GetAll<Analyzer>("p_Analyzer_GetAll", AnalyzerFromRow);

            return result;
        }

        public Analyzer Insert(Analyzer entity) 
        {
            Analyzer entityOut = base.Upsert<Analyzer>("p_Analyzer_Insert", entity, AddUpsertParameters, AnalyzerFromRow);

            return entityOut;
        }

        public Analyzer Update(Analyzer entity) 
        {
            Analyzer entityOut = base.Upsert<Analyzer>("p_Analyzer_Update", entity, AddUpsertParameters, AnalyzerFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Analyzer entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 255, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
                SqlParameter pIsActive = new SqlParameter("@IsActive", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Current, (object)entity.IsActive != null ? (object)entity.IsActive : DBNull.Value);   cmd.Parameters.Add(pIsActive); 
        
            return cmd;
        }

        protected Analyzer AnalyzerFromRow(DataRow row)
        {
            var entity = new Analyzer();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
                    entity.IsActive = !DBNull.Value.Equals(row["IsActive"]) ? (System.Boolean)row["IsActive"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
