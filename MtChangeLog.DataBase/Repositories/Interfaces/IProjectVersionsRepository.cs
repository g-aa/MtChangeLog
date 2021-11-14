using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IProjectVersionsRepository : ICRUD<ProjectVersionEditable>
    {
        IEnumerable<ProjectVersionView> GetEntities();
        IEnumerable<ProjectVersionShortView> GetShortEntities();
    }
}
