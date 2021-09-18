using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectVersionEditable : ProjectVersionBase
    {
        public AnalogModuleBase AnalogModule { get; set; }
        public PlatformBase Platform { get; set; }

        public ProjectVersionEditable() : base() 
        {
            
        }

        public ProjectVersionEditable(ProjectVersionBase other) : base(other) 
        {
            
        }

        public static new ProjectVersionEditable Default => new ProjectVersionEditable(ProjectVersionBase.Default);
    }
}
