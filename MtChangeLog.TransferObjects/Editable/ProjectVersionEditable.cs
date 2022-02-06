using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class ProjectVersionEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage ="Децимальный номер версии проекта обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }
        
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Наименование проекта должно содержать не больше {1} и не менее {2} символов")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Версия проекта обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Версия проекта может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание проекта должно содержать не больше 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Статус проекта параметр обязательный для заполнения")]
        public ProjectStatusShortView ProjectStatus { get; set; }

        [Required(ErrorMessage = "Аналоговый модуль параметр обязательный для заполнения")]
        public AnalogModuleShortView AnalogModule { get; set; }
        
        [Required(ErrorMessage = "Платформа параметр обязательный для заполнения")]
        public PlatformShortView Platform { get; set; }

        public override string ToString()
        {
            return $"{this.AnalogModule.Title}_{this.Title}_{this.Version}";
        }
    }
}
