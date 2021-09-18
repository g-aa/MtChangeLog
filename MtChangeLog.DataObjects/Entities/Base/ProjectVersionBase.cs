using MtChangeLog.DataObjects.Enumerations;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class ProjectVersionBase : IEquatable<ProjectVersionBase>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public ProjectVersionBase() 
        {
            this.Id = Guid.NewGuid();
            // this.Status = Status.Test;
        }

        public ProjectVersionBase(ProjectVersionBase other) 
        {
            this.Id = other.Id;
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Version = other.Version;
            this.Status = other.Status;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] ProjectVersionBase other)
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Title == other.Title && this.Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProjectVersionBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DIVG, this.Title, this.Version);
        }
        public override string ToString()
        {
            return $"id: {this.Id}, DIVG: {this.DIVG}, title: {this.Title}, version: {this.Version}";
        }

        public static ProjectVersionBase Default => new ProjectVersionBase()
        {
            Id = Guid.Empty,
            DIVG = "ДИВГ.00000-00",
            Title = "КСЗ",
            Version = "00",
            Status = "Test",
            Description = "шаблон"
        };
    }
}
