using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IAnalogModuleRepository : ICRUD<AnalogModuleEditable>
    {
        IEnumerable<AnalogModuleBase> GetEntities();
    }
}
