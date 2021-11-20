using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class PlatformEditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<AnalogModuleShortView> AnalogModules { get; set; }

        public PlatformEditable()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<AnalogModuleShortView>();
        }
    }
}
