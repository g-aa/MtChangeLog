using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectVersionEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage ="Децимальный номер обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }
        
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Наименование должно содержать не больше {1} и не менее {2} символов")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Версия БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Версия БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(32, ErrorMessage = "Статус должен содержать не больше 32 символов")]
        public string Status { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Аналоговый модуль параметр обязательный для заполнения")]
        public AnalogModuleShortView AnalogModule { get; set; }
        
        [Required(ErrorMessage = "Платформа параметр обязательный для заполнения")]
        public PlatformShortView Platform { get; set; }
    }
}
