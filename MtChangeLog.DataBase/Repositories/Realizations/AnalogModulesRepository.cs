using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

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

        public IEnumerable<AnalogModuleBase> GetEntities() 
        {
            return this.context.AnalogModules.OrderBy(module => module.Title).Select(module => module.GetBase());
        }

        public AnalogModuleEditable GetEntity(Guid guid) 
        {
            var dbModule = this.GetDbAnalogModule(guid);
            return dbModule.GetEditable();
        }

        public void AddEntity(AnalogModuleEditable entity) 
        {
            if (this.context.AnalogModules.AsEnumerable().FirstOrDefault(module => module.Equals(entity)) != null) 
            {
                throw new ArgumentException($"AnalogModule {entity.DIVG} {entity.Title} is contained in database");
            }
            var dbModule = new DbAnalogModule(entity)
            {
                Platforms = this.GetDbPlatforms(entity.Platforms.Select(platform => platform.Id))
            };
            this.context.AnalogModules.Add(dbModule);
            this.context.SaveChanges();
        }

        public void UpdateEntity(AnalogModuleEditable entity) 
        {
            DbAnalogModule dbAnalogModule = this.GetDbAnalogModule(entity.Id);
            dbAnalogModule.Update(entity, this.GetDbPlatforms(entity.Platforms.Select(platform => platform.Id)));
            // dbAnalogModule.Projects - обновляются только через project !!!

            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid) 
        {
            throw new NotImplementedException("функционал не поддерживается");
            //DbAnalogModule dbAnalogModule = this.GetDbAnalogModule(guid);
            //this.context.AnalogModules.Remove(dbAnalogModule);
            //this.context.SaveChanges();
        }
    }
}
