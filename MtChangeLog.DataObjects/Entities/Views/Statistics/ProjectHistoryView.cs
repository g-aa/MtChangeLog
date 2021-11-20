using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Statistics
{
    public class ProjectHistoryView
    {
        /// <summary>
        /// наименование проекта, комбинация: "AnalogModule.Title"-"Project.Title"-"Version"_"Revision"
        /// </summary>
        public string Title {get;set;}
        public DateTime Date { get; set; }
        public string Platform { get; set; }

        public string ArmEdit { get; set; }
        public string Communication { get; set; }
        
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> RelayAlgorithms { get; set; }

        public string Reason { get; set; }
        public string Description { get; set; }

        public ProjectHistoryView() 
        {
            this.Authors = new HashSet<string>();
            this.RelayAlgorithms = new HashSet<string>();
        }
    }
}
