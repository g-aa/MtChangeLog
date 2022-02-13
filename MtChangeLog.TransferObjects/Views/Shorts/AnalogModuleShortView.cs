using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Shorts
{
    public class AnalogModuleShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование аналогово модуля обязательный параметр для заполнения")]
        [RegularExpression("^БМРЗ-[0-9 A-Z А-Я]{2,4}$", ErrorMessage = "Наименование аналогово модуля должено иметь следующий вид БМРЗ-xxxx, где x - [0-9 A-Z А-Я]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
