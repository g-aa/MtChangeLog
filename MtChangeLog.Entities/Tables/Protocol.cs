using MtChangeLog.Abstractions.Entities;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class Protocol : IDefaultable, IIdentifiable, IEqualityPredicate<Protocol>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование протокола параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование протокола должно содержать не более 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<CommunicationModule> CommunicationModules { get; set; }
        #endregion

        public Protocol() 
        {
            this.Id = Guid.NewGuid();
            this.CommunicationModules = new HashSet<CommunicationModule>();
            this.Default = false;
        }

        public Protocol(ProtocolEditable other) : this()
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public Func<Protocol, bool> GetEqualityPredicate()
        {
            return (Protocol e) => e.Id == this.Id || e.Title == this.Title;
        }

        public void Update(ProtocolEditable other, ICollection<CommunicationModule> adapters) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.CommunicationModules = adapters;
        }

        public ProtocolShortView ToShortView() 
        {
            var result = new ProtocolShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public ProtocolEditable ToEditable() 
        {
            if (this.CommunicationModules is null)
            {
                throw new ArgumentException($"Нельзя преобразовать сущность \"Protocol\" к представлению для редактирования, отсутствует перечень коммуникационных модулей используемых в БМРЗ");
            }
            var result = new ProtocolEditable()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                CommunicationModules = this.CommunicationModules.OrderBy(e => e.Title).Select(e => e.ToShortView())
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
