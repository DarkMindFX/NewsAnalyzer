


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Convertors
{
	public class NewsSourceConvertor
	{

		public static DMFX.NewsAnalysis.Interfaces.Entities.NewsSource FromEFEntity(DMFX.NewsAnalysis.DAL.EF.Models.NewsSource efEntity)
        {
			DMFX.NewsAnalysis.Interfaces.Entities.NewsSource result = new Interfaces.Entities.NewsSource()
			{
							ID = efEntity.ID,
							Name = efEntity.Name,
							Url = efEntity.Url,
							IsActive = efEntity.IsActive,
						};

            return result;
        }

		public static DMFX.NewsAnalysis.DAL.EF.Models.NewsSource ToEFEntity(DMFX.NewsAnalysis.Interfaces.Entities.NewsSource entity)
		{
			DMFX.NewsAnalysis.DAL.EF.Models.NewsSource result = new DMFX.NewsAnalysis.DAL.EF.Models.NewsSource()
			{
							ID = entity.ID,
							Name = entity.Name,
							Url = entity.Url,
							IsActive = entity.IsActive,
						};

			
			return result;
		}
	}
	
}