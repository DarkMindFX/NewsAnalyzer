using DMFX.NewsAnalysis.Parser.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Parser.OilPrice
{
    public class SourcePaginator : ISourcePaginator
    {
        private readonly string _paginationUrl = "https://oilprice.com/Latest-Energy-News/World-News/Page-{0}.html";
        private int _currentPage = 1;
        private int _pagesCount = 0;
        public void Initialize()
        {
            _currentPage = 1;
        }
        public string GetNextPageUrl()
        {
            return string.Format(_paginationUrl, _currentPage++);
        }

        public bool HasNextPage
        {
            get
            {
                return _currentPage < TotalPagesCount;
            }
        }

        public void Reset()
        {
            _currentPage = 1;
        }

        public int TotalPagesCount
        {
            get
            {
                if (_pagesCount == 0)
                {
                    var html = string.Format(_paginationUrl, 1);

                    HtmlWeb web = new HtmlWeb();
                    var doc = web.Load(html);
                    if (doc == null || doc.DocumentNode == null)
                    {
                        throw new InvalidOperationException("Failed to load the document or document node is null.");
                    }
                    _pagesCount = GetPageCountFromDoc(doc);
                }
                return _pagesCount;
            }
        }

        #region Support methods
        protected int GetPageCountFromDoc(HtmlDocument doc)
        {
            int numPages = 0;
            var root = doc.DocumentNode;
            if (root != null)
            {
                var node = root.SelectSingleNode("//span[@class='num_pages']");
                if (node != null)
                {
                    string innerText = node.InnerText.Trim().TrimStart('/');
                    Int32.TryParse(innerText, out numPages);
                }
            }

            return numPages;
        }


        #endregion  
    }
}
