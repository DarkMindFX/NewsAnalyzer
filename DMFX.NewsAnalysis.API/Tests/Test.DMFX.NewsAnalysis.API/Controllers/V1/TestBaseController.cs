using DMFX.NewsAnalysis.API.Controllers.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test.E2E.API.Controllers.V1
{
    class TestBaseController
    {
        class BaseControllerTestWrapper : BaseController
        {
            private readonly DMFX.NewsAnalysis.Interfaces.Entities.User currentUser = new DMFX.NewsAnalysis.Interfaces.Entities.User { ID = 100001 };

            public override DMFX.NewsAnalysis.Interfaces.Entities.User CurrentUser
            {
                get
                {
                    return currentUser;
                }
            }

            public new void SetCreatedModifiedProperties(object obj, string propNameDate, string propNameID)
            {
                base.SetCreatedModifiedProperties(obj, propNameDate, propNameID);
            }

        }
        
    }
}
