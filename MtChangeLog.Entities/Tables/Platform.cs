using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class Platform : IDefaultable, IIdentifiable, IEqualityPredicate<Platform>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<AnalogModule> AnalogModules { get; set; }
        public ICollection<ProjectVersion> Projects { get; set; }
        #endregion

        public Platform()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<AnalogModule>();
            this.Projects = new HashSet<ProjectVersion>();
            this.Default = false;
        }

        public Func<Platform, bool> GetEqualityPredicate()
        {
            return (Platform e) => e.Id == this.Id || e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, платформа: {this.Title}";
        }
    }
}
