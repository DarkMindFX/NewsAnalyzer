


using DMFX.NewsAnalysis.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMFX.NewsAnalysis.Services.Dal
{
    public interface IAnalyzerDal : IDalBase<Analyzer>
    {
        Analyzer Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
        }
}
