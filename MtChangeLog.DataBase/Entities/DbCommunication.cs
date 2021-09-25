using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbCommunication : CommunicationBase
    {
        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbCommunication() : base() 
        {
            this.ProjectRevisions = new HashSet<DbProjectRevision>();    
        }

        public DbCommunication(CommunicationBase other) : base(other) 
        {
            
        }

        public void Update(CommunicationBase other) 
        {
            this.Description = other.Description;
            this.Protocols = other.Protocols;

        }

        public CommunicationBase GetBase() 
        {
            return new CommunicationBase(this);
        }
    }
}
