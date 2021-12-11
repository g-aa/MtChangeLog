using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IProjectRevisionsRepository : ICRUD<ProjectRevisionEditable>
    {
        IEnumerable<ProjectRevisionShortView> GetShortEntities();
        IEnumerable<ProjectRevisionTableView> GetTableEntities();
        ProjectRevisionEditable GetTemplate(Guid guid);
    }
}
