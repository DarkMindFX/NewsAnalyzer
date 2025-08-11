


using DMFX.NewsAnalysis.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMFX.NewsAnalysis.Services.Dal
{
    public interface IArticleDal : IDalBase<Article>
    {
        Article Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<Article> GetByNewsSourceID(System.Int64 NewsSourceID);
    
        }
}
