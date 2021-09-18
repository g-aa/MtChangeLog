using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class AnalogModuleEditable : AnalogModuleBase
    {
        public IEnumerable<PlatformBase> Platforms { get; set; }

        public AnalogModuleEditable() : base() 
        {
            this.Platforms = new HashSet<PlatformBase>();    
        }

        public AnalogModuleEditable(AnalogModuleBase other) : base(other) 
        {
            
        }

        public static new AnalogModuleEditable Default => new AnalogModuleEditable(AnalogModuleBase.Default)
        {
            Platforms = new HashSet<PlatformBase>()
        };
    }
}
