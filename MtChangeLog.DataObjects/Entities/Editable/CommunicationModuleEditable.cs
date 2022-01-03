using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class CommunicationModuleEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Наименование адаптера параметр обязательный для заполнения")]
        [StringLength(255, ErrorMessage ="Наименование адаптера должно содержать не больше 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public IEnumerable<ProtocolShortView> Protocols { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
