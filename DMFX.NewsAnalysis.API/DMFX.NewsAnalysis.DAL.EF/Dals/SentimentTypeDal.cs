


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Dals
{
    class SentimentTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(ISentimentTypeDal))]
    public class SentimentTypeDal : ISentimentTypeDal
    {
        NewsAnalysisContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new SentimentTypeDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.SentimentTypes.Find(ID);
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


        public DMFX.NewsAnalysis.Interfaces.Entities.SentimentType Get(System.Int64? ID)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType result = null;
            var entity = dbContext.SentimentTypes.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.SentimentTypeConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<DMFX.NewsAnalysis.Interfaces.Entities.SentimentType> GetAll()
        {
            var entities = dbContext.SentimentTypes.ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.SentimentType> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new NewsAnalysisContext(initParams.Parameters["ConnectionString"]);
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.SentimentType Insert(DMFX.NewsAnalysis.Interfaces.Entities.SentimentType entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType result = null;
            var efEntity = Convertors.SentimentTypeConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<DMFX.NewsAnalysis.DAL.EF.Models.SentimentType>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.SentimentTypeConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.SentimentType Update(DMFX.NewsAnalysis.Interfaces.Entities.SentimentType entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.SentimentType result = null;
            var efEntity = dbContext.SentimentTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Name = entity.Name;
		                dbContext.SaveChanges();

                efEntity = dbContext.SentimentTypes.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.SentimentTypeConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<DMFX.NewsAnalysis.Interfaces.Entities.SentimentType> ToList(IList<DMFX.NewsAnalysis.DAL.EF.Models.SentimentType> entities)
        {
            IList<DMFX.NewsAnalysis.Interfaces.Entities.SentimentType> result = new List<DMFX.NewsAnalysis.Interfaces.Entities.SentimentType>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.SentimentTypeConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}