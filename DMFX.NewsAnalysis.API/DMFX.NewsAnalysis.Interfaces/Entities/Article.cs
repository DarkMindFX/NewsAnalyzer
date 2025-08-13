

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Interfaces.Entities
{
    public class Article
    {
        public System.Int64? ID { get; set; }

        public System.String Title { get; set; }

        public System.String Content { get; set; }

        public System.DateTime Timestamp { get; set; }

        public System.Int64 NewsSourceID { get; set; }

        public System.String Url { get; set; }

        public System.DateTime NewsTime { get; set; }


    }
}
