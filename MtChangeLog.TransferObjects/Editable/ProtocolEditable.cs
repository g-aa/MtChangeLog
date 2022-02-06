using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Editable
{
    public class ProtocolEditable : ProtocolShortView
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public IEnumerable<CommunicationModuleShortView> CommunicationModules { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
