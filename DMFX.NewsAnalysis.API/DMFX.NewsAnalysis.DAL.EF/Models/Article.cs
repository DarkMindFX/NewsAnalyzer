

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DMFX.NewsAnalysis.DAL.EF.Models
{
    public partial class Article
    {
        public Article()
        {
                    ArticleAnalysises = new HashSet<ArticleAnalysis>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Title { get; set; }
				public System.String Content { get; set; }
				public System.DateTime Timestamp { get; set; }
		
		[ForeignKey("FK_Articale_NewsSource")]
				public System.Int64 NewsSourceID { get; set; }
			


                public virtual NewsSource NewsSource { get; set; }
        
                public virtual ICollection<ArticleAnalysis> ArticleAnalysises { get; set; }
            }
}