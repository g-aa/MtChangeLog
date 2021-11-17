using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectRevisionEditable : ProjectRevisionBase
    {
        public ProjectVersionView ProjectVersion { get; set; }
        public ProjectRevisionShortView ParentRevision { get; set; }
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

        public override string ToString()
        {
            return $"Project: {this.ProjectVersion.Module}-{this.ProjectVersion.Version}_{this.Revision}, date: {this.Date}, reason: {this.Reason}";
        }

        public static new ProjectRevisionEditable Default => new ProjectRevisionEditable(ProjectRevisionBase.Default)
        {
            Authors = new HashSet<AuthorBase>(),
            RelayAlgorithms = new HashSet<RelayAlgorithmBase>()
        };
    }
}
