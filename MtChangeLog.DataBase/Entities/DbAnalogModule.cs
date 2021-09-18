using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbAnalogModule : AnalogModuleBase
    {
        #region Relationships
        public ICollection<DbProjectVersion> ProjectVersion { get; set; }
        public ICollection<DbPlatform> Platforms { get; set; }
        #endregion

        public DbAnalogModule() : base()
        {
            this.ProjectVersion = new HashSet<DbProjectVersion>();
            this.Platforms = new HashSet<DbPlatform>();
        }

        public DbAnalogModule(AnalogModuleBase other) : base(other)
        {
            
        }

        public void Update(AnalogModuleBase other, ICollection<DbPlatform> platforms) 
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
            this.Platforms = platforms;
        }

        public AnalogModuleBase GetBase() 
        {
            return new AnalogModuleBase(this);
        }

        public AnalogModuleEditable GetEditable() 
        {
            return new AnalogModuleEditable(this)
            {
                Platforms = this.Platforms.Select(platforms => platforms.GetBase())
            };
        }
    }
}
