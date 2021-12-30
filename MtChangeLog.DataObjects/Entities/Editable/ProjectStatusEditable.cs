using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectStatusEditable
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование статуса параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование статуса должно содержать не более 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание статуса должно содержать не больше 500 символов")]
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
