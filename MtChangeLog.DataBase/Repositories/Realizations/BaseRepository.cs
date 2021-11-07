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

        internal DbAuthor GetDbAuthor(Guid guid)
        {
            var dbAuthor = this.context.Authors.FirstOrDefault(author => author.Id == guid);
            if (dbAuthor is null)
            {
                throw new ArgumentException($"The author under id = {guid} was not found in database");
            }
            return dbAuthor;
        }

        internal DbCommunication GetDbCommunication(Guid guid) 
        {
            var dbCommunication = this.context.Communications.FirstOrDefault(com => com.Id == guid);
            if (dbCommunication is null) 
            {
                throw new ArgumentException($"The communication under id = {guid} was not found in database");
            }
            return dbCommunication;
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
            var dbProjectVersion = this.context.ProjectVersions
                .Include(pv => pv.AnalogModule)
                .Include(pv => pv.Platform)
                .Include(pv => pv.ProjectRevisions).ThenInclude(pr => pr.ArmEdit)
                .Include(pv => pv.ProjectRevisions).ThenInclude(pr => pr.Communication)
                .Include(pv => pv.ProjectRevisions).ThenInclude(pr => pr.Authors)
                .Include(pv => pv.ProjectRevisions).ThenInclude(pr => pr.RelayAlgorithms)
                .FirstOrDefault(pv => pv.Id == guid);
            if (dbProjectVersion is null)
            {
                throw new ArgumentException($"The project version under id = {guid} was not found in the database");
            }
            return dbProjectVersion;
        }

        internal DbProjectRevision GetDbProjectRevision(Guid guid)
        {
            var dbProjectRevision = this.context.ProjectRevisions
                .Include(pr => pr.ParentRevision).ThenInclude(p => p.ProjectVersion).ThenInclude(pv => pv.AnalogModule)
                .Include(pr => pr.ProjectVersion).ThenInclude(pv => pv.AnalogModule)
                .Include(pr => pr.ProjectVersion).ThenInclude(pv => pv.Platform)
                .Include(pr => pr.ArmEdit)
                .Include(pr => pr.Communication)
                .Include(pr => pr.Authors)
                .Include(pr => pr.RelayAlgorithms)
                .FirstOrDefault(pr => pr.Id == guid);
            if (dbProjectRevision is null)
            {
                throw new ArgumentException($"The project revision under id = {guid} was not found in the database");
            }
            return dbProjectRevision;
        }

        internal DbRelayAlgorithm GetDbRelayAlgorithm(Guid guid) 
        {
            var dbAlgorithm = this.context.RelayAlgorithms.FirstOrDefault(alg => alg.Id == guid);
            if (dbAlgorithm is null) 
            {
                throw new ArgumentException($"The relay algorithm under id = {guid} was not found in database");
            }
            return dbAlgorithm;
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

        internal ICollection<DbAuthor> GetDbAuthors(IEnumerable<Guid> guids)
        {
            var dbAuthors = this.context.Authors.Where(a => guids.Contains(a.Id));
            if (dbAuthors is null)
            {
                throw new ArgumentException($"Not found authors by transmitted ids");
            }
            return dbAuthors.ToHashSet();
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

        internal ICollection<DbProjectRevision> GetDbProjectRevisions(IEnumerable<Guid> guids) 
        {
            var dbProjectRevisions = this.context.ProjectRevisions.Where(pr => guids.Contains(pr.Id));
            if (dbProjectRevisions is null)
            {
                throw new ArgumentException($"Not found project revisions by transmitted ids");
            }
            return dbProjectRevisions.ToHashSet();
        }

        internal ICollection<DbRelayAlgorithm> GetDbRelayAlgorithms(IEnumerable<Guid> guids) 
        {
            var dbAlgorithms = this.context.RelayAlgorithms.Where(alg => guids.Contains(alg.Id));
            if (dbAlgorithms is null)
            {
                throw new ArgumentException($"Not found algorithms by transmitted ids");
            }
            return dbAlgorithms.ToHashSet();
        }
    }
}
