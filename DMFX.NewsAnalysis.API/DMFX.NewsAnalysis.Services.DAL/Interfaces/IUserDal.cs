

using DMFX.NewsAnalysis.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using DMFX.NewsAnalysis.Services.Dal;

namespace DMFX.NewsAnalysis.Services.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

    }
}
