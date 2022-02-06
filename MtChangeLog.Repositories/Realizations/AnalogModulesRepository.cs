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
                .SearchManyOrDefault(entity.Platforms.Select(e => e.Id))
                .ToHashSet();
            var dbAnalogModule = new AnalogModule(entity)
            {
                Platforms = dbPlatforms
            };
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
                .Include(e => e.Platforms).ThenInclude(e => e.Projects)
                .Search(entity.Id);
            var dbPlatforms = this.context.Platforms
                .SearchManyOrDefault(entity.Platforms.Select(e => e.Id))
                .ToHashSet();
            dbAnalogModule.Update(entity, dbPlatforms);
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал по удалению аналогового модуля на данный момент не доступен");
            // проверить на значение по умолчанию;
            // проверить используется ли в платформах;
            // проверить используется ли в проекте;
            // не удалять модуль используемый в проектах;
            // если у платформы аналоговый модуль последний подставить в платформу значение по умолчанию;
            //DbAnalogModule dbAnalogModule = this.GetDbAnalogModule(guid);
            //if (dbAnalogModule.Default) 
            //{
            //    throw new ArgumentException($"Default entity {dbAnalogModule} can not by remove");
            //}
            //this.context.AnalogModules.Remove(dbAnalogModule);
            //this.context.SaveChanges();
        }
    }
}
