using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbProjectVersion : IEquatable<DbProjectVersion>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }

        #region Relationships
        public Guid PlatformId { get; set; }
        public DbPlatform Platform { get; set; }
        
        public Guid AnalogModuleId { get; set; }
        public DbAnalogModule AnalogModule { get; set; }

        public Guid ProjectStatusId { get; set; }
        public DbProjectStatus ProjectStatus { get; set; } 

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
            this.Version = other.Version;
            this.Description = other.Description;
        }

        public void Update(ProjectVersionEditable other, DbAnalogModule module, DbPlatform platform, DbProjectStatus status) 
        {
            // this.Id - не обновляется !!!
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Version = other.Version;
            this.Description = other.Description;
            this.AnalogModule = module;
            this.Platform = platform;
            this.ProjectStatus = status;
            // this.ProjectRevisions - не обновляется !!!
        }

        public ProjectVersionShortView ToShortView() 
        {
            var result = new ProjectVersionShortView()
            {
                Id = this.Id,
                Title = this.Title,
                Version = this.Version,
                Module = this.AnalogModule?.Title ?? "БМРЗ-000"
            };
            return result;
        }

        public ProjectVersionTableView ToTableView()
        {
            var result = new ProjectVersionTableView() 
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Title = this.Title,
                Status = this.ProjectStatus.Title,
                Version = this.Version,
                Description = this.Description,
                Module = this.AnalogModule?.Title ?? "БМРЗ-000",
                Platform = this.Platform?.Title ?? "БМРЗ-000"
            };
            return result;
        }

        public ProjectVersionEditable ToEditable()
        {
            var result = new ProjectVersionEditable()
            {
                Id = this.Id,
                DIVG = this.DIVG,
                Title = this.Title,
                ProjectStatus = this.ProjectStatus?.ToShortView(),
                Version = this.Version,
                Description = this.Description,
                AnalogModule = this.AnalogModule?.ToShortView(),
                Platform = this.Platform?.ToShortView()
            };
            return result;
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
            return $"{this.DIVG}, {this.Title}_{this.Version}";
        }
    }
}
