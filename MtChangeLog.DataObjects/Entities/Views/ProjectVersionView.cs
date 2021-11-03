using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views
{
    public class ProjectVersionView : ProjectVersionBase
    {
        public string Platform { get; set; }
        public string Module { get; set; }

        public ProjectVersionView() 
        {
            
        }

        public ProjectVersionView(ProjectVersionBase other) : base(other) 
        {
            
        }
    }
}
