


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Convertors
{
	public class ArticleConvertor
	{

		public static DMFX.NewsAnalysis.Interfaces.Entities.Article FromEFEntity(DMFX.NewsAnalysis.DAL.EF.Models.Article efEntity)
        {
			DMFX.NewsAnalysis.Interfaces.Entities.Article result = new Interfaces.Entities.Article()
			{
							ID = efEntity.ID,
							Title = efEntity.Title,
							Content = efEntity.Content,
							Timestamp = efEntity.Timestamp,
							NewsSourceID = efEntity.NewsSourceID,
						};

            return result;
        }

		public static DMFX.NewsAnalysis.DAL.EF.Models.Article ToEFEntity(DMFX.NewsAnalysis.Interfaces.Entities.Article entity)
		{
			DMFX.NewsAnalysis.DAL.EF.Models.Article result = new DMFX.NewsAnalysis.DAL.EF.Models.Article()
			{
							ID = entity.ID,
							Title = entity.Title,
							Content = entity.Content,
							Timestamp = entity.Timestamp,
							NewsSourceID = entity.NewsSourceID,
						};

			
			return result;
		}
	}
	
}