using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbAnalogModule : IEquatable<DbAnalogModule>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Current { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProjectVersion> Projects { get; set; }
        public ICollection<DbPlatform> Platforms { get; set; }
        #endregion

        public DbAnalogModule()
        {
            this.Id = Guid.NewGuid();
            this.Projects = new HashSet<DbProjectVersion>();
            this.Platforms = new HashSet<DbPlatform>();
            this.Default = false;
        }

        public DbAnalogModule(AnalogModuleEditable other) : this()
        {
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
        }

        public void Update(AnalogModuleEditable other, ICollection<DbPlatform> platforms) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Default entity {this} can not by update");
            }
            var prohibPlatforms = this.Platforms.Except(platforms).Where(e => e.Projects.Intersect(this.Projects).Any()).Select(e => e.Title);
            if (prohibPlatforms.Any()) 
            {
                throw new ArgumentException($"The platform: {string.Join(", ", prohibPlatforms)} used in projects and cannot be excluded from the analog module");
            }
            // this.Id = - не обновляется !!!
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
            this.Platforms = platforms;
            // this.ProjectVersion - не обновляется !!!
        }

        public AnalogModuleShortView ToShortView() 
        {
            return new AnalogModuleShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
        }

        public AnalogModuleTableView ToTableView() 
        {
            return new AnalogModuleTableView()
            {
                Id = this.Id,
                Title = this.Title,
                Current = this.Current,
                DIVG = this.DIVG,
                Description = this.Description
            };
        }

        public AnalogModuleEditable ToEditable()
        {
            return new AnalogModuleEditable()
            {
                Id = this.Id,
                Title = this.Title,
                DIVG = this.DIVG,
                Current = this.Current,
                Description = this.Description,
                Platforms = this.Platforms?.Select(platforms => platforms.ToShortView())
            };
        }

        public bool Equals([AllowNull] DbAnalogModule other)
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Title == other.Title && this.Current == other.Current;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbAnalogModule);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Title, this.Current);
        }

        public override string ToString()
        {
            return $"{this.Title}, номинальный ток: {this.Current}";
        }
    }
}
