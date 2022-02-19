using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class LastProjectRevision
    {
        public Guid ProjectVersionId { get; set; }
        public Guid ProjectRevisionId { get; set; }
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}_{this.Revision}";
        }
    }
}
