using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
using MtChangeLog.DataObjects.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class StatisticsRepository : BaseRepository, IStatisticsRepository
    {
        public StatisticsRepository(ApplicationContext context) : base(context) 
        {
            
        }

        public StatisticsShortView GetShortStatistics() 
        {
            var result = new StatisticsShortView()
            {
                Date = DateTime.Now,
                ArmEdit = this.context.ArmEdits.OrderByDescending(e => e.Version).FirstOrDefault()?.Version,
                ProjectCount = this.context.ProjectVersions.Count(),
                ActualProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Actual.ToString()).Count(),
                TestProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Test.ToString()).Count(),
                DeprecatedProjectCount = this.context.ProjectVersions.Where(e => e.Status == Status.Deprecated.ToString()).Count(),
            };
            return result;
        }
    }
}
