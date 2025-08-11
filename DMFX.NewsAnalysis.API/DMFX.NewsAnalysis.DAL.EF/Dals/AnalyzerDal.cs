


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Dals
{
    class AnalyzerDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IAnalyzerDal))]
    public class AnalyzerDal : IAnalyzerDal
    {
        NewsAnalysisContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new AnalyzerDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.Analyzers.Find(ID);
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


        public DMFX.NewsAnalysis.Interfaces.Entities.Analyzer Get(System.Int64? ID)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer result = null;
            var entity = dbContext.Analyzers.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.AnalyzerConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<DMFX.NewsAnalysis.Interfaces.Entities.Analyzer> GetAll()
        {
            var entities = dbContext.Analyzers.ToList();

            IList<DMFX.NewsAnalysis.Interfaces.Entities.Analyzer> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new NewsAnalysisContext(initParams.Parameters["ConnectionString"]);
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.Analyzer Insert(DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer result = null;
            var efEntity = Convertors.AnalyzerConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<DMFX.NewsAnalysis.DAL.EF.Models.Analyzer>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.AnalyzerConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public DMFX.NewsAnalysis.Interfaces.Entities.Analyzer Update(DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity)
        {
            DMFX.NewsAnalysis.Interfaces.Entities.Analyzer result = null;
            var efEntity = dbContext.Analyzers.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Name = entity.Name;
						efEntity.IsActive = entity.IsActive;
		                dbContext.SaveChanges();

                efEntity = dbContext.Analyzers.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.AnalyzerConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<DMFX.NewsAnalysis.Interfaces.Entities.Analyzer> ToList(IList<DMFX.NewsAnalysis.DAL.EF.Models.Analyzer> entities)
        {
            IList<DMFX.NewsAnalysis.Interfaces.Entities.Analyzer> result = new List<DMFX.NewsAnalysis.Interfaces.Entities.Analyzer>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.AnalyzerConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}