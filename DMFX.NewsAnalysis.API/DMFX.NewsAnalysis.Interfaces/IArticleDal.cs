


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Interfaces;

namespace DMFX.NewsAnalysis.Interfaces
{
    public interface IArticleDal : IDalBase<Article>
    {
        Article Get(System.Int64? ID);

        Article GetByUrl(System.String Url);

        bool Delete(System.Int64? ID);

        IList<Article> GetByNewsSourceID(System.Int64 NewsSourceID);

    }
}

