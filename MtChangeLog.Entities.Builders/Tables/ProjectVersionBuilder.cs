using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Builders.Tables
{
    public class ProjectVersionBuilder
    {
        private readonly ProjectVersion entity;

        private string divg;
        private string prefix;
        private string title;
        private string version;
        private string description;
        private Platform platform;
        private AnalogModule module;
        private ProjectStatus status;

        public ProjectVersionBuilder(ProjectVersion project) 
        {
            this.entity = project;
        }

        public ProjectVersionBuilder SetAttributes(ProjectVersionEditable editable)
        {
            this.divg = editable?.DIVG;
            this.prefix = editable?.Prefix;
            this.title = editable?.Title;
            this.version = editable?.Version;
            this.description = editable?.Description;
            return this;
        }

        public ProjectVersionBuilder SetPlatform(Platform platform) 
        {
            this.platform = platform;
            return this;
        }

        public ProjectVersionBuilder SetAnalogModule(AnalogModule module) 
        {
            this.module = module;
            return this;
        }

        public ProjectVersionBuilder SetProjectStatus(ProjectStatus status) 
        {
            this.status = status;
            return this;
        }

        public ProjectVersion Build()
        {
            // атрибуты:
            // this.entity.Id - не обновляется!
            this.entity.DIVG = divg;
            this.entity.Prefix = string.IsNullOrEmpty(this.prefix) ? this.module.Title.Replace("БМРЗ", "БФПО") : this.prefix;
            this.entity.Title = title;
            this.entity.Version = version;
            this.entity.Description = description;
            // реляционные связи:
            this.entity.AnalogModule = module;
            this.entity.Platform = platform;
            this.entity.ProjectStatus = status;
            // this.entity.ProjectRevisions - не обновляется!
            return this.entity;
        }

        public static ProjectVersionBuilder GetBuilder() 
        {
            return new ProjectVersionBuilder(new ProjectVersion());
        }
    }
}
