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
    public class RelayAlgorithm : IIdentifiable, IDefaultable, IEqualityPredicate<RelayAlgorithm>
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(32, ErrorMessage = "Наименование группы алгоритмов должно содержать не больше 32 символо")]
        public string Group { get; set; }

        [Required(ErrorMessage = "Наименование алгоритма параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование алгоритма должно содержать не больше 32 символо")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Код ANSI параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Код ANSI должен содержать не больше 32 символо")]
        [RegularExpression("^[0-9 A-Z -/]{0,32}$", ErrorMessage = "Код ANSI может содержать следующие символы 0-9, A-Z, -, /", MatchTimeoutInMilliseconds = 1000)]
        public string ANSI { get; set; }

        [Required(ErrorMessage = "Logical Node параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Logical Node должен содержать не больше 32 символо")]
        [RegularExpression("^[0-9 A-Z -/]{0,32}$", ErrorMessage = "Logical Node может содержать следующие символы 0-9, A-Z, -, /", MatchTimeoutInMilliseconds = 1000)]
        public string LogicalNode { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание алгоритма должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public RelayAlgorithm()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        public RelayAlgorithm(RelayAlgorithmEditable other) : this()
        {
            this.Group = other.Group;
            this.Title= other.Title;
            this.ANSI = other.ANSI;
            this.LogicalNode = other.LogicalNode;
            this.Description = other.Description;
        }

        public Func<RelayAlgorithm, bool> GetEqualityPredicate()
        {
            return (RelayAlgorithm e) => e.Id == this.Id || e.Group == this.Group && e.Title == this.Title;
        }

        public void Update(RelayAlgorithmEditable other) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            // this.Id - не обновляется !!!
            this.Group = other.Group;
            this.ANSI = other.ANSI;
            this.Title = other.Title;
            this.LogicalNode = other.LogicalNode;
            this.Description = other.Description;
            // this.ProjectRevisions - не обновляется !!!
        }

        public RelayAlgorithmShortView ToShortView() 
        {
            var result = new RelayAlgorithmShortView() 
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public RelayAlgorithmEditable ToEditable() 
        {
            var result = new RelayAlgorithmEditable() 
            {
                Id = this.Id,
                Group = this.Group,
                Title = this.Title,
                ANSI = this.ANSI,
                LogicalNode = this.LogicalNode,
                Description = this.Description
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Group, this.Title, this.ANSI, this.LogicalNode);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Title) && string.IsNullOrEmpty(this.ANSI))
            {
                return $"ID = {this.Id}";
            }
            return $"ANSI: {this.ANSI}, {this.Title}";
        }
    }
}
