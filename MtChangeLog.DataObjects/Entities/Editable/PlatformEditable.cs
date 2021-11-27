using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class PlatformEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Наименование платформы БМРЗ параметр обязательный для заполнения")]
        [RegularExpression("^БМРЗ-[0-9]{3}[A-Z]{0,2}$", ErrorMessage = "Наименование должено иметь следующий вид БМРЗ-XXXyy, где XXX - число [0-9], y - символы [A-Z] (не обязательны)", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public IEnumerable<AnalogModuleShortView> AnalogModules { get; set; }

        public PlatformEditable()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<AnalogModuleShortView>();
        }
    }
}
