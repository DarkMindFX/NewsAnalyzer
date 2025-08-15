using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Parser.Common
{
    public class SourceCrawlerParams
    {
        public DateTime?    StartDate { get; set; }
        public DateTime?    EndDate { get; set; }
        public ISourcePaginator Paginator { get; set; }
    }

    public interface ISourceCrawler
    {
        public delegate void OnArticleAvailableHandler(object sender, ArticleDetails e);

        public event OnArticleAvailableHandler OnArticleAvailable;
        
        int StartCrawling(SourceCrawlerParams crawlerParams);

        string SourceName { get; }
    }
}
