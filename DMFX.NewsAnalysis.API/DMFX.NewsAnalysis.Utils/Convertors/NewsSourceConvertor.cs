

using Microsoft.AspNetCore.Mvc;
using System;

namespace DMFX.NewsAnalysis.Utils.Convertors
{
    public class NewsSourceConvertor
    {
        public static DTO.NewsSource Convert(Interfaces.Entities.NewsSource entity, IUrlHelper url)
        {
            var dto = new DTO.NewsSource()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        Url = entity.Url,

				        IsActive = entity.IsActive,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetNewsSource", "newssources", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteNewsSource", "newssources", new { id = dto.ID  }), "delete_newssource", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertNewsSource", "newssources"), "insert_newssource", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateNewsSource", "newssources"), "update_newssource", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.NewsSource Convert(DTO.NewsSource dto)
        {
            var entity = new Interfaces.Entities.NewsSource()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        Url = dto.Url,

				        IsActive = dto.IsActive,

				
     
            };

            return entity;
        }
    }
}
