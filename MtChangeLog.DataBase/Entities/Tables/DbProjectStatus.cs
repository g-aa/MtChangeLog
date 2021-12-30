using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbProjectStatus : IEquatable<DbProjectStatus>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProjectVersion> ProjectVersions { get; set; }
        #endregion

        public DbProjectStatus() 
        {
            this.Id = Guid.NewGuid();
            this.ProjectVersions = new HashSet<DbProjectVersion>();
            this.Default = false;
        }

        public DbProjectStatus(ProjectStatusEditable other) : this() 
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public void Update(ProjectStatusEditable other) 
        {
            if (this.Default) 
            {
                throw new ArgumentException($"Default entity {this} can not by update");
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

        public bool Equals([AllowNull] DbProjectStatus other)
        {
            return this.Id == other.Id || this.Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbProjectStatus);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
