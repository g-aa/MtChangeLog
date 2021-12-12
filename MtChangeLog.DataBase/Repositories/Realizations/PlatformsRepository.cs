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

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public class PlatformsRepository : BaseRepository, IPlatformsRepository
    {
        public PlatformsRepository(ApplicationContext context) : base(context)
        {

        }

        public IEnumerable<PlatformShortView> GetShortEntities()
        {
            var result = this.context.Platforms.OrderBy(e => e.Title).Select(e => e.ToShortView());
            return result;
        }

        public IEnumerable<PlatformTableView> GetTableEntities() 
        {
            var result = this.context.Platforms.OrderBy(e => e.Title).Select(e => e.ToTableView());
            return result;
        }

        public PlatformEditable GetTemplate() 
        {
            var modules = this.context.AnalogModules.Where(e => e.Default)?.Select(e => e.ToShortView());
            var template = new PlatformEditable()
            {
                Id = Guid.Empty,
                Title = "БМРЗ-000",
                Description = "введите описание для платформы",
                AnalogModules = modules
            };
            return template;
        }

        public PlatformEditable GetEntity(Guid guid)
        {
            var dbPlatform = this.GetDbPlatform(guid);
            return dbPlatform.ToEditable();
        }

        public void AddEntity(PlatformEditable entity)
        {
            var dbPlatform = new DbPlatform(entity)
            {
                AnalogModules = this.GetDbAnalogModules(entity.AnalogModules.Select(module => module.Id))
            };
            if (this.context.Platforms.FirstOrDefault(platform => platform.Equals(dbPlatform)) != null)
            {
                throw new ArgumentException($"Platform {entity} is contained in the database");
            }
            this.context.Platforms.Add(dbPlatform);
            this.context.SaveChanges();
        }

        public void UpdateEntity(PlatformEditable entity)
        {
            DbPlatform dbPlatform = this.GetDbPlatform(entity.Id);
            if (dbPlatform.Default)
            {
                throw new ArgumentException($"Default entity {entity} can not by update");
            }
            dbPlatform.Update(entity, this.GetDbAnalogModules(entity.AnalogModules.Select(module => module.Id)));
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            throw new NotImplementedException("функционал не поддерживается");
            //DbPlatform dbPlatform = this.GetDbPlatform(guid);
            //this.context.Platforms.Remove(dbPlatform);
            //this.context.SaveChanges();
        }
    }
}
