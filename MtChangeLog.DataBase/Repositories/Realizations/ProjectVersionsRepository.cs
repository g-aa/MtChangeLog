using Microsoft.EntityFrameworkCore;

using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
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
                .Select(pv => pv.ToShortView());
            return result;
        }

        public IEnumerable<ProjectVersionTableView> GetTableEntities() 
        {
            var result = this.context.ProjectVersions
                .Include(pv => pv.AnalogModule)
                .Include(pv => pv.Platform)
                .OrderBy(pv => pv.AnalogModule.Title).ThenBy(pv => pv.Title).ThenBy(pv => pv.Version)
                .Select(pv => pv.ToTableView());
            return result;
        }

        public ProjectVersionEditable GetTemplate() 
        {
            var platform = this.context.Platforms.First(e => e.Default)?.ToShortView();
            var module = this.context.AnalogModules.First(e => e.Default)?.ToShortView();
            var template = new ProjectVersionEditable()
            {
                Id = Guid.Empty,
                DIVG = "ДИВГ.00000-00",
                Title = "КСЗ",
                Status = "Test",
                Version = "00",
                Description = "введите описание проекта",
                Platform = platform,
                AnalogModule = module
            };
            return template;
        }

        public IEnumerable<string> GetProjectTitles() 
        {
            var result = this.context.ProjectVersions.Select(pv => pv.Title).Distinct().OrderBy(s => s);
            return result;
        }

        public ProjectVersionEditable GetEntity(Guid guid)
        {
            var dbProjectVersion = this.GetDbProjectVersion(guid);
            return dbProjectVersion.ToEditable();
        }

        public void AddEntity(ProjectVersionEditable entity)
        {
            var dbProjectVersion = new DbProjectVersion(entity)
            {
                AnalogModule = this.GetDbAnalogModule(entity.AnalogModule.Id),
                Platform = this.GetDbPlatform(entity.Platform.Id)
            };
            if (this.context.ProjectVersions.FirstOrDefault(p => p.Equals(dbProjectVersion)) != null)
            {
                throw new ArgumentException($"The project version {entity.DIVG} {entity.Title} is contained in the database");
            }
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
            throw new NotImplementedException("функционал не поддерживается");
            //DbProjectVersion dbProjectVersion = this.GetDbProjectVersion(guid);
            //this.context.ProjectVersions.Remove(dbProjectVersion);
            //this.context.SaveChanges();
        }
    }
}
