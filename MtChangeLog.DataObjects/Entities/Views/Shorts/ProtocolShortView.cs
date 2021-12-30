using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class ProtocolShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование протокола параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование протокола должно содержать не более 32 символов")]
        public string Title { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
