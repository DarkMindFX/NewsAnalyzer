

using Microsoft.AspNetCore.Mvc;
using System;

namespace DMFX.NewsAnalysis.Utils.Convertors
{
    public class ArticleConvertor
    {
        public static DTO.Article Convert(Interfaces.Entities.Article entity, IUrlHelper url)
        {
            var dto = new DTO.Article()
            {
        		        ID = entity.ID,

				        Title = entity.Title,

				        Content = entity.Content,

				        Timestamp = entity.Timestamp,

				        NewsSourceID = entity.NewsSourceID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetArticle", "articles", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteArticle", "articles", new { id = dto.ID  }), "delete_article", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertArticle", "articles"), "insert_article", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateArticle", "articles"), "update_article", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Article Convert(DTO.Article dto)
        {
            var entity = new Interfaces.Entities.Article()
            {
                
        		        ID = dto.ID,

				        Title = dto.Title,

				        Content = dto.Content,

				        Timestamp = dto.Timestamp,

				        NewsSourceID = dto.NewsSourceID,

				
     
            };

            return entity;
        }
    }
}
