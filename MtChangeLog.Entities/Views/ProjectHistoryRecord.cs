using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class ProjectHistoryRecord
    {
        public Guid ProjectVersionId { get; set; }
        public Guid ParentRevisionId { get; set; }
        public Guid ProjectRevisionId { get; set; }
        public string Platform { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string ArmEdit { get; set; }
        public string Algorithms { get; set; }
        public string Authors { get; set; }
        public string Protocols { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
