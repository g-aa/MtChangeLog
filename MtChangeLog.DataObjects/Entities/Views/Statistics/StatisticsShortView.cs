using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Statistics
{
    public class StatisticsShortView
    {
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public int ProjectCount { get; set; }
        public int ActualProjectCount { get; set; }
        public int TestProjectCount { get; set; }
        public int DeprecatedProjectCount { get; set; }
        public IEnumerable<ProjectHistoryShortView> LastModifiedProjects { get; set; }
        public IEnumerable<ProjectHistoryShortView> MostChangingProjects { get; set; }
    }
}
