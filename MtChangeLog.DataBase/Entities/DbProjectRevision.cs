using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbProjectRevision : ProjectRevisionBase
    {
        #region Relationships
        public Guid ProjectVersionId { get; set; }
        public DbProjectVersion ProjectVersion { get; set; }

        public Guid ParentRevisionId { get; set; }
        public DbProjectRevision ParentRevision { get; set; }

        public Guid ArmEditId { get; set; }
        public DbArmEdit ArmEdit { get; set; }

        public Guid CommunicationId { get; set; }
        public DbCommunication Communication { get; set; }

        public ICollection<DbAuthor> Authors { get; set; }
        public ICollection<DbRelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        public DbProjectRevision() : base() 
        {
            this.Authors = new HashSet<DbAuthor>();
            this.RelayAlgorithms = new HashSet<DbRelayAlgorithm>();
        }

        public ProjectRevisionBase GetBase() 
        {
            return new ProjectRevisionBase(this);
        }

        public ProjectRevisionEditable GetEditable() 
        {
            return new ProjectRevisionEditable(this)
            {
                ParentRevision = this.ParentRevision.GetBase(),
                ProjectVersion = this.ProjectVersion.GetBase(),
                Communication = this.Communication.GetBase(),
                Authors = this.Authors.Select(author => author.GetBase()),
                ArmEdit = this.ArmEdit.GetBase(),
                RelayAlgorithms = this.RelayAlgorithms.Select(alg => alg.GetBase()),
            };
        }
    }
}
