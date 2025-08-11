


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMFX.NewsAnalysis.Interfaces.Entities;
using DMFX.NewsAnalysis.Interfaces;

namespace DMFX.NewsAnalysis.Interfaces
{
    public interface INewsSourceDal : IDalBase<NewsSource>
    {
        NewsSource Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        
            }
}

