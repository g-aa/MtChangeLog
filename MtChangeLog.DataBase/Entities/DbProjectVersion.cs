using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbProjectVersion : ProjectVersionBase
    {
        #region Relationships
        public Guid PlatformId { get; set; }
        public DbPlatform Platform { get; set; }
        
        public Guid AnalogModuleId { get; set; }
        public DbAnalogModule AnalogModule { get; set; }

        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbProjectVersion() : base()
        {
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbProjectVersion(ProjectVersionBase other) : base(other) 
        {
            
        }

        public void Update(ProjectVersionBase other, DbAnalogModule module, DbPlatform platform) 
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Version = other.Version;
            this.Status = other.Status;
            this.Description = other.Description;
            this.AnalogModule = module;
            this.Platform = platform;
        }

        public ProjectVersionBase GetBase() 
        {
            return new ProjectVersionBase(this);
        }

        public ProjectVersionEditable GetEditable() 
        {
            return new ProjectVersionEditable(this)
            {
                AnalogModule = this.AnalogModule.GetBase(),
                Platform = this.Platform.GetBase()
            };
        }

        public ProjectVersionView GetView() 
        {
            return new ProjectVersionView(this)
            {
                Module = this.AnalogModule.Title,
                Platform = this.Platform.Title
            };
        }

        public ProjectVersionShortView GetShortView() 
        {
            return new ProjectVersionShortView()
            {
                Id = this.Id,
                Module = this.AnalogModule?.Title ?? "БМРЗ-000",
                Title = this.Title,
                Version = this.Version
            };
        }
    }
}
