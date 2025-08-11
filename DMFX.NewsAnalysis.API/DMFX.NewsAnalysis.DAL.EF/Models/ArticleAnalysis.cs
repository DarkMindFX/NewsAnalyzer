

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DMFX.NewsAnalysis.DAL.EF.Models
{
    public partial class ArticleAnalysis
    {
        public ArticleAnalysis()
        {
                }

        
		[Key]
				public System.Int64? ID { get; set; }
				public System.DateTime Timestamp { get; set; }
		
		[ForeignKey("FK_ArticleAnalysis_Articale")]
				public System.Int64 ArticleID { get; set; }
		
		[ForeignKey("FK_ArticleAnalysis_SentimentType")]
				public System.Int64 SentimentID { get; set; }
		
		[ForeignKey("FK_ArticleAnalysis_Analyzer")]
				public System.Int64 AnalyzerID { get; set; }
			


                public virtual Article Article { get; set; }
                public virtual SentimentType Sentiment { get; set; }
                public virtual Analyzer Analyzer { get; set; }
        
            }
}