using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class PlatformEditable : PlatformBase
    {
        public IEnumerable<AnalogModuleBase> AnalogModules { get; set; }

        public PlatformEditable() : base()
        {
            this.AnalogModules = new HashSet<AnalogModuleBase>();
        }

        public PlatformEditable(PlatformBase other) : base(other) 
        {
            
        }

        public static new PlatformEditable Default => new PlatformEditable(PlatformBase.Default)
        {
            AnalogModules = new HashSet<AnalogModuleBase>()
        };
    }
}
