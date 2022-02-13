using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Shorts
{
    public class ProjectVersionShortView
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings =true, ErrorMessage = "Префикс проекта, псевдоним аналогового модуля обязательный параметр для заполнения")]
        [RegularExpression("^(БФПО(-[0-9]{3})?)?$", ErrorMessage = "Префикс проекта, псевдоним аналогового модуля должено иметь следующий вид БФПО-xxx, где x - [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Prefix { get; set; }

        [StringLength(16, MinimumLength = 2, ErrorMessage = "Наименование проекта должно содержать не больше {1} и не менее {2} символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Версия БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Версия БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }

        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}
