using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{ 
    public class ProjectStatus : IDefaultable, IIdentifiable, IEqualityPredicate<ProjectStatus>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
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

        public Func<ProjectStatus, bool> GetEqualityPredicate()
        {
            return (ProjectStatus e) => e.Id == this.Id || e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, статус: {this.Title}";
        }
    }
}
