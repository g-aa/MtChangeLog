using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Tables
{
    public class AnalogModuleTableView
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Current { get; set; }
        public string Description { get; set; }
    }
}
