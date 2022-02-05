using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class ArmEditEditable : ArmEditShortView
    {
        [Required(ErrorMessage = "Децимальный номер ArmEdit обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер ArmEdit должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание ArmEdit должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
