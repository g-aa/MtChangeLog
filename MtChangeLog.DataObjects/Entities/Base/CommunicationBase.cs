using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class CommunicationBase : IEquatable<CommunicationBase>
    {
        public Guid Id { get; set; }
        public string Protocols { get; set; }
        public string Description { get; set; }

        public CommunicationBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public CommunicationBase(CommunicationBase other) 
        {
            this.Id = other.Id;
            this.Protocols = other.Protocols;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] CommunicationBase other) 
        {
            return this.Id == other.Id || this.Protocols == other.Protocols;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CommunicationBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Protocols);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, protocols: {this.Protocols}";
        }

        public static CommunicationBase Default => new CommunicationBase() 
        {
            Id = Guid.Empty,
            Protocols = "modbus-mt",
            Description = "шаблон"
        };
    }
}
