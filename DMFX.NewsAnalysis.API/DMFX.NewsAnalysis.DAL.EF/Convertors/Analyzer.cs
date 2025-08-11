


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Convertors
{
	public class AnalyzerConvertor
	{

		public static DMFX.NewsAnalysis.Interfaces.Entities.Analyzer FromEFEntity(DMFX.NewsAnalysis.DAL.EF.Models.Analyzer efEntity)
        {
			DMFX.NewsAnalysis.Interfaces.Entities.Analyzer result = new Interfaces.Entities.Analyzer()
			{
							ID = efEntity.ID,
							Name = efEntity.Name,
							IsActive = efEntity.IsActive,
						};

            return result;
        }

		public static DMFX.NewsAnalysis.DAL.EF.Models.Analyzer ToEFEntity(DMFX.NewsAnalysis.Interfaces.Entities.Analyzer entity)
		{
			DMFX.NewsAnalysis.DAL.EF.Models.Analyzer result = new DMFX.NewsAnalysis.DAL.EF.Models.Analyzer()
			{
							ID = entity.ID,
							Name = entity.Name,
							IsActive = entity.IsActive,
						};

			
			return result;
		}
	}
	
}