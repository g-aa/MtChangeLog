using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Shorts
{
    public class ArmEditShortView
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Версия ArmEdit параметр обязательный для заполнения")]
        [RegularExpression("^v[0-9]{1}.[0-9]{2}.[0-9]{2}.[0-9]{2}$", ErrorMessage = "Версия ArmEdit должна принимать следующий вид vx.xx.xx.xx, где - x число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }

        public override string ToString()
        {
            return $"ArmEdit: {this.Version}";
        }
    }
}
