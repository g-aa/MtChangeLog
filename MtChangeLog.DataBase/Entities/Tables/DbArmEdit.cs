using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbArmEdit : IEquatable<DbArmEdit>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbArmEdit() : base() 
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbArmEdit(ArmEditEditable other) : base()
        {
            this.DIVG = other.DIVG;
            this.Version = other.Version;
            this.Date = other.Date;
            this.Description = other.Description;
        }

        public void Update(ArmEditEditable other) 
        {
            // this.Id - не обновляеться !!!
            this.DIVG = other.DIVG;
            this.Date = other.Date;
            this.Description = other.Description;
            this.Version = other.Version;
            // this.ProjectRevisions - не обновляеться !!!
        }

        public ArmEditShortView ToShortView() 
        {
            return new ArmEditShortView()
            {
                Id = this.Id,
                Version = this.Version
            };
        }

        public ArmEditEditable ToEditable() 
        {
            return new ArmEditEditable() 
            {
                Id = this.Id,
                Date = this.Date,
                DIVG = this.DIVG,
                Version = this.Version,
                Description = this.Description
            }; 
        }

        public bool Equals([AllowNull] DbArmEdit other)
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbArmEdit);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Version);
        }

        public override string ToString()
        {
            return $"DIVG: {this.DIVG}, version: {this.Version}";
        }
    }
}
