using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class Protocol : IDefaultable, IIdentifiable, IEqualityPredicate<Protocol>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<CommunicationModule> CommunicationModules { get; set; }
        #endregion

        public Protocol() 
        {
            this.Id = Guid.NewGuid();
            this.CommunicationModules = new HashSet<CommunicationModule>();
            this.Default = false;
        }

        public Func<Protocol, bool> GetEqualityPredicate()
        {
            return (Protocol e) => e.Id == this.Id || e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Title);
        }
        
        public override string ToString()
        {
            return $"ID = {this.Id}, протокол: {this.Title}";
        }
    }
}
