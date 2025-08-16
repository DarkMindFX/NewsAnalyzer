using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Parser.Common
{
    public class ArticleDetails
    {
        public string URL { get; set; }

        public string Source { get; set; }

        public DateTime? PublishedDate { get; set; }
    }
}
