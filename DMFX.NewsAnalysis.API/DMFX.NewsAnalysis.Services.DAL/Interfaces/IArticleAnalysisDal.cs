


using DMFX.NewsAnalysis.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMFX.NewsAnalysis.Services.Dal
{
    public interface IArticleAnalysisDal : IDalBase<ArticleAnalysis>
    {
        ArticleAnalysis Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<ArticleAnalysis> GetByArticleID(System.Int64 ArticleID);
            IList<ArticleAnalysis> GetBySentimentID(System.Int64 SentimentID);
            IList<ArticleAnalysis> GetByAnalyzerID(System.Int64 AnalyzerID);
    
        }
}
