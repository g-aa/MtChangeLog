using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Editable
{
    public class ProjectRevisionEditable
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Revision { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public ProjectVersionShortView ProjectVersion { get; set; }
        public ProjectRevisionShortView ParentRevision { get; set; }
        public CommunicationShortView Communication { get; set; }
        public ArmEditShortView ArmEdit { get; set; }
        public IEnumerable<AuthorShortView> Authors { get; set; }
        public IEnumerable<RelayAlgorithmShortView> RelayAlgorithms { get; set; }

        public ProjectRevisionEditable()
        {
            this.Authors = new HashSet<AuthorShortView>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithmShortView>();
        }

        public override string ToString()
        {
            return $"Project: {this.ProjectVersion.Module}-{this.ProjectVersion.Version}_{this.Revision}, date: {this.Date}, reason: {this.Reason}";
        }
    }
}
