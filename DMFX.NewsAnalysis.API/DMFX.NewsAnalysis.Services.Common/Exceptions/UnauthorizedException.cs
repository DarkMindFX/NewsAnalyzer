using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Services.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
