using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.Entities.Tables
{
    public class CommunicationModule : IDefaultable, IIdentifiable, IEqualityPredicate<CommunicationModule>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование адаптера параметр обязательный для заполнения")]
        [StringLength(255, ErrorMessage = "Наименование адаптера должно содержать не больше 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<Protocol> Protocols { get; set; }
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public CommunicationModule()
        {
            this.Id = Guid.NewGuid();
            this.Protocols = new HashSet<Protocol>();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public CommunicationModule(CommunicationModuleEditable editable) : this()
        {
            this.Title = editable.Title;
            this.Description = editable.Description;
        }

        public Func<CommunicationModule, bool> GetEqualityPredicate()
        {
            return (CommunicationModule e) => e.Id == this.Id || e.Title == this.Title;
        }

        public void Update(CommunicationModuleEditable other, ICollection<Protocol> protocols) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.Protocols = protocols;
            // this.ProjectRevisions - не обновляться !!!
        }

        public CommunicationModuleShortView ToShortView() 
        {
            var result = new CommunicationModuleShortView() 
            {
                Id = this.Id,
                Title = this.Title,
            };
            return result;
        }

        public CommunicationModuleTableView ToTableView() 
        {
            if (this.Protocols is null) 
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"CommunicationModule\" к представлению для таблицы, отсутствует перечень протоколов используемых в БМРЗ");   
            }
            var result = new CommunicationModuleTableView() 
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Protocols = this.Protocols.Any() ? string.Join(", ", this.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : ""
            };
            return result;
        }

        public CommunicationModuleEditable ToEditable() 
        {
            if (this.Protocols is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"CommunicationModule\" к представлению для редактирования, отсутствует перечень протоколов используемых в БМРЗ");
            }
            var result = new CommunicationModuleEditable() 
            {
                Id = this.Id,
                Title = this.Title,
                Protocols = this.Protocols.OrderBy(e => e.Title).Select(e => e.ToShortView()),
                Description = this.Description
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Title)) 
            {
                return $"ID = {this.Id}";
            }
            return this.Title;
        }
    }
}
