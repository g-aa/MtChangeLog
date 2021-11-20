using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Tables
{
    public class ProjectRevisionTableView : ProjectRevisionShortView
    {
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public string Reason { get; set; }
    }
}
