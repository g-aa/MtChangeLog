using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class AuthorEditable
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Имя должно содержать не больше 32 символов")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Фамилия должна содержать не больше 32 символов")]
        public string LastName { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Должность должна содержать не больше 250 символов")]
        public string Position { get; set; }
    }
}
