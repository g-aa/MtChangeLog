using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ArmEditEditable
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
