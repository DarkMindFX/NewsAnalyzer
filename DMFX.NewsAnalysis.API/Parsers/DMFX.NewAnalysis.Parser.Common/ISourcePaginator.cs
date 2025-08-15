using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Parser.Common
{
    public interface ISourcePaginator
    {
        void Initialize();

        void Reset();

        int TotalPagesCount { get; }

        bool HasNextPage { get; }

        string GetNextPageUrl();
    }
}
