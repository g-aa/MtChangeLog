using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class ArmEdit : IIdentifiable, IDefaultable, IEqualityPredicate<ArmEdit>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public ArmEdit() 
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public Func<ArmEdit, bool> GetEqualityPredicate()
        {
            // return (ArmEdit e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Version == this.Version; - пока нет полных данных по ДИВГ-ам
            return (ArmEdit e) => e.Id == this.Id || e.Version == this.Version;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Version);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, ArmEdit: {this.DIVG ?? ""}, {this.Version ?? ""}";
        }
    }
}
