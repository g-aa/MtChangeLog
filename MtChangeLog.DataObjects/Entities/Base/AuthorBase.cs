using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class AuthorBase : IEquatable<AuthorBase>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position {get; set;}

        public AuthorBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public AuthorBase(AuthorBase other) 
        {
            this.Id = other.Id;
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.Position = other.Position;
        }

        public bool Equals([AllowNull] AuthorBase other) 
        {
            return this.Id == other.Id || this.FirstName == other.FirstName && this.LastName == other.LastName;  
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AuthorBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.FirstName, this.LastName);
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}, position: {this.Position}";
        }

        public static AuthorBase Default => new AuthorBase()
        {
            Id = Guid.Empty,
            FirstName = "имя",
            LastName = "фамилия",
            Position = "должность автора"
        };
    }
}
