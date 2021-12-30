using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MtChangeLog.DataBase.Entities.Tables
{
    internal class DbRelayAlgorithm : IEquatable<DbRelayAlgorithm>
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public string ANSI { get; set; }
        public string LogicalNode { get; set; }
        public string Description { get; set; }

        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbRelayAlgorithm()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbRelayAlgorithm(RelayAlgorithmEditable other) : base()
        {
            this.Group = other.Group;
            this.Title= other.Title;
            this.ANSI = other.ANSI;
            this.LogicalNode = other.LogicalNode;
            this.Description = other.Description;
        }

        public void Update(RelayAlgorithmEditable other) 
        {
            // this.Id - не обновляется !!!
            this.Group = other.Group;
            this.ANSI = other.ANSI;
            this.Title = other.Title;
            this.LogicalNode = other.LogicalNode;
            this.Description = other.Description;
            // this.ProjectRevisions - не обновляется !!!
        }

        public RelayAlgorithmShortView ToShortView() 
        {
            return new RelayAlgorithmShortView() 
            {
                Id = this.Id,
                Title = this.Title
            };
        }

        public RelayAlgorithmEditable ToEditable() 
        {
            return new RelayAlgorithmEditable() 
            {
                Id = this.Id,
                Group = this.Group,
                Title = this.Title,
                ANSI = this.ANSI,
                LogicalNode = this.LogicalNode,
                Description = this.Description
            };
        }

        public bool Equals([AllowNull] DbRelayAlgorithm other)
        {
            return this.Id == other.Id || this.Group == other.Group && this.Title == other.Title && this.ANSI == other.ANSI && this.LogicalNode == other.LogicalNode;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbRelayAlgorithm);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Group, this.Title, this.ANSI, this.LogicalNode);
        }

        public override string ToString()
        {
            return $"ANSI: {this.ANSI}, {this.Title}";
        }
    }
}
