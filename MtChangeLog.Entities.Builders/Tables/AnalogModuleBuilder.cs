using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class AnalogModuleBuilder
    {
        private readonly AnalogModule entity;

        private string divg;
        private string title;
        private string current;
        private string description;
        private IQueryable<Platform> platforms;

        public AnalogModuleBuilder(AnalogModule entity) 
        {
            this.entity = entity;
        }

        public AnalogModuleBuilder SetAttributes(AnalogModuleEditable editable) 
        {
            this.divg = editable?.DIVG;
            this.title = editable?.Title;
            this.current = editable?.Current;
            this.description = editable?.Description;
            return this;
        }

        public AnalogModuleBuilder SetPlatforms(IQueryable<Platform> platforms) 
        {
            this.platforms = platforms;
            return this;
        }

        public AnalogModule Build()
        {
            var prohibPlatforms = this.entity.Platforms.Except(this.platforms).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
            if (prohibPlatforms.Any())
            {
                throw new ArgumentException($"Следующие платформы: \"{string.Join(", ", prohibPlatforms)}\" используются в проектах (БФПО) и не могут быть исключены из состава аналогового модуля \"{this.entity}\"");
            }
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = this.divg;
            this.entity.Title = this.title;
            this.entity.Current = this.current;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.Platforms = platforms.ToHashSet();
            // this.entity.ProjectVersion - не обновляется!
            return this.entity;
        }

        public static AnalogModuleBuilder GetBuilder() 
        {
            return new AnalogModuleBuilder(new AnalogModule());
        }
    }
}
