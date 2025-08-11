


using DMFX.NewsAnalysis.DAL.EF.Models;
using DMFX.NewsAnalysis.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.DAL.EF.Convertors
{
	public class ArticleAnalysisConvertor
	{

		public static DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis FromEFEntity(DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis efEntity)
        {
			DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis result = new Interfaces.Entities.ArticleAnalysis()
			{
							ID = efEntity.ID,
							Timestamp = efEntity.Timestamp,
							ArticleID = efEntity.ArticleID,
							SentimentID = efEntity.SentimentID,
							AnalyzerID = efEntity.AnalyzerID,
						};

            return result;
        }

		public static DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis ToEFEntity(DMFX.NewsAnalysis.Interfaces.Entities.ArticleAnalysis entity)
		{
			DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis result = new DMFX.NewsAnalysis.DAL.EF.Models.ArticleAnalysis()
			{
							ID = entity.ID,
							Timestamp = entity.Timestamp,
							ArticleID = entity.ArticleID,
							SentimentID = entity.SentimentID,
							AnalyzerID = entity.AnalyzerID,
						};

			
			return result;
		}
	}
	
}