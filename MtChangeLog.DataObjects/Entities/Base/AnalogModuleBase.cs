using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class AnalogModuleBase : IEquatable<AnalogModuleBase>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Current { get; set; }
        public string Description { get; set; }

        public AnalogModuleBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public AnalogModuleBase(AnalogModuleBase other) 
        {
            this.Id = other.Id;
            this.DIVG = other.DIVG;
            this.Title = other.Title;
            this.Current = other.Current;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] AnalogModuleBase other)
        {
            return this.Id == other.Id || this.DIVG == other.DIVG && this.Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AnalogModuleBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DIVG, this.Title);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, DIVG: {this.DIVG}, title: {this.Title}, current: {this.Current}";
        }

        public static AnalogModuleBase Default => new AnalogModuleBase() 
        {
            Id = Guid.Empty,
            DIVG = "ДИВГ.00000-00",
            Title = "БМРЗ-000",
            Current = "0A",
            Description = "шаблон"
        };
    }
}
