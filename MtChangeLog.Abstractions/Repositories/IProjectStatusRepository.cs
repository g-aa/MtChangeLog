using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Repositories
{
    public interface IProjectStatusRepository : ICRUD<ProjectStatusEditable>
    {
        IQueryable<ProjectStatusShortView> GetShortEntities();
        IQueryable<ProjectStatusEditable> GetTableEntities();
        ProjectStatusEditable GetTemplate();
    }
}
