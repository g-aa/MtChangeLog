using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbProtocol : IEquatable<DbProtocol>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbCommunicationModule> CommunicationModules { get; set; }
        #endregion

        public DbProtocol() 
        {
            this.Id = Guid.NewGuid();
            this.CommunicationModules = new HashSet<DbCommunicationModule>();
            this.Default = false;
        }

        public DbProtocol(ProtocolEditable other) : this()
        {
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public void Update(ProtocolEditable other, ICollection<DbCommunicationModule> adapters) 
        {
            if (this.Default)
            {
                throw new ArgumentException($"Default entity {this} can not by update");
            }
            // this.Id - не обновляется !!!
            this.Title = other.Title;
            this.Description = other.Description;
            this.CommunicationModules = adapters;
        }

        public bool Equals([AllowNull]DbProtocol other)
        {
            return this.Id == other.Id || this.Title == other.Title;
        }

        public ProtocolShortView ToShortView() 
        {
            var result = new ProtocolShortView()
            {
                Id = this.Id,
                Title = this.Title
            };
            return result;
        }

        public ProtocolEditable ToEditable() 
        {
            var result = new ProtocolEditable()
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                CommunicationModules = this.CommunicationModules?.OrderBy(e => e.Title).Select(e => e.ToShortView())
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbProtocol);
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
