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
    public class ProjectStatus : IDefaultable, IIdentifiable, IEqualityPredicate<ProjectStatus>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Наименование статуса параметр обязательный для заполнения")]
        [StringLength(32, ErrorMessage = "Наименование статуса должно содержать не более 32 символов")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true)]
        [StringLength(500, ErrorMessage = "Описание статуса должно содержать не больше 500 символов")]
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectVersion> ProjectVersions { get; set; }
        #endregion

        public ProjectStatus() 
        {
            this.Id = Guid.NewGuid();
            this.ProjectVersions = new HashSet<ProjectVersion>();
            this.Default = false;
        }

        public ProjectStatus(ProjectStatusEditable other) : this() 
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public Func<ProjectStatus, bool> GetEqualityPredicate()
        {
            return (ProjectStatus e) => e.Id == this.Id || e.Title == this.Title;
        }

        public void Update(ProjectStatusEditable other) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{this}\" не может быть обновлена");
            }
            //this.Id - не обновляется!
            this.Title = other.Title;
            this.Description = other.Description;
            //this.ProjectVersions - не обновляется!
        }

        public ProjectStatusShortView ToShortView() 
        {
            var result = new ProjectStatusShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public ProjectStatusEditable ToEditable() 
        {
            var result = new ProjectStatusEditable() 
            {
                Id = this.Id,
                Title = this.Title,
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
