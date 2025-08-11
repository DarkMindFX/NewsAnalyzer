

using Microsoft.AspNetCore.Mvc;
using System;

namespace DMFX.NewsAnalysis.Utils.Convertors
{
    public class ArticleAnalysisConvertor
    {
        public static DTO.ArticleAnalysis Convert(Interfaces.Entities.ArticleAnalysis entity, IUrlHelper url)
        {
            var dto = new DTO.ArticleAnalysis()
            {
        		        ID = entity.ID,

				        Timestamp = entity.Timestamp,

				        ArticleID = entity.ArticleID,

				        SentimentID = entity.SentimentID,

				        AnalyzerID = entity.AnalyzerID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetArticleAnalysis", "articleanalysises", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteArticleAnalysis", "articleanalysises", new { id = dto.ID  }), "delete_articleanalysis", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertArticleAnalysis", "articleanalysises"), "insert_articleanalysis", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateArticleAnalysis", "articleanalysises"), "update_articleanalysis", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.ArticleAnalysis Convert(DTO.ArticleAnalysis dto)
        {
            var entity = new Interfaces.Entities.ArticleAnalysis()
            {
                
        		        ID = dto.ID,

				        Timestamp = dto.Timestamp,

				        ArticleID = dto.ArticleID,

				        SentimentID = dto.SentimentID,

				        AnalyzerID = dto.AnalyzerID,

				
     
            };

            return entity;
        }
    }
}
