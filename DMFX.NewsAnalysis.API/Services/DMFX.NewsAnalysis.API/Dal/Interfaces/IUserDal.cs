

using DMFX.NewsAnalysis.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMFX.NewsAnalysis.API.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

    }
}
