

using DMFX.NewsAnalysis.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.Services.Dal
{
    [Export(typeof(IArticleAnalysisDal))]
    public class ArticleAnalysisDal : DalBaseImpl<ArticleAnalysis, Interfaces.IArticleAnalysisDal>, IArticleAnalysisDal
    {

        public ArticleAnalysisDal(Interfaces.IArticleAnalysisDal dalImpl) : base(dalImpl)
        {
        }

        public ArticleAnalysis Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<ArticleAnalysis> GetByArticleID(System.Int64 ArticleID)
        {
            return _dalImpl.GetByArticleID(ArticleID);
        }
        public IList<ArticleAnalysis> GetBySentimentID(System.Int64 SentimentID)
        {
            return _dalImpl.GetBySentimentID(SentimentID);
        }
        public IList<ArticleAnalysis> GetByAnalyzerID(System.Int64 AnalyzerID)
        {
            return _dalImpl.GetByAnalyzerID(AnalyzerID);
        }
            }
}
