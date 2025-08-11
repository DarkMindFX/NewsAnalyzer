

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DMFX.NewsAnalysis.DAL.EF.Models
{
    public partial class NewsSource
    {
        public NewsSource()
        {
                    Articles = new HashSet<Article>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Name { get; set; }
				public System.String Url { get; set; }
				public System.Boolean IsActive { get; set; }
			


        
                public virtual ICollection<Article> Articles { get; set; }
            }
}