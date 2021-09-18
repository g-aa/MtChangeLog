using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbPlatform : PlatformBase
    {
        #region Relationships
        public ICollection<DbAnalogModule> AnalogModules { get; set; }
        public ICollection<DbProjectVersion> Projects { get; set; }
        #endregion

        public DbPlatform() : base()
        {
            this.AnalogModules = new HashSet<DbAnalogModule>();
            this.Projects = new HashSet<DbProjectVersion>();
        }

        public DbPlatform(PlatformBase other) : base(other) 
        {
            
        }

        public void Update(PlatformBase other, ICollection<DbAnalogModule> modules) 
        {
            this.Title = other.Title;
            this.Description = other.Description;
            this.AnalogModules = modules;
        }

        public PlatformBase GetBase() 
        {
            return new PlatformBase(this);
        }

        public PlatformEditable GetEditable() 
        {
            return new PlatformEditable(this)
            {
                AnalogModules = this.AnalogModules.Select(module => module.GetBase())
            };
        }
    }
}
