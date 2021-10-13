using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class RelayAlgorithmBase : IEquatable<RelayAlgorithmBase>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ANSI { get; set; }
        public string LogicalNode {get; set;}
        public string Description { get; set; }

        public RelayAlgorithmBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public RelayAlgorithmBase(RelayAlgorithmBase other) 
        {
            this.Id = other.Id;
            this.Title = other.Title;
            this.ANSI = other.ANSI;
            this.LogicalNode = other.LogicalNode;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] RelayAlgorithmBase other) 
        {
            return this.Id == other.Id || this.Title == other.Title && this.ANSI == other.ANSI && this.LogicalNode == other.LogicalNode;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as RelayAlgorithmBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Title, this.ANSI, this.LogicalNode);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, title: {this.Title}, ANSI: {this.ANSI}, LN: {this.LogicalNode}";
        }

        public static RelayAlgorithmBase Default => new RelayAlgorithmBase()
        {
            Id = Guid.Empty,
            Title = "наименование алгоритма РЗА",
            ANSI = "код ANSI",
            LogicalNode = "логический узел в МЭК-61850",
            Description = "шаблон функции релейной защиты"
        };
    }
}
