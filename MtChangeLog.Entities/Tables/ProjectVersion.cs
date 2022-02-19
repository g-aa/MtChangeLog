using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class ProjectVersion : IIdentifiable, IEqualityPredicate<ProjectVersion>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }

        #region Relationships
        public Guid PlatformId { get; set; }
        public Platform Platform { get; set; }
        
        public Guid AnalogModuleId { get; set; }
        public AnalogModule AnalogModule { get; set; }

        public Guid ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; } 

        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public ProjectVersion()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        public Func<ProjectVersion, bool> GetEqualityPredicate()
        {
            return (ProjectVersion e) => e.Id == this.Id 
            || e.DIVG == this.DIVG 
            || e.Prefix == this.Prefix && e.Title == this.Title && e.Version == this.Version;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Prefix, this.Title, this.Version);
        }
        
        public override string ToString()
        {
            return $"ID = {this.Id}, {this.DIVG}, {this.Prefix}-{this.Title}-{this.Version}";
        }
    }
}
