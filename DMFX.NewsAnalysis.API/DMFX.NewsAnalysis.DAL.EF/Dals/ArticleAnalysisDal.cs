


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Dals
{
    class ArticleAnalysisDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IArticleAnalysisDal))]
    public class ArticleAnalysisDal : IArticleAnalysisDal
    {
        NewsAnalysisContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ArticleAnalysisDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.ArticleAnalysises.Find(ID);
            if (entity != null)
            {
							dbContext.Remove(entity);
			                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis Get(System.Int64? ID)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis result = null;
            var entity = dbContext.ArticleAnalysises.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.ArticleAnalysisConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> GetAll()
        {
            var entities = dbContext.ArticleAnalysises.ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> result = ToList(entities);
            
            return result;
        }

                public IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> GetByArticleID(System.Int64 ArticleID)
        {
            var entities = dbContext.ArticleAnalysises.Where(e => e.ArticleID == ArticleID).ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> result = ToList(entities);

            return result;
        }
                public IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> GetBySentimentID(System.Int64 SentimentID)
        {
            var entities = dbContext.ArticleAnalysises.Where(e => e.SentimentID == SentimentID).ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> result = ToList(entities);

            return result;
        }
                public IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> GetByAnalyzerID(System.Int64 AnalyzerID)
        {
            var entities = dbContext.ArticleAnalysises.Where(e => e.AnalyzerID == AnalyzerID).ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new NewsAnalysisContext(initParams.Parameters["ConnectionString"]);
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis Insert(DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis result = null;
            var efEntity = Convertors.ArticleAnalysisConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.ArticleAnalysisConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis Update(DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis result = null;
            var efEntity = dbContext.ArticleAnalysises.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Timestamp = entity.Timestamp;
						efEntity.ArticleID = entity.ArticleID;
						efEntity.SentimentID = entity.SentimentID;
						efEntity.AnalyzerID = entity.AnalyzerID;
		                dbContext.SaveChanges();

                efEntity = dbContext.ArticleAnalysises.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.ArticleAnalysisConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> ToList(IList<DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis> entities)
        {
            IList<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis> result = new List<DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.ArticleAnalysisConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}