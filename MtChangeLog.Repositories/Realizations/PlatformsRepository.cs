using Microsoft.EntityFrameworkCore;
using MtChangeLog.Abstractions.Extensions;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Context.Realizations;
using MtChangeLog.Entities.Tables;
using MtChangeLog.TransferObjects.Editable;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MtChangeLog.Repositories.Realizations
{
    public class PlatformsRepository : IPlatformsRepository
    {
        private readonly ApplicationContext context;

        public PlatformsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<PlatformShortView> GetShortEntities()
        {
            var result = this.context.Platforms
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<PlatformTableView> GetTableEntities() 
        {
            var result = this.context.Platforms
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToTableView());
            return result;
        }

        public PlatformEditable GetTemplate() 
        {
            var modules = this.context.AnalogModules
                .AsNoTracking()
                .Where(e => e.Default)
                .Select(e => e.ToShortView());
            var result = new PlatformEditable()
            {
                Id = Guid.Empty,
                Title = "БМРЗ-000",
                Description = "введите описание для платформы",
                AnalogModules = modules
            };
            return result;
        }

        public PlatformEditable GetEntity(Guid guid)
        {
            var result = this.context.Platforms
                .AsNoTracking()
                .Include(e => e.AnalogModules)
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(PlatformEditable entity)
        {
            var dbAnalogModules = this.context.AnalogModules
                .SearchManyOrDefault(entity.AnalogModules.Select(e => e.Id))
                .ToHashSet();
            var dbPlatform = new Platform(entity)
            {
                AnalogModules = dbAnalogModules
            };
            if (this.context.Platforms.IsContained(dbPlatform))
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.Platforms.Add(dbPlatform);
            this.context.SaveChanges();
        }

        public void UpdateEntity(PlatformEditable entity)
        {
            var dbPlatform = this.context.Platforms
                .Include(e => e.Projects)
                .Include(e => e.AnalogModules).ThenInclude(e => e.Projects)
                .Search(entity.Id);
            var dbAnalogModules = this.context.AnalogModules
                .SearchManyOrDefault(entity.AnalogModules.Select(e => e.Id))
                .ToHashSet();
            dbPlatform.Update(entity, dbAnalogModules);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал по удалению платформы БМРЗ на данный момент не доступен");
        }
    }
}
