

using DMFX.NewsAnalysis.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.Services.Dal
{
    [Export(typeof(ISentimentTypeDal))]
    public class SentimentTypeDal : DalBaseImpl<SentimentType, Interfaces.ISentimentTypeDal>, ISentimentTypeDal
    {

        public SentimentTypeDal(Interfaces.ISentimentTypeDal dalImpl) : base(dalImpl)
        {
        }

        public SentimentType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
