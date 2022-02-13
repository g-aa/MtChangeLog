using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Statistics
{
    public class ProjectVersionHistoryView
    {
        /// <summary>
        /// наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"
        /// </summary>
        public string Title { get; set; }
        public ICollection<ProjectRevisionHistoryView> History { get; private set; }
        
        public ProjectVersionHistoryView() 
        {
            this.Title = "БМРЗ";
            this.History = new List<ProjectRevisionHistoryView>();
        }
    }
}
