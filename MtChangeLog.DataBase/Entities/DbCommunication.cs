using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbCommunication : IEquatable<DbCommunication>
    {
        public Guid Id { get; set; }
        public string Protocols { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbCommunication()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();    
        }

        public DbCommunication(CommunicationEditable editable) : base()
        {
            this.Protocols = editable.Protocols;
            this.Description = editable.Description;
        }

        public void Update(CommunicationEditable other) 
        {
            // this.Id - не обновляется !!!
            this.Protocols = other.Protocols;
            this.Description = other.Description;
            // this.ProjectRevisions - не обновляться !!!
        }

        public CommunicationShortView ToShortView() 
        {
            return new CommunicationShortView() 
            {
                Id = this.Id,
                Protocols = this.Protocols
            };
        }

        public CommunicationEditable ToEditable() 
        {
            return new CommunicationEditable() 
            {
                Id = this.Id,
                Protocols = this.Protocols,
                Description = this.Description
            };
        }

        public bool Equals([AllowNull] DbCommunication other)
        {
            return this.Id == other.Id || this.Protocols == other.Protocols;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbCommunication);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Protocols);
        }

        public override string ToString()
        {
            return $"protocols: {this.Protocols}";
        }
    }
}
