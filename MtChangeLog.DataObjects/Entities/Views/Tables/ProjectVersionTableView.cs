using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Tables
{
    public class ProjectVersionTableView
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public string Module { get; set; }
    }
}
