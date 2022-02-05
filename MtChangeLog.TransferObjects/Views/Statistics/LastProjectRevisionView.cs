using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Statistics
{
    public class LastProjectRevisionView
    {
        public string Module { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }
    }
}
