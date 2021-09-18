using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class ProjectRevisionBase : IEquatable<ProjectRevisionBase>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Revision { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

        public ProjectRevisionBase() 
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
        }

        public ProjectRevisionBase(ProjectRevisionBase other) 
        {
            this.Id = other.Id;
            this.Date = other.Date;
            this.Revision = other.Revision;
            this.Reason = other.Reason;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] ProjectRevisionBase other)
        {
            return this.Id == other.Id || this.Date == other.Date && this.Revision == other.Revision && this.Reason == other.Reason;            
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProjectRevisionBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Date, this.Revision, this.Reason);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, date: {this.Date}, revision: {this.Revision}, reason {this.Reason}";
        }

        public static ProjectRevisionBase Default => new ProjectRevisionBase()
        {
            Id = Guid.Empty,
            Date = DateTime.Now,
            Revision = "00",
            Reason = "причина ревизии проекта",
            Description = "шаблон"
        };
    }
}
