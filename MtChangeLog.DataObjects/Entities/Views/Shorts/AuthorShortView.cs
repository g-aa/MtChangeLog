using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class AuthorShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Имя должно содержать не больше 32 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Фамилия должна содержать не больше 32 символов")]
        public string LastName { get; set; }
    }
}
