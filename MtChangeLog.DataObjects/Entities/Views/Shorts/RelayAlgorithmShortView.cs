using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Views.Shorts
{
    public class RelayAlgorithmShortView
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование алгоритма параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование алгоритма должно содержать не больше 32 символо")]
        public string Title { get; set; }
    }
}
