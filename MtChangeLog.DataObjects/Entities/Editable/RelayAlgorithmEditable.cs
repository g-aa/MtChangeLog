using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class RelayAlgorithmEditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ANSI { get; set; }
        public string LogicalNode { get; set; }
        public string Description { get; set; }
    }
}
