﻿using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities
{
    internal class DbAuthor : IEquatable<DbAuthor>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<DbProjectRevision> ProjectRevisions { get; set; }
        #endregion

        public DbAuthor()
        {
            this.Id = Guid.NewGuid();
            this.ProjectRevisions = new HashSet<DbProjectRevision>();
        }

        public DbAuthor(AuthorEditable other) : base()
        {
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.Position = other.Position;
        }

        public void Update(AuthorEditable other) 
        {
            // this.Id - не должно обновляться !!!
            this.LastName = other.LastName;
            this.FirstName = other.FirstName;
            this.Position = other.Position;
            // this.ProjectRevisions - не должно обновляеться !!!
        }

        public AuthorShortView ToShortView() 
        {
            return new AuthorShortView() 
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName =this.LastName
            };
        }

        public AuthorEditable ToEditable() 
        {
            return new AuthorEditable()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Position = this.Position
            };
        }

        public bool Equals([AllowNull] DbAuthor other)
        {
            return this.Id == other.Id || this.FirstName == other.FirstName && this.LastName == other.LastName;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DbAuthor);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.FirstName, this.LastName);
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}, position: {this.Position}";
        }
    }
}
