using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Tables
{
    public class CommunicationModuleTableView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Protocols { get; set; }
        public string Description { get; set; }
    }
}
