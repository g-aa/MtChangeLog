using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Statistics
{
    public class ProjectVersionHistoryView
    {
        public string Title { get; set; }
        public ICollection<ProjectRevisionHistoryView> History { get; private set; }
        public ProjectVersionHistoryView() 
        {
            this.Title = "БМРЗ";
            this.History = new List<ProjectRevisionHistoryView>();
        }
    }
}
