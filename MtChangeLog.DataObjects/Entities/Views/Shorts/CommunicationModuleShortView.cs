using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class CommunicationModuleShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование адаптера параметр обязательный для заполнения")]
        [StringLength(255, ErrorMessage = "Наименование адаптера должно содержать не больше 32 символов")]
        public string Title { get; set; }

        public string Protocols { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
