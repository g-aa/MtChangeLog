using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IRelayAlgorithmsRepository : ICRUD<RelayAlgorithmEditable>
    {
        IQueryable<RelayAlgorithmShortView> GetShortEntities();
        IQueryable<RelayAlgorithmEditable> GetTableEntities();
        RelayAlgorithmEditable GetTemplate();
    }
}
