using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;

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

        public IEnumerable<PlatformBase> GetEntities()
        {
            return this.context.Platforms.OrderBy(platform => platform.Title).Select(platform => platform.GetBase());
        }

        public PlatformEditable GetEntity(Guid guid)
        {
            var dbPlatform = this.GetDbPlatform(guid);
            return dbPlatform.GetEditable();
        }

        public void AddEntity(PlatformEditable entity)
        {
            if (this.context.Platforms.AsEnumerable().FirstOrDefault(platform => platform.Equals(entity)) != null)
            {
                throw new ArgumentException($"Platform {entity.Title} is contained in the database");
            }
            var dbPlatform = new DbPlatform(entity)
            {
                AnalogModules = this.GetDbAnalogModules(entity.AnalogModules.Select(module => module.Id))
            };
            this.context.Platforms.Add(dbPlatform);
            this.context.SaveChanges();
        }

        public void UpdateEntity(PlatformEditable entity)
        {
            DbPlatform dbPlatform = this.GetDbPlatform(entity.Id);
            dbPlatform.Update(entity, this.GetDbAnalogModules(entity.AnalogModules.Select(module => module.Id)));
            // dbPlatform.Projects - обновляются только через project !!!
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
