using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class ProjectVersionShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование аналогово модуля обязательный параметр для заполнения")]
        [RegularExpression("^БМРЗ-[0-9]{3}$", ErrorMessage = "Наименование аналогово модуля должено иметь следующий вид БМРЗ-xxx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Module { get; set; }

        [StringLength(16, MinimumLength = 2, ErrorMessage = "Наименование проекта должно содержать не больше {1} и не менее {2} символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Версия БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Версия БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }
    }
}
