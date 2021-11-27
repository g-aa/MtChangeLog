using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class CommunicationEditable
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Перечень протоколов должен содержать не больше 250 символов")]
        public string Protocols { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
    }
}
