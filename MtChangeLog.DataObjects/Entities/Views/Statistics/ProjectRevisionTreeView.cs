using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Statistics
{
    public class ProjectRevisionTreeView : ProjectRevisionShortView
    {
        public Guid ParentId { get; set; }
        public string ArmEdit { get; set; }
        public string Date { get; set; }
        public string Platform { get; set; }
    }
}
