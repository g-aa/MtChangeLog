using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class AnalogModuleEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Децимальный номер обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }

        [Required(ErrorMessage = "Наименование модуля обязательный параметр для заполнения")]
        [RegularExpression("^БМРЗ-[0-9]{3}$", ErrorMessage = "Наименование должено иметь следующий вид БМРЗ-xxx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Номинальный ток обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]A$", ErrorMessage ="Номинальный ток должен принимать значение от [0-9]A", MatchTimeoutInMilliseconds = 1000)]
        public string Current { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public IEnumerable<PlatformShortView> Platforms { get; set; }

        public AnalogModuleEditable()
        {
            this.Id = Guid.NewGuid();
            this.Platforms = new HashSet<PlatformShortView>();    
        }
    }
}
