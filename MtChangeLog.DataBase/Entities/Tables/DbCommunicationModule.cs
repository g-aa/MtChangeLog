using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbCommunicationModule : IEquatable<DbCommunicationModule>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProtocol> Protocols { get; set; }
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbCommunicationModule()
        {
            this.Id = Guid.NewGuid();
            this.Protocols = new HashSet<DbProtocol>();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();    
        }

        public DbCommunicationModule(CommunicationModuleEditable editable) : base()
        {
            this.Title = editable.Title;
            this.Description = editable.Description;
        }

        public void Update(CommunicationModuleEditable other, ICollection<DbProtocol> protocols) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Default entity {this} can not by update");
            }
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.Protocols = protocols;
            // this.ProjectRevisions - не обновляться !!!
        }

        public CommunicationModuleShortView ToShortView() 
        {
            var result = new CommunicationModuleShortView() 
            {
                Id = this.Id,
                Title = this.Title,
            };
            return result;
        }

        public CommunicationModuleTableView ToTableView() 
        {
            var result = new CommunicationModuleTableView() 
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                Protocols = this.Protocols.Any() ? string.Join(", ", this.Protocols.OrderBy(e => e.Title).Select(e => e.Title)) : ""
            };
            return result;
        }

        public CommunicationModuleEditable ToEditable() 
        {
            var result = new CommunicationModuleEditable() 
            {
                Id = this.Id,
                Title = this.Title,
                Protocols = this.Protocols.OrderBy(e => e.Title).Select(e => e.ToShortView()),
                Description = this.Description
            };
            return result;
        }

        public bool Equals([AllowNull] DbCommunicationModule other)
        {
            return this.Id == other.Id || this.Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbCommunicationModule);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
