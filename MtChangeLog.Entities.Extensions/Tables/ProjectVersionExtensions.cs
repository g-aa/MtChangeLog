using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Tables
{
    public static class ProjectVersionExtensions
    {
        public static ProjectVersionShortView ToShortView(this ProjectVersion entity)
        {
            var result = new ProjectVersionShortView()
            {
                Id = entity.Id,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version
            };
            return result;
        }

        public static ProjectVersionTableView ToTableView(this ProjectVersion entity)
        {
            var result = new ProjectVersionTableView()
            {
                Id = entity.Id,
                DIVG = entity.DIVG,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Status = entity.ProjectStatus.Title,
                Version = entity.Version,
                Description = entity.Description,
                Module = entity.AnalogModule.Title,
                Platform = entity.Platform.Title
            };
            return result;
        }

        public static ProjectVersionEditable ToEditable(this ProjectVersion entity)
        {
            var result = new ProjectVersionEditable()
            {
                Id = entity.Id,
                DIVG = entity.DIVG,
                Prefix = entity.Prefix,
                Title = entity.Title,
                ProjectStatus = entity.ProjectStatus.ToShortView(),
                Version = entity.Version,
                Description = entity.Description,
                AnalogModule = entity.AnalogModule.ToShortView(),
                Platform = entity.Platform.ToShortView()
            };
            return result;
        }

        public static ProjectVersionBuilder GetBuilder(this ProjectVersion project)
        {
            return new ProjectVersionBuilder(project);
        }
    }
}
