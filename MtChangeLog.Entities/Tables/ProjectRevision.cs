using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class ProjectRevision : IIdentifiable, IEqualityPredicate<ProjectRevision>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Revision { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }

        #region Relationships
        public Guid ProjectVersionId { get; set; }
        public ProjectVersion ProjectVersion { get; set; }

        public Guid ParentRevisionId { get; set; }
        public ProjectRevision ParentRevision { get; set; }

        public Guid ArmEditId { get; set; }
        public ArmEdit ArmEdit { get; set; }

        public Guid CommunicationModuleId { get; set; }
        public CommunicationModule CommunicationModule { get; set; }

        public ICollection<Author> Authors { get; set; }
        public ICollection<RelayAlgorithm> RelayAlgorithms { get; set; }
        #endregion

        public ProjectRevision()
        {
            this.Id = Guid.NewGuid();
            this.Date = DateTime.Now;
            this.Authors = new HashSet<Author>();
            this.RelayAlgorithms = new HashSet<RelayAlgorithm>();
        }

        public Func<ProjectRevision, bool> GetEqualityPredicate()
        {
            return (ProjectRevision e) => e.Id == this.Id
            || (e.ProjectVersionId == this.ProjectVersionId || e.ProjectVersion.DIVG == this.ProjectVersion.DIVG) && e.Revision == this.Revision;
        }

        public override int GetHashCode()
        {
            // при определении уникальности картежа нужно учитывать и версию проекта к которой он привязан !!!
            // ПС чисто теоретически даты и время компиляции должны отличасться, но так происходит не всегда
            return HashCode.Combine(this.ProjectVersionId, this.Date, this.Revision);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, {this.ProjectVersion?.Prefix}-{this.ProjectVersion?.Title}-{this.ProjectVersion?.Version}_{this.Revision}, дата изменения: {this.Date}";
        }
    }
}
