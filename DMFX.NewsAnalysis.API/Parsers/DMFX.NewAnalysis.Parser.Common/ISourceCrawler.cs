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
        public bool         SkipExisting { get; set; }
    }

    public interface ISourceCrawler
    {
        public delegate void OnArticleAvailableHandler(object sender, ArticleDetails e);

        public event OnArticleAvailableHandler OnArticleAvailable;
        
        void StartCrawling(SourceCrawlerParams crawlerParams);

        string SourceName { get; }
    }
}
