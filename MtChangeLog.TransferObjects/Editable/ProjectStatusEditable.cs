using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class ProjectStatusEditable : ProjectStatusShortView
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание статуса должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
