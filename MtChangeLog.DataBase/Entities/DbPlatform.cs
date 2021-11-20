using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbPlatform : IEquatable<DbPlatform>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        #region Relationships
        public ICollection<DbAnalogModule> AnalogModules { get; set; }
        public ICollection<DbProjectVersion> Projects { get; set; }
        #endregion

        public DbPlatform()
        {
            this.Id = Guid.NewGuid();
            this.AnalogModules = new HashSet<DbAnalogModule>();
            this.Projects = new HashSet<DbProjectVersion>();
        }

        public DbPlatform(PlatformEditable other) : base() 
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public void Update(PlatformEditable other, ICollection<DbAnalogModule> modules) 
        {
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.AnalogModules = modules;
            // this.Projects - не обновляется !!!
        }

        public PlatformShortView ToShortView() 
        {
            return new PlatformShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
        }

        public PlatformTableView ToTableView()
        {
            return new PlatformTableView()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description
            };
        }

        public PlatformEditable ToEditable()
        {
            return new PlatformEditable()
            {
                Id = this.Id,
                Title = this.Title,
                Description= this.Description,
                AnalogModules = this.AnalogModules?.Select(module => module.ToShortView())
            };
        }

        public bool Equals([AllowNull] DbPlatform other)
        {
            return this.Id == other.Id || this.Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbPlatform);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return $"title: {this.Title}";
        }
    }
}
