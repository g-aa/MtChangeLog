using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class AnalogModuleShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование модуля обязательный параметр для заполнения")]
        [RegularExpression("^БМРЗ-[0-9]{3}$", ErrorMessage = "Наименование должено иметь следующий вид БМРЗ-xxx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }
    }
}
