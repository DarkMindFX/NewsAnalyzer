

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DMFX.NewsAnalysis.DAL.EF.Models
{
    public partial class SentimentType
    {
        public SentimentType()
        {
                    ArticleAnalysises = new HashSet<ArticleAnalysis>();
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.String Name { get; set; }
			


        
                public virtual ICollection<ArticleAnalysis> ArticleAnalysises { get; set; }
            }
}