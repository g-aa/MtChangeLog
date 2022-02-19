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
                .SearchManyOrDefault(entity.AnalogModules.Select(e => e.Id));
            var dbPlatform = PlatformBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetAnalogModules(dbAnalogModules)
                .Build();
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
                .Include(e => e.AnalogModules)
                .Search(entity.Id);
            if (dbPlatform.Default)
            {
                throw new ArgumentException($"Сущность по умолчанию \"{entity}\" не может быть обновлена");
            }
            var dbAnalogModules = this.context.AnalogModules
                .SearchManyOrDefault(entity.AnalogModules.Select(e => e.Id));
            dbPlatform.GetBuilder()
                .SetAttributes(entity)
                .SetAnalogModules(dbAnalogModules)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            var dbRemovable = this.context.Platforms
                .Include(e => e.Projects)
                .Include(e => e.AnalogModules).ThenInclude(e => e.Platforms)
                .AsSingleQuery()
                .Search(guid);
            if (dbRemovable.Default) 
            {
                throw new ArgumentException($"Сущность по умолчанию \"{dbRemovable}\" не может быть удалена из БД");
            }
            if (dbRemovable.Projects.Any())
            {
                throw new ArgumentException($"Сущность \"{dbRemovable}\" используемая в проектах не может быть удалена из БД");
            }
            if (dbRemovable.AnalogModules.Any()) 
            {
                var defPlatform = this.context.Platforms.First(e => e.Default);
                foreach (var dbModule in dbRemovable.AnalogModules)
                {
                    dbModule.Platforms.Remove(dbRemovable);
                    if (!dbModule.Platforms.Any()) 
                    {
                        dbModule.Platforms.Add(defPlatform);
                    }
                }
            }
            this.context.Platforms.Remove(dbRemovable);
            this.context.SaveChanges();
        }
    }
}
