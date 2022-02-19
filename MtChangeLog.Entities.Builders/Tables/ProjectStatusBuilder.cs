using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class ProjectStatusBuilder
    {
        private readonly ProjectStatus entity;

        private string title;
        private string description;

        public ProjectStatusBuilder(ProjectStatus entity) 
        {
            this.entity = entity;
        }

        public ProjectStatusBuilder SetAttributes(ProjectStatusEditable editable)
        {
            this.title = editable?.Title;
            this.description = editable?.Description;
            return this;
        }

        public ProjectStatus Build()
        {
            // атрибуты:
            //this.entity.Id - не обновляется!
            this.entity.Title = title;
            this.entity.Description = description;
            // реляционные связи:
            //this.entity.ProjectVersions - не обновляется!
            return entity;
        }

        public static ProjectStatusBuilder GetBuilder()
        {
            return new ProjectStatusBuilder(new ProjectStatus());
        }
    }
}
