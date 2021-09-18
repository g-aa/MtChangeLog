using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MtChangeLog.DataObjects.Entities.Base
{
    public class PlatformBase : IEquatable<PlatformBase>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public PlatformBase() 
        {
            this.Id = Guid.NewGuid();
        }

        public PlatformBase(PlatformBase other) 
        {
            this.Id = other.Id;
            this.Title = other.Title;
            this.Description = other.Description;
        }

        public bool Equals([AllowNull] PlatformBase other)
        {
            return this.Id == other.Id || this.Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PlatformBase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Title);
        }

        public override string ToString()
        {
            return $"id: {this.Id}, title: {this.Title}";
        }

        public static PlatformBase Default => new PlatformBase() 
        {
            Id = Guid.Empty,
            Title = "БМРЗ-000",
            Description = "шаблон"
        };
    }
}
