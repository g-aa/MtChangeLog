using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class AuthorEditable : AuthorShortView
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(250, ErrorMessage = "Должность автора должна содержать не больше 250 символов")]
        public string Position { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, {this.Position}";
        }
    }
}
