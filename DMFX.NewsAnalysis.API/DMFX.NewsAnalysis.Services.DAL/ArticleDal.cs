

using DMFX.NewsAnalysis.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.Services.Dal
{
    [Export(typeof(IArticleDal))]
    public class ArticleDal : DalBaseImpl<Article, Interfaces.IArticleDal>, IArticleDal
    {

        public ArticleDal(Interfaces.IArticleDal dalImpl) : base(dalImpl)
        {
        }

        public Article Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<Article> GetByNewsSourceID(System.Int64 NewsSourceID)
        {
            return _dalImpl.GetByNewsSourceID(NewsSourceID);
        }
            }
}
