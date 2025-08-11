

using DMFX.NewsAnalysis.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.Services.Dal
{
    [Export(typeof(INewsSourceDal))]
    public class NewsSourceDal : DalBaseImpl<NewsSource, Interfaces.INewsSourceDal>, INewsSourceDal
    {

        public NewsSourceDal(Interfaces.INewsSourceDal dalImpl) : base(dalImpl)
        {
        }

        public NewsSource Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
