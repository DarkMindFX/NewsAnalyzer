using DMFX.NewsAnalysis.Parser.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DMFX.NewsAnalysis.Parser.Common.ISourceCrawler;

namespace DMFX.NewsAnalysis.Parser.OilPrice
{
    [Export("OilPrice.com", typeof(ISourceCrawler))]
    public class SourceCrawler : ISourceCrawler
    {
        private readonly string _paginationUrl = "https://oilprice.com/Latest-Energy-News/World-News/Page-{0}.html";

        public string SourceName => "OilPrice.com";

        public event OnArticleAvailableHandler OnArticleAvailable;

        public int StartCrawling(SourceCrawlerParams crawlerParams)
        {
            HtmlWeb web = new HtmlWeb();
            int totalArticlesFound = 0;

            while (crawlerParams.Paginator.HasNextPage)
            {
                var page = crawlerParams.Paginator.GetNextPageUrl();
                var doc = web.Load(page);
                // finding all news
                var root = doc.DocumentNode;
                if (root != null)
                {
                    var nodes = root.SelectNodes("//div[@class='categoryArticle__content']");
                    foreach (var node in nodes)
                    {
                        string url = string.Empty;
                        DateTime articelDate = GetArticleDatetime(node);

                        // if article date is within the specified range, extract URL
                        if (articelDate != DateTime.MinValue
                            && (crawlerParams.EndDate == null || crawlerParams.EndDate >= articelDate)
                            && (crawlerParams.StartDate == null || crawlerParams.StartDate <= articelDate))
                        {
                            url = GetArticleUrl(node);

                            if (!string.IsNullOrEmpty(url))
                            {
                                // creating article details
                                var articleDetails = new ArticleDetails
                                {
                                    ULR = url,
                                    Source = SourceName
                                };
                                // raising event
                                OnArticleAvailable?.Invoke(this, articleDetails);
                                ++totalArticlesFound;
                            }

                        }
                    }

                }
            }

            return totalArticlesFound;
        }

        #region Support methods

        protected DateTime GetArticleDatetime(HtmlNode node)
        {
            DateTime result = DateTime.MinValue;
            var dateNode = node.SelectSingleNode(".//p[@class='categoryArticle__meta']");
            if (dateNode != null)
            {
                string dateText = dateNode.InnerText
                                    .Substring(0, dateNode.InnerText.IndexOf("|"))
                                    .Trim()
                                    .Replace("at ", string.Empty);

                DateTime.TryParse(dateText, out result);
            }
            return result;
        }

        protected string GetArticleUrl(HtmlNode node)
        {
            var aHref = node.SelectSingleNode("a");
            if (aHref != null)
            {
                return aHref.GetAttributeValue("href", string.Empty);
            }
            return string.Empty;
        }
        #endregion


    }
}
