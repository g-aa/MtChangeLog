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
    public interface IRelayAlgorithmsRepository : ICRUD<RelayAlgorithmEditable>
    {
        IQueryable<RelayAlgorithmShortView> GetShortEntities();
        IQueryable<RelayAlgorithmEditable> GetTableEntities();
        RelayAlgorithmEditable GetTemplate();
    }
}
