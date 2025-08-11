


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Dals
{
    class ArticleDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IArticleDal))]
    public class ArticleDal : IArticleDal
    {
        NewsAnalysisContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ArticleDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.Articles.Find(ID);
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


        public DMFX.NewsAnalysis.Interfaces.Entities.Article Get(System.Int64? ID)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article result = null;
            var entity = dbContext.Articles.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.ArticleConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> GetAll()
        {
            var entities = dbContext.Articles.ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> result = ToList(entities);
            
            return result;
        }

                public IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> GetByNewsSourceID(System.Int64 NewsSourceID)
        {
            var entities = dbContext.Articles.Where(e => e.NewsSourceID == NewsSourceID).ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new NewsAnalysisContext(initParams.Parameters["ConnectionString"]);
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.Article Insert(DMFX.NewsAnalysis.Interfaces.Entities.Article entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article result = null;
            var efEntity = Convertors.ArticleConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<DMFX.NewsAnalysis.DAL.EF.Models.Article>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.ArticleConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.Article Update(DMFX.NewsAnalysis.Interfaces.Entities.Article entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Article result = null;
            var efEntity = dbContext.Articles.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Title = entity.Title;
						efEntity.Content = entity.Content;
						efEntity.Timestamp = entity.Timestamp;
						efEntity.NewsSourceID = entity.NewsSourceID;
		                dbContext.SaveChanges();

                efEntity = dbContext.Articles.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.ArticleConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> ToList(IList<DMFX.NewsAnalysis.DAL.EF.Models.Article> entities)
        {
            IList<DMFX.NewsAnalysis.Interfaces.Entities.Article> result = new List<DMFX.NewsAnalysis.Interfaces.Entities.Article>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.ArticleConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}