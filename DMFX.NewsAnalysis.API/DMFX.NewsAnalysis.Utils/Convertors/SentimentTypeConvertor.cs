

using Microsoft.AspNetCore.Mvc;
using System;

namespace DMFX.NewsAnalysis.Utils.Convertors
{
    public class SentimentTypeConvertor
    {
        public static DTO.SentimentType Convert(Interfaces.Entities.SentimentType entity, IUrlHelper url)
        {
            var dto = new DTO.SentimentType()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetSentimentType", "sentimenttypes", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteSentimentType", "sentimenttypes", new { id = dto.ID  }), "delete_sentimenttype", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertSentimentType", "sentimenttypes"), "insert_sentimenttype", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateSentimentType", "sentimenttypes"), "update_sentimenttype", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.SentimentType Convert(DTO.SentimentType dto)
        {
            var entity = new Interfaces.Entities.SentimentType()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				
     
            };

            return entity;
        }
    }
}
