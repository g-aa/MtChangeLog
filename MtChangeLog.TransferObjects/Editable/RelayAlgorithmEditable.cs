using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class RelayAlgorithmEditable : RelayAlgorithmShortView
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(32, ErrorMessage = "Наименование группы алгоритмов должно содержать не больше 32 символо")]
        public string Group { get; set; }

        [Required(ErrorMessage = "Код ANSI параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Код ANSI должен содержать не больше 32 символо")]
        [RegularExpression("^[0-9 A-Z -/]{0,32}$", ErrorMessage = "Код ANSI может содержать следующие символы 0-9, A-Z, -, /", MatchTimeoutInMilliseconds = 1000)]
        public string ANSI { get; set; }
        
        [Required(ErrorMessage = "Logical Node параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Logical Node должен содержать не больше 32 символо")]
        [RegularExpression("^[0-9 A-Z -/]{0,32}$", ErrorMessage = "Logical Node может содержать следующие символы 0-9, A-Z, -, /", MatchTimeoutInMilliseconds = 1000)]
        public string LogicalNode { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание алгоритма должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"ANSI: {this.ANSI}, {base.ToString()}";
        }
    }
}
