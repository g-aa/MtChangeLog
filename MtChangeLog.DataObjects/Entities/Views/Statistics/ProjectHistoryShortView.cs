using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Statistics
{
    public class ProjectHistoryShortView
    {
        public Guid Id { get; set; }
        /// <summary>
        /// наименование проекта, комбинация: "AnalogModule.Title"-"Project.Title"-"Version"_"Revision"
        /// </summary>
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Platform { get; set; }
    }
}
