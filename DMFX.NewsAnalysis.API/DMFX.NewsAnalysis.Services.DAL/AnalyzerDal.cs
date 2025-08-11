

using DMFX.NewsAnalysis.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace DMFX.NewsAnalysis.Services.Dal
{
    [Export(typeof(IAnalyzerDal))]
    public class AnalyzerDal : DalBaseImpl<Analyzer, Interfaces.IAnalyzerDal>, IAnalyzerDal
    {

        public AnalyzerDal(Interfaces.IAnalyzerDal dalImpl) : base(dalImpl)
        {
        }

        public Analyzer Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
