using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Statistics
{
    public class StatisticsView
    {
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public int ProjectCount { get; set; }
        public Dictionary<string, int> ProjectDistributions { get; set; }
        public IEnumerable<AuthorContributionView> AuthorContributions { get; set; }
        public IEnumerable<ProjectRevisionHistoryShortView> LastModifiedProjects { get; set; }
    }
}
