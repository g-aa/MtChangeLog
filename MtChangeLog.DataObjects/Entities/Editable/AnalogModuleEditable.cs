using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class AnalogModuleEditable
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Current { get; set; }
        public string Description { get; set; }
        public IEnumerable<PlatformShortView> Platforms { get; set; }

        public AnalogModuleEditable()
        {
            this.Id = Guid.NewGuid();
            this.Platforms = new HashSet<PlatformShortView>();    
        }
    }
}
