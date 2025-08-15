using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Parser.Common;
using DMFX.NewsAnalysis.Parser.OilPrice;
using HtmlAgilityPack;
using System.ComponentModel.Composition;
using System.Xml;

namespace DMFX.NewsAnalysis.Parser.OilPrice
{
    [Export("OilPrice.com", typeof(IArticleParser))]
    public class ArticleParser : IArticleParser
    {
        public Article Parse(string rawContent)
        {
            var json = ExtractJson(rawContent);

            var article = new Article
            {
                Title = json.Headline,
                Content = json.ArticleBody,
                Timestamp = DateTime.Now,
                Url = json.URL,
                NewsTime = json.DateModified
            };

            return article;
        }

        #region Support methods
        private ArticleContentJson ExtractJson(string rawContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(rawContent);

            var root = doc.DocumentNode;
            if (root != null)
            {
                var node = root.SelectSingleNode("//script[@type='application/ld+json']");
                if (node != null)
                {
                    var rawJsonText = node.InnerText.Trim();
                    if (!string.IsNullOrEmpty(rawJsonText))
                    {
                        var json = System.Text.Json.JsonSerializer.Deserialize<ArticleContentJson>(rawJsonText);
                        return json;
                    }
                }
            }
            throw new ArgumentException("No news content found - json either not present or empty");

        }
        #endregion
    }
}
