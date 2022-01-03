using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProtocolEditable
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Наименование протокола параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование протокола должно содержать не более 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public IEnumerable<CommunicationModuleShortView> CommunicationModules { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
