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
    public class ProjectVersionsRepository : BaseRepository, IProjectVersionsRepository
    {
        public ProjectVersionsRepository(ApplicationContext context) : base(context)
        {
            
        }

        public IEnumerable<ProjectVersionBase> GetEntities()
        {
            return this.context.ProjectVersions.OrderBy(p => p.Title).ThenBy(p => p.Version).Select(p => p.GetBase());
        }

        public ProjectVersionEditable GetEntity(Guid guid)
        {
            var dbProjectVersion = this.GetDbProjectVersion(guid);
            return dbProjectVersion.GetEditable();
        }

        public void AddEntity(ProjectVersionEditable entity)
        {
            if (this.context.ProjectVersions.AsEnumerable().FirstOrDefault(p => p.Equals(entity)) != null)
            {
                throw new ArgumentException($"The platform version {entity.DIVG} {entity.Title} is contained in the database");
            }
            var dbProjectVersion = new DbProjectVersion(entity) 
            {
                AnalogModule = this.GetDbAnalogModule(entity.AnalogModule.Id),
                Platform = this.GetDbPlatform(entity.Platform.Id)
            };
            this.context.ProjectVersions.Add(dbProjectVersion);
            this.context.SaveChanges();
        }

        public void UpdateEntity(ProjectVersionEditable entity)
        {
            DbProjectVersion dbProjectVersion = this.GetDbProjectVersion(entity.Id);
            dbProjectVersion.Update(entity, this.GetDbAnalogModule(entity.Id), this.GetDbPlatform(entity.Id));
            this.context.SaveChanges();
        }

        public void DeleteEntity(Guid guid)
        {
            DbProjectVersion dbProjectVersion = this.GetDbProjectVersion(guid);
            this.context.ProjectVersions.Remove(dbProjectVersion);
            this.context.SaveChanges();
        }
    }
}
