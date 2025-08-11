


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Convertors
{
	public class SentimentTypeConvertor
	{

		public static DMFX.NewsAnalysis.Interfaces.Entities.SentimentType FromEFEntity(DMFX.NewsAnalysis.DAL.EF.Models.SentimentType efEntity)
        {
			DMFX.NewsAnalysis.Interfaces.Entities.SentimentType result = new Interfaces.Entities.SentimentType()
			{
							ID = efEntity.ID,
							Name = efEntity.Name,
						};

            return result;
        }

		public static DMFX.NewsAnalysis.DAL.EF.Models.SentimentType ToEFEntity(DMFX.NewsAnalysis.Interfaces.Entities.SentimentType entity)
		{
			DMFX.NewsAnalysis.DAL.EF.Models.SentimentType result = new DMFX.NewsAnalysis.DAL.EF.Models.SentimentType()
			{
							ID = entity.ID,
							Name = entity.Name,
						};

			
			return result;
		}
	}
	
}