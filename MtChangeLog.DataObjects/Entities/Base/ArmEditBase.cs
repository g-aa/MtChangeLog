using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class ArmEditBase : IEquatable<ArmEditBase>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public ArmEditBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public ArmEditBase(ArmEditBase other) 
        {
            this.Id = other.Id;
            this.DIVG = other.DIVG;
            this.Version = other.Version;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] ArmEditBase other) 
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Version == other.Version;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ArmEditBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DIVG, this.Version);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, DIVG: {this.DIVG}, version: {this.Version}";
        }

        public static ArmEditBase Default => new ArmEditBase()
        {
            Id = Guid.Empty,
            DIVG = "ДИВГ.00000-00",
            Version = "v0.00.0.00",
            Description = "шаблон для ArmEdit"
        };
    }
}
