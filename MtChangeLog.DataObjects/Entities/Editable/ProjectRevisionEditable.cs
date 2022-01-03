using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectRevisionEditable
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Редакция БФПО обязательный параметр для заполнения")]
        [RegularExpression("^[0-9]{2}$", ErrorMessage = "Редакция БФПО, может принимать значение в интервала 00-99", MatchTimeoutInMilliseconds = 1000)]
        public string Revision { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Причина выпуска новой редакции должна содержать не больше 500 символов")]
        public string Reason { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [StringLength(5000, ErrorMessage = "Описание редакции должно содержать не больше 5000 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Версия проекта параметр обязательный для заполнения")]
        public ProjectVersionShortView ProjectVersion { get; set; }
        public ProjectRevisionShortView ParentRevision { get; set; }

        [Required(ErrorMessage = "Перечень поддерживаемых протоколов информационного обмена параметр обязательный для заполнения")]
        public CommunicationModuleShortView CommunicationModule { get; set; }

        [Required(ErrorMessage = "Версия ArmEdit параметр обязательный для заполнения")]
        public ArmEditShortView ArmEdit { get; set; }
        
        public IEnumerable<AuthorShortView> Authors { get; set; }
        public IEnumerable<RelayAlgorithmShortView> RelayAlgorithms { get; set; }

        public ProjectRevisionEditable()
        {
            this.Authors = new HashSet<AuthorShortView>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmShortView>();
        }

        public override string ToString()
        {
            return $"{this.ProjectVersion.Module}-{this.ProjectVersion.Version}_{this.Revision}, дата изменения: {this.Date}";
        }
    }
}
