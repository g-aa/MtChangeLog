using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class AnalogModuleEditable : AnalogModuleShortView
    {
        [Required(ErrorMessage = "Децимальный номер аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер аналогового модуля должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }

        [Required(ErrorMessage = "Номинальный ток аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{1}A$", ErrorMessage = "Номинальный ток аналогового модуля должен принимать значение от [0-9]A", MatchTimeoutInMilliseconds = 1000)]
        public string Current { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание аналогового модуля должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public IEnumerable<PlatformShortView> Platforms { get; set; }

        public AnalogModuleEditable()
        {
            this.Id = Guid.NewGuid();
            this.Platforms = new HashSet<PlatformShortView>();    
        }

        public override string ToString()
        {
            return $"{base.ToString()}, номинальный ток: {this.Current}";
        }
    }
}
