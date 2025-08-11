

using Microsoft.AspNetCore.Mvc;
using System;

namespace DMFX.NewsAnalysis.Utils.Convertors
{
    public class AnalyzerConvertor
    {
        public static DTO.Analyzer Convert(Interfaces.Entities.Analyzer entity, IUrlHelper url)
        {
            var dto = new DTO.Analyzer()
            {
        		        ID = entity.ID,

				        Name = entity.Name,

				        IsActive = entity.IsActive,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetAnalyzer", "analyzers", new { id = dto.ID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteAnalyzer", "analyzers", new { id = dto.ID  }), "delete_analyzer", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertAnalyzer", "analyzers"), "insert_analyzer", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateAnalyzer", "analyzers"), "update_analyzer", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.Analyzer Convert(DTO.Analyzer dto)
        {
            var entity = new Interfaces.Entities.Analyzer()
            {
                
        		        ID = dto.ID,

				        Name = dto.Name,

				        IsActive = dto.IsActive,

				
     
            };

            return entity;
        }
    }
}
