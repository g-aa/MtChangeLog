using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.Entities.Tables
{
    public class ArmEdit : IIdentifiable, IDefaultable, IEqualityPredicate<ArmEdit>
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Децимальный номер ArmEdit обязательный параметр для заполнения")]
        [RegularExpression("^ДИВГ.[0-9]{5}-[0-9]{2}$", ErrorMessage = "Децимальный номер ArmEdit должен иметь следующий вид ДИВГ.xxxxx-xx, где x - число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string DIVG { get; set; }

        [Required(ErrorMessage = "Версия ArmEdit параметр обязательный для заполнения")]
        [RegularExpression("^v[0-9]{1}.[0-9]{2}.[0-9]{2}.[0-9]{2}$", ErrorMessage = "Версия ArmEdit должна принимать следующий вид vx.xx.xx.xx, где - x число [0-9]", MatchTimeoutInMilliseconds = 1000)]
        public string Version { get; set; }
        
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание ArmEdit должно содержать не больше 500 символов")]
        public string Description { get; set; }
        
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public ArmEdit() 
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public ArmEdit(ArmEditEditable other) : this()
        {
            this.DIVG = other.DIVG;
            this.Version = other.Version;
            this.Date = other.Date;
            this.Description = other.Description;
        }

        public Func<ArmEdit, bool> GetEqualityPredicate()
        {
            // return (ArmEdit e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Version == this.Version; - пока нет полных данных по ДИВГ-ам
            return (ArmEdit e) => e.Id == this.Id || e.Version == this.Version;
        }

        public void Update(ArmEditEditable other) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            // this.Id - не обновляеться !!!
            this.DIVG = other.DIVG;
            this.Date = other.Date;
            this.Description = other.Description;
            this.Version = other.Version;
            // this.ProjectRevisions - не обновляеться !!!
        }

        public ArmEditShortView ToShortView() 
        {
            var result = new ArmEditShortView()
            {
                Id = this.Id,
                Version = this.Version
            };
            return result;
        }

        public ArmEditEditable ToEditable() 
        {
            var result = new ArmEditEditable() 
            {
                Id = this.Id,
                Date = this.Date,
                DIVG = this.DIVG,
                Version = this.Version,
                Description = this.Description
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Version);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Version) && string.IsNullOrEmpty(this.DIVG)) 
            {
                return $"ID = {this.Id}";
            }
            return $"ArmEdit: {this.DIVG}, {this.Version}";
        }
    }
}
