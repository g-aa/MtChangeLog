using Microsoft.EntityFrameworkCore;

using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationContext context;

        public BaseRepository(ApplicationContext context) 
        {
            this.context = context;
        }

        internal DbAnalogModule GetDbAnalogModule(Guid guid)
        {
            var dbAnalogModule = this.context.AnalogModules.Include(module => module.Platforms).FirstOrDefault(am => am.Id == guid);
            if (dbAnalogModule is null)
            {
                throw new ArgumentException($"The analog module under id = {guid} was not found in database");
            }
            return dbAnalogModule;
        }

        internal DbArmEdit GetDbArmEdit(Guid guid)
        {
            var dbArmEdit = this.context.ArmEdits.FirstOrDefault(arm => arm.Id == guid);
            if (dbArmEdit is null)
            {
                throw new ArgumentException($"The ArmEdit under id = {guid} was not found in database");
            }
            return dbArmEdit;
        }

        internal DbPlatform GetDbPlatform(Guid guid)
        {
            var dbPlatform = this.context.Platforms.Include(platform => platform.AnalogModules).FirstOrDefault(p => p.Id == guid);
            if (dbPlatform is null)
            {
                throw new ArgumentException($"The platform under id = {guid} was not found in database");
            }
            return dbPlatform;
        }

        internal DbProjectVersion GetDbProjectVersion(Guid guid)
        {
            var dbProject = this.context.ProjectVersions.Include(pv => pv.AnalogModule).Include(pv => pv.Platform).FirstOrDefault(p => p.Id == guid);
            if (dbProject is null)
            {
                throw new ArgumentException($"The project version under id = {guid} was not found in the database");
            }
            return dbProject;
        }

        internal DbProjectRevision GetDbProjectRevision(Guid guid)
        {
            throw new NotImplementedException();
        }

        internal ICollection<DbAnalogModule> GetDbAnalogModules(IEnumerable<Guid> guids) 
        {
            var dbAnalogModules = this.context.AnalogModules.Where(am => guids.Contains(am.Id));
            if (dbAnalogModules is null) 
            {
                throw new ArgumentException($"Not found analog modules by transmitted ids");
            }
            return dbAnalogModules.ToHashSet();
        }

        internal ICollection<DbArmEdit> GetDbArmEdits(IEnumerable<Guid> guids)
        {
            var dbArmEdits = this.context.ArmEdits.Where(am => guids.Contains(am.Id));
            if (dbArmEdits is null)
            {
                throw new ArgumentException($"Not found ArmEdits by transmitted ids");
            }
            return dbArmEdits.ToHashSet();
        }

        internal ICollection<DbPlatform> GetDbPlatforms(IEnumerable<Guid> guids) 
        {
            var dbPlatforms = this.context.Platforms.Where(p => guids.Contains(p.Id));
            if (dbPlatforms is null) 
            {
                throw new ArgumentException($"Not found platforms by transmitted ids");
            }
            return dbPlatforms.ToHashSet();
        }

        internal ICollection<DbProjectVersion> GetDbProjectVersions(IEnumerable<Guid> guids) 
        {
            var dbProjectVersions = this.context.ProjectVersions.Where(p => guids.Contains(p.Id));
            if (dbProjectVersions is null) 
            {
                throw new ArgumentException($"Not found project versions by transmitted ids");
            }
            return dbProjectVersions.ToHashSet();
        }

        internal ICollection<DbProjectRevision> GetDbProjectRevisions(Guid guids) 
        {
            throw new NotImplementedException();
        }
    }
}
