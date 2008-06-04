using System;
using System.Collections.Generic;
using System.Text;
using Habanero.Base;

namespace Habanero.UI.Base
{

    public class DefaultBODeletor : IBusinessObjectDeletor
    {
        public virtual void DeleteBusinessObject(IBusinessObject businessObject)
        {
            businessObject.Delete();
            businessObject.Save();
        }
    }
}