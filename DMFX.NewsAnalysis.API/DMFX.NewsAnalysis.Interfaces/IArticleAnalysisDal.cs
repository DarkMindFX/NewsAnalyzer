


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Interfaces;

namespace DMFX.NewsAnalysis.Interfaces
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

