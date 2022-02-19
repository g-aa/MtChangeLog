using MtChangeLog.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Tables
{
    public class AnalogModule : IDefaultable, IIdentifiable, IEqualityPredicate<AnalogModule>
    {
        public Guid Id { get; set; }
        public string DIVG { get; set; }
        public string Title { get; set; }
        public string Current { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }

        #region Relationships
        public ICollection<ProjectVersion> Projects { get; set; }
        public ICollection<Platform> Platforms { get; set; }
        #endregion

        public AnalogModule()
        {
            this.Id = Guid.NewGuid();
            this.Projects = new HashSet<ProjectVersion>();
            this.Platforms = new HashSet<Platform>();
            this.Default = false;
        }

        public Func<AnalogModule, bool> GetEqualityPredicate()
        {
            //return (AnalogModule e) => e.Id == this.Id || e.DIVG == this.DIVG || e.Title == this.Title; - пока нет данных по ДИВГ-ам
            return (AnalogModule e) => e.Id == this.Id || e.Title == this.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.DIVG, this.Title, this.Current);
        }

        public override string ToString()
        {
            return $"ID = {this.Id}, модуль: {this.Title ?? ""}";
        }
    }
}
