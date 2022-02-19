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
    public class AnalogModulesRepository : IAnalogModulesRepository
    {
        private readonly ApplicationContext context;

        public AnalogModulesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<AnalogModuleShortView> GetShortEntities() 
        {
            var result = this.context.AnalogModules
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<AnalogModuleTableView> GetTableEntities() 
        {
            var result = this.context.AnalogModules
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .Select(e => e.ToTableView());
            return result;
        }

        public AnalogModuleEditable GetTemplate() 
        {
            var platforms = this.context.Platforms
                .AsNoTracking()
                .Where(p => p.Default)
                .Select(p => p.ToShortView());
            var result = new AnalogModuleEditable()
            {
                Id = Guid.Empty,
                DIVG = "ДИВГ.00000-00",
                Title = "БМРЗ-000",
                Current = "0A",
                Description = "введите описание для модуля",
                Platforms = platforms
            };
            return result;
        }

        public AnalogModuleEditable GetEntity(Guid guid) 
        {
            var result = this.context.AnalogModules
                .AsNoTracking()
                .Include(e => e.Platforms)
                .Search(guid)
                .ToEditable();
            return result;
        }

        public void AddEntity(AnalogModuleEditable entity) 
        {
            var dbPlatforms = this.context.Platforms
                .SearchManyOrDefault(entity.Platforms.Select(e => e.Id));
            var dbAnalogModule = AnalogModuleBuilder.GetBuilder()
                .SetAttributes(entity)
                .SetPlatforms(dbPlatforms)
                .Build();
            if (this.context.AnalogModules.IsContained(dbAnalogModule)) 
            {
                throw new ArgumentException($"Сущность \"{entity}\" уже содержится в БД");
            }
            this.context.AnalogModules.Add(dbAnalogModule);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AnalogModuleEditable entity) 
        {
            var dbAnalogModule = this.context.AnalogModules
                .Include(e => e.Projects)
                .Include(e => e.Platforms)
                .Search(entity.Id);
            if (dbAnalogModule.Default) 
            {
                throw new ArgumentException($"Сущность по умолчанию \"{entity}\" не может быть обновлена");
            }
            var dbPlatforms = this.context.Platforms
                .SearchManyOrDefault(entity.Platforms.Select(e => e.Id));
            dbAnalogModule.GetBuilder()
                .SetAttributes(entity)
                .SetPlatforms(dbPlatforms)
                .Build();
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            var dbRemovable = this.context.AnalogModules
                .Include(e => e.Projects)
                .Include(e => e.Platforms).ThenInclude(e => e.AnalogModules)
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
            if (dbRemovable.Platforms.Any()) 
            {
                var defModule = this.context.AnalogModules.First(e => e.Default);
                foreach (var dbPlatform in dbRemovable.Platforms)
                {
                    dbPlatform.AnalogModules.Remove(dbRemovable);
                    if (!dbPlatform.AnalogModules.Any()) 
                    {
                        dbPlatform.AnalogModules.Add(defModule);
                    }
                }
            }
            this.context.AnalogModules.Remove(dbRemovable);
            this.context.SaveChanges();
        }
    }
}
