using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class ProjectRevisionShortView : ProjectVersionShortView
    {
        [Required(ErrorMessage = "Редакция БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Редакция БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Revision { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}_{this.Revision}";
        }
    }
}
