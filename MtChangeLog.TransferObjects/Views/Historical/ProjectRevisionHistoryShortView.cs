using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Historical
{
    public class ProjectRevisionHistoryShortView
    {
        public Guid Id { get; set; }
        /// <summary>
        /// наименование проекта, комбинация: "ProjectVersion.Prefix"-"ProjectVersion.Title"-"ProjectVersion.Version"_"ProjectRevision.Revision"
        /// </summary>
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Platform { get; set; }
    }
}
