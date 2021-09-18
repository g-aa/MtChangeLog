using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbRelayAlgorithm : RelayAlgorithmBase
    {
        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbRelayAlgorithm() : base() 
        {
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbRelayAlgorithm(RelayAlgorithmBase other) : base(other) 
        {
            
        }

        public RelayAlgorithmBase GetBase() 
        {
            return new RelayAlgorithmBase(this);
        }
    }
}
