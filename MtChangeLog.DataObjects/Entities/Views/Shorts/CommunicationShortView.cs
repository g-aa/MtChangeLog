using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class CommunicationShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Перечень протоколов параметр обязательный для заполнения")]
        [StringLength(250, ErrorMessage = "Перечень протоколов должен содержать не больше 250 символов")]
        public string Protocols { get; set; }
    }
}
