using MtChangeLog.DataObjects.Entities.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectRevisionEditable : ProjectRevisionBase
    {
        public ProjectVersionBase ProjectVersion { get; set; }
        public ProjectRevisionBase ParentRevision { get; set; }
        public CommunicationBase Communication { get; set; }
        public IEnumerable<AuthorBase> Authors { get; set; }
        public ArmEditBase ArmEdit { get; set; }
        public IEnumerable<RelayAlgorithmBase> RelayAlgorithms { get; set; }

        public ProjectRevisionEditable() : base() 
        {
            this.Authors = new HashSet<AuthorBase>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmBase>();
        }

        public ProjectRevisionEditable(ProjectRevisionBase other) : base(other) 
        {
            
        }

        public static new ProjectRevisionEditable Default => new ProjectRevisionEditable(ProjectRevisionBase.Default)
        {
            Authors = new HashSet<AuthorBase>(),
            RelayAlgorithms = new HashSet<RelayAlgorithmBase>()
        };
    }
}
