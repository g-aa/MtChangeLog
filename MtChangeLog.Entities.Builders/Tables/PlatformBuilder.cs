using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class PlatformBuilder
    {
        private readonly Platform entity;

        private string title;
        private string description;
        private IQueryable<AnalogModule> modules;

        public PlatformBuilder(Platform entity) 
        {
            this.entity = entity;
        }

        public PlatformBuilder SetAttributes(PlatformEditable editable)
        {
            
            this.title = editable?.Title;
            this.description = editable?.Description;
            return this;
        }

        public PlatformBuilder SetAnalogModules(IQueryable<AnalogModule> modules)
        {
            this.modules = modules;
            return this;
        }

        public Platform Build()
        { 
            var prohibModules = this.entity.AnalogModules.Except(modules).Where(e => e.Projects.Intersect(this.entity.Projects).Any()).Select(e => e.Title);
            if (prohibModules.Any())
            {
                throw new ArgumentException($"Следующие аналоговые модули: \"{string.Join(",", prohibModules)}\" используются в проектах (БФПО) и не могут быть исключены из состава программных платформ \"{this}\"");
            }
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.Title = this.title;
            this.entity.Description = this.description;
            // реляционные связи:
            this.entity.AnalogModules = modules.ToHashSet();
            // this.entity.Projects - не обновляется!
            return this.entity;
        }

        public static PlatformBuilder GetBuilder()
        {
            return new PlatformBuilder(new Platform());
        }
    }
}
