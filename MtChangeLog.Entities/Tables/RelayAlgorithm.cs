using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class RelayAlgorithm : IIdentifiable, IDefaultable, IEqualityPredicate<RelayAlgorithm>
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public string ANSI { get; set; }
        public string LogicalNode { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public RelayAlgorithm()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
        }

        public Func<RelayAlgorithm, bool> GetEqualityPredicate()
        {
            return (RelayAlgorithm e) => e.Id == this.Id || e.Group == this.Group && e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Group, this.Title, this.ANSI, this.LogicalNode);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, ANSI: {this.ANSI}, {this.Title}";
        }
    }
}
