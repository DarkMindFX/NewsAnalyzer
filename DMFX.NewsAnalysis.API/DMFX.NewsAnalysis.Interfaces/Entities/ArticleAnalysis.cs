

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Interfaces.Entities
{
    public class ArticleAnalysis 
    {
				public System.Int64? ID { get; set; }

				public System.DateTime Timestamp { get; set; }

				public System.Int64 ArticleID { get; set; }

				public System.Int64 SentimentID { get; set; }

				public System.Int64 AnalyzerID { get; set; }

				
    }
}
