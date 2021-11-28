using MtChangeLog.DataObjects.Entities.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        StatisticsShortView GetShortStatistics();
    }
}
