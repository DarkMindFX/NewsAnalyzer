


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Dals
{
    class NewsSourceDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(INewsSourceDal))]
    public class NewsSourceDal : INewsSourceDal
    {
        NewsAnalysisContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new NewsSourceDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.NewsSources.Find(ID);
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


        public DMFX.NewsAnalysis.Interfaces.Entities.NewsSource Get(System.Int64? ID)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource result = null;
            var entity = dbContext.NewsSources.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.NewsSourceConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<DMFX.NewsAnalysis.Interfaces.Entities.NewsSource> GetAll()
        {
            var entities = dbContext.NewsSources.ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.NewsSource> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new NewsAnalysisContext(initParams.Parameters["ConnectionString"]);
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.NewsSource Insert(DMFX.NewsAnalysis.Interfaces.Entities.NewsSource entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource result = null;
            var efEntity = Convertors.NewsSourceConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<DMFX.NewsAnalysis.DAL.EF.Models.NewsSource>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.NewsSourceConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.NewsSource Update(DMFX.NewsAnalysis.Interfaces.Entities.NewsSource entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.NewsSource result = null;
            var efEntity = dbContext.NewsSources.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Name = entity.Name;
						efEntity.Url = entity.Url;
						efEntity.IsActive = entity.IsActive;
		                dbContext.SaveChanges();

                efEntity = dbContext.NewsSources.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.NewsSourceConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<DMFX.NewsAnalysis.Interfaces.Entities.NewsSource> ToList(IList<DMFX.NewsAnalysis.DAL.EF.Models.NewsSource> entities)
        {
            IList<DMFX.NewsAnalysis.Interfaces.Entities.NewsSource> result = new List<DMFX.NewsAnalysis.Interfaces.Entities.NewsSource>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.NewsSourceConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}