using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class CommunicationEditable
    {
        public Guid Id { get; set; }
        public string Protocols { get; set; }
        public string Description { get; set; }
    }
}
