using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class Author : IIdentifiable, IDefaultable, IEqualityPredicate<Author>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public Author()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<ProjectRevision>();
            this.Default = false;
        }

        public Func<Author, bool> GetEqualityPredicate()
        {
            return (Author e) => e.Id == this.Id || e.FirstName == this.FirstName && e.LastName == this.LastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.FirstName, this.LastName);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, {this.FirstName ?? ""} {this.LastName ?? ""}";
        }
    }
}
