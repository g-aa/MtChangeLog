using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class CommunicationModule : IDefaultable, IIdentifiable, IEqualityPredicate<CommunicationModule>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<Protocol> Protocols { get; set; }
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public CommunicationModule()
        {
            this.Id = Guid.NewGuid();
            this.Protocols = new HashSet<Protocol>();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public Func<CommunicationModule, bool> GetEqualityPredicate()
        {
            return (CommunicationModule e) => e.Id == this.Id || e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, адаптер: {this.Title ?? ""}";
        }
    }
}
