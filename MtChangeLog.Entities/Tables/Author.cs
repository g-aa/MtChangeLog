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
    public class Author : IIdentifiable, IDefaultable, IEqualityPredicate<Author>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Имя должно содержать не больше 32 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Фамилия должна содержать не больше 32 символов")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(250, ErrorMessage = "Должность автора должна содержать не больше 250 символов")]
        public string Position { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public Author()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public Author(AuthorEditable other) : this()
        {
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.Position = other.Position;
        }

        public Func<Author, bool> GetEqualityPredicate()
        {
            return (Author e) => e.Id == this.Id || e.FirstName == this.FirstName && e.LastName == this.LastName;
        }

        public void Update(AuthorEditable other) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            // this.Id - не должно обновляться !!!
            this.LastName = other.LastName;
            this.FirstName = other.FirstName;
            this.Position = other.Position;
            // this.ProjectRevisions - не должно обновляеться !!!
        }

        public AuthorShortView ToShortView() 
        {
            var result = new AuthorShortView() 
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName =this.LastName
            };
            return result;
        }

        public AuthorEditable ToEditable() 
        {
            var result = new AuthorEditable()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Position = this.Position
            };
            return result;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.FirstName, this.LastName);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.LastName)) 
            {
                return $"ID = {this.Id}";
            }
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
