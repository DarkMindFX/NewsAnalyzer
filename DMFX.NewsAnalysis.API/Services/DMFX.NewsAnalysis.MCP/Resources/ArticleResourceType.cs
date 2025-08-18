using DMFX.NewsAnalysis.DTO;
using DMFX.NewsAnalysis.Interfaces;
using DMFX.NewsAnalysis.Utils.Convertors;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace DMFX.NewsAnalysis.MCP.Resources
{

    [McpServerResourceType]
    public class ArticleResourceType
    {

        [McpServerResource]
        [Description("Article with given id")]
        public static ResourceContents SingleArticle(RequestContext<ReadResourceRequestParams> requestContext,
                                                        IArticleDal articleDal,
                                                        int id)
        {
            var resource = articleDal.Get(id);

            if (resource != null)
            {
                var dto = ArticleConvertor.Convert(resource, null);

                var response = new TextResourceContents
                {
                    Text = JsonSerializer.Serialize(dto),
                    MimeType = "text/plain",
                    Uri = resource.Url,
                };

                return response;
            }
            else
            {
                throw new ArgumentException($"Unknown Article resource with ID = {id}");
            }
        }

        [McpServerResource]
        [Description("List of Articles ID's published within given date range")]
        public static ResourceContents ArticlesByDateRange(RequestContext<ReadResourceRequestParams> requestContext,
                                                        IArticleDal articleDal,
                                                        DateTime dtStart,
                                                        DateTime dtEnd)
        {
            var resources = articleDal.GetAll().Where(a => a.NewsTime >= dtStart && a.NewsTime <= dtEnd);


            var articleInfos = new List<ArticleInfo>();
            foreach (var resource in resources)
            {
                articleInfos.Add(new ArticleInfo
                {
                    ID = (long)resource.ID,
                    Title = resource.Title,
                    PublishedDateTime = resource.NewsTime
                });
            }

            var response = new TextResourceContents
            {
                Text = JsonSerializer.Serialize(articleInfos),
                MimeType = "text/plain",
                Uri = null,
            };

            return response;

        }
    }
}
