using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataObjects.Entities.Base;
using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views;
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

        public IEnumerable<ProjectVersionShortView> GetShortEntities() 
        {
            var result = this.context.ProjectVersions
                .Include(pv => pv.AnalogModule)
                .Distinct()
                .OrderBy(pv => pv.AnalogModule.Title).ThenBy(pv => pv.Title).ThenBy(pv =>pv.Version)
                .Select(pv => pv.GetShortView());
            return result;
        }

        public IEnumerable<ProjectVersionView> GetEntities() 
        {
            return this.context.ProjectVersionViews;
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
                throw new ArgumentException($"The project version {entity.DIVG} {entity.Title} is contained in the database");
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
            var dbProjectVersion = this.GetDbProjectVersion(entity.Id);
            dbProjectVersion.Update(entity, this.GetDbAnalogModule(entity.AnalogModule.Id), this.GetDbPlatform(entity.Platform.Id));
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
