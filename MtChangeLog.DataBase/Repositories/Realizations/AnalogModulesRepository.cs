using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities.Tables;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class AnalogModulesRepository : BaseRepository, IAnalogModulesRepository
    {
        public AnalogModulesRepository(ApplicationContext context) : base(context)
        {

        }

        public IQueryable<AnalogModuleShortView> GetShortEntities() 
        {
            var result = this.context.AnalogModules.OrderBy(e => e.Title).Select(e => e.ToShortView());
            return result;
        }

        public IQueryable<AnalogModuleTableView> GetTableEntities() 
        {
            var result = this.context.AnalogModules.OrderBy(e => e.Title).Select(e => e.ToTableView());
            return result;
        }

        public AnalogModuleEditable GetTemplate() 
        {
            var platforms = this.context.Platforms.Where(p => p.Default)?.Select(p => p.ToShortView());
            var template = new AnalogModuleEditable()
            {
                Id = Guid.Empty,
                DIVG = "ДИВГ.00000-00",
                Title = "БМРЗ-000",
                Current = "0A",
                Description = "введите описание для модуля",
                Platforms = platforms
            };
            return template;
        }

        public AnalogModuleEditable GetEntity(Guid guid) 
        {
            var dbModule = this.GetDbAnalogModule(guid);
            return dbModule.ToEditable();
        }

        public void AddEntity(AnalogModuleEditable entity) 
        {
            var dbModule = new DbAnalogModule(entity)
            {
                Platforms = this.GetDbPlatformsOrDefault(entity.Platforms.Select(platform => platform.Id))
            };
            if (this.context.AnalogModules.FirstOrDefault(module => module.Equals(dbModule)) != null) 
            {
                throw new ArgumentException($"AnalogModule {entity} is contained in database");
            }
            this.context.AnalogModules.Add(dbModule);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AnalogModuleEditable entity) 
        {
            DbAnalogModule dbAnalogModule = this.GetDbAnalogModule(entity.Id);
            dbAnalogModule.Update(entity, this.GetDbPlatformsOrDefault(entity.Platforms.Select(platform => platform.Id)));
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
