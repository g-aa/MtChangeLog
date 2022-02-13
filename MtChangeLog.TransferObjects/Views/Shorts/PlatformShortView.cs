using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Shorts
{
    public class PlatformShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование платформы БМРЗ параметр обязательный для заполнения")]
        [RegularExpression("^БМРЗ-[0-9 A-Z А-Я]{2,5}$", ErrorMessage = "Наименование платформы должено иметь следующий вид БМРЗ-xxxxx, где x - [0-9 A-Z А-Я]", MatchTimeoutInMilliseconds = 1000)]
        public string Title { get; set; }
        public override string ToString()
        {
            return this.Title;
        }
    }
}
