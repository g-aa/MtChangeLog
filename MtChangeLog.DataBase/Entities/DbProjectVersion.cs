using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbProjectVersion : IEquatable<DbProjectVersion>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        #region Relationships
        public Guid PlatformId { get; set; }
        public DbPlatform Platform { get; set; }
        
        public Guid AnalogModuleId { get; set; }
        public DbAnalogModule AnalogModule { get; set; }

        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbProjectVersion()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbProjectVersion(ProjectVersionEditable other) : this()
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Status = other.Status;
            this.Version = other.Version;
            this.Description = other.Description;
        }

        public void Update(ProjectVersionEditable other, DbAnalogModule module, DbPlatform platform) 
        {
            // this.Id - не обновляется !!!
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Version = other.Version;
            this.Status = other.Status;
            this.Description = other.Description;
            this.AnalogModule = module;
            this.Platform = platform;
            // this.ProjectRevisions - не обновляется !!!
        }

        public ProjectVersionShortView ToShortView() 
        {
            return new ProjectVersionShortView()
            {
                Id = this.Id,
                Title = this.Title,
                Version = this.Version,
                Module = this.AnalogModule?.Title ?? "БМРЗ-000"
            };
        }

        public ProjectVersionTableView ToTableView()
        {
            return new ProjectVersionTableView() 
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Title = this.Title,
                Status = this.Status,
                Version = this.Version,
                Description = this.Description,
                Module = this.AnalogModule?.Title ?? "БМРЗ-000",
                Platform = this.Platform?.Title ?? "БМРЗ-000"
            };
        }

        public ProjectVersionEditable ToEditable()
        {
            return new ProjectVersionEditable()
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Title = this.Title,
                Status = this.Status,
                Version = this.Version,
                Description = this.Description,
                AnalogModule = this.AnalogModule?.ToShortView(),
                Platform = this.Platform?.ToShortView()
            };
        }

        public bool Equals([AllowNull] DbProjectVersion other)
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Title == other.Title && this.Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbProjectVersion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Title, this.Version);
        }
        public override string ToString()
        {
            return $"DIVG: {this.DIVG}, title: {this.Title}, version: {this.Version}";
        }
    }
}
