using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Builders.Tables;
using MtChangeLog.Entities.Extensions.Tables;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Repositories.Realizations
{
    public class ProjectVersionsRepository : IProjectVersionsRepository
    {
        private readonly ApplicationContext context;

        public ProjectVersionsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<ProjectVersionShortView> GetShortEntities() 
        {
            var result = this.context.ProjectVersions
                .AsNoTracking()
                .Include(e => e.AnalogModule)
                .OrderBy(e => e.AnalogModule.Title).ThenBy(e => e.Title).ThenBy(e =>e.Version)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<ProjectVersionTableView> GetTableEntities() 
        {
            var result = this.context.ProjectVersions
                .AsNoTracking()
                .Include(e => e.AnalogModule)
                .Include(e => e.Platform)
                .Include(e => e.ProjectStatus)
                .OrderBy(e => e.AnalogModule.Title).ThenBy(e => e.Title).ThenBy(e => e.Version)
                .Select(e => e.ToTableView());
            return result;
        }

        public ProjectVersionEditable GetTemplate() 
        {
            var status = this.context.ProjectStatuses
                .AsNoTracking()
                .First(e => e.Default)
                .ToShortView();
            var platform = this.context.Platforms
                .AsNoTracking()
                .First(e => e.Default)
                .ToShortView();
            var module = this.context.AnalogModules
                .AsNoTracking()
                .First(e => e.Default)
                .ToShortView();
            var result = new ProjectVersionEditable()
            {
                Id = Guid.Empty,
                DIVG = "ДИВГ.00000-00",
                Prefix = "БФПО",
                Title = "ПЛК",
                Version = "00",
                Description = "введите описание проекта",
                ProjectStatus = status,
                Platform = platform,
                AnalogModule = module
            };
            return result;
        }

        public ProjectVersionEditable GetEntity(Guid guid)
        {
            var result = this.context.ProjectVersions
                .AsNoTracking()
                .Include(e => e.AnalogModule)
                .Include(e => e.Platform)
                .Include(e => e.ProjectStatus)
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(ProjectVersionEditable entity)
        {
            var dbStatus = this.context.ProjectStatuses
                .SearchOrDefault(entity.ProjectStatus.Id);
            var dbPlatform = this.context.Platforms
                .Include(e => e.AnalogModules)
                .SearchOrDefault(entity.Platform.Id);
            var dbAnalogModule = dbPlatform.AnalogModules
                .Search(entity.AnalogModule.Id);
            var dbProjectVersion = ProjectVersionBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetProjectStatus(dbStatus)
                .SetPlatform(dbPlatform)
                .SetAnalogModule(dbAnalogModule)
                .Build();
            if (this.context.ProjectVersions.IsContained(dbProjectVersion))
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.ProjectVersions.Add(dbProjectVersion);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectVersionEditable entity)
        {
            var dbStatus = this.context.ProjectStatuses
               .SearchOrDefault(entity.ProjectStatus.Id);
            var dbPlatform = this.context.Platforms
                .Include(e => e.AnalogModules)
                .SearchOrDefault(entity.Platform.Id);
            var dbAnalogModule = dbPlatform.AnalogModules
                .Search(entity.AnalogModule.Id);
            var dbProjectVersion = this.context.ProjectVersions
                .Search(entity.Id)
                .GetBuilder()
                .SetAttributes(entity)
                .SetProjectStatus(dbStatus)
                .SetPlatform(dbPlatform)
                .SetAnalogModule(dbAnalogModule)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            var dbRemovable = this.context.ProjectVersions
                .Search(guid);
            this.context.ProjectVersions.Remove(dbRemovable);
            this.context.SaveChanges();
        }
    }
}
