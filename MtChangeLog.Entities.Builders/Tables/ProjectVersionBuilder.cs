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
        private readonly ProjectVersion project;

        public ProjectVersionBuilder(ProjectVersion project) 
        {
            this.project = project;
        }

        public ProjectVersionBuilder SetAttributes(ProjectVersionEditable editable)
        { 
            return this;
        }

        public ProjectVersionBuilder SetPlatform(Platform platform) 
        {
            return this;
        }

        public ProjectVersionBuilder SetAnalogModule(AnalogModule module) 
        {
            return this;
        }

        public ProjectVersionBuilder SetProjectStatus(ProjectStatus status) 
        {
            return this;
        }

        public ProjectVersion Build()
        {
            return this.project;
        }

        public static ProjectVersionBuilder GetBuilder() 
        {
            return new ProjectVersionBuilder(new ProjectVersion());
        }
    }

    public static class ProjectVersionExtension
    {
        public static ProjectVersionBuilder GetBuilder(this ProjectVersion project) 
        {
            return new ProjectVersionBuilder(project);
        }
    }
}
