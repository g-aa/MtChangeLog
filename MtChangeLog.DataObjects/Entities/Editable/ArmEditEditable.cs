using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ArmEditEditable
    {
        public Guid Id { get; set; }
        [Required]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}", ErrorMessage = "Децимальный номер: должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }
        [Required]
        [RegularExpression("^[0-9]{2}", ErrorMessage = "Версия БФПО должна находиться в диапазоне [00-99]", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символо")]
        public string Description { get; set; }
    }
}
