using Microsoft.EntityFrameworkCore;
using MtChangeLog.DataBase.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Realizations.Base
{
    public abstract partial class BaseRepository
    {
        internal DbAnalogModule GetDbAnalogModuleOrDefault(Guid guid)
        {
            var dbAnalogModule = this.context.AnalogModules.FirstOrDefault(am => am.Id == guid);
            if (dbAnalogModule is null)
            {
                dbAnalogModule = this.context.AnalogModules.FirstOrDefault(am => am.Default);
            }
            if (dbAnalogModule is null)
            {
                throw new ArgumentException($"The analog module under id = {guid} or default was not found in database");
            }
            return dbAnalogModule;
        }

        internal DbArmEdit GetDbArmEditOrDefault(Guid guid)
        {
            var dbArmEdit = this.context.ArmEdits.FirstOrDefault(arm => arm.Id == guid);
            if (dbArmEdit is null)
            {
                dbArmEdit = this.context.ArmEdits.FirstOrDefault(arm => arm.Default);
            }
            if (dbArmEdit is null)
            {
                throw new ArgumentException($"The ArmEdit under id = {guid} or default was not found in database");
            }
            return dbArmEdit;
        }

        internal DbAuthor GetDbAuthorOrDefault(Guid guid)
        {
            var dbAuthor = this.context.Authors.FirstOrDefault(author => author.Id == guid);
            if (dbAuthor is null)
            {
                dbAuthor = this.context.Authors.FirstOrDefault(author => author.Default);
            }
            if (dbAuthor is null)
            {
                throw new ArgumentException($"The author under id = {guid} or default was not found in database");
            }
            return dbAuthor;
        }

        internal DbProtocol GetDbProtocolOrDefault(Guid guid)
        {
            var dbProtocol = this.context.Protocols.FirstOrDefault(p => p.Id == guid);
            if (dbProtocol is null)
            {
                dbProtocol = this.context.Protocols.FirstOrDefault(p => p.Default);
            }
            if (dbProtocol is null)
            {
                throw new ArgumentException($"The information protocol under id = {guid} or default was not found in database");
            }
            return dbProtocol;
        }

        internal DbCommunicationModule GetDbCommunicationOrDefault(Guid guid)
        {
            var dbCommunication = this.context.CommunicationModules.FirstOrDefault(com => com.Id == guid);
            if (dbCommunication is null)
            {
                dbCommunication = this.context.CommunicationModules.FirstOrDefault(com => com.Default);
            }
            if (dbCommunication is null)
            {
                throw new ArgumentException($"The communication under id = {guid} or default was not found in database");
            }
            return dbCommunication;
        }

        internal DbPlatform GetDbPlatformOrDefault(Guid guid)
        {
            var query = this.context.Platforms.Include(p => p.AnalogModules);
            var dbPlatform = query.FirstOrDefault(p => p.Id == guid);
            if (dbPlatform is null)
            {
                dbPlatform = query.FirstOrDefault(p => p.Default);
            }
            if (dbPlatform is null)
            {
                throw new ArgumentException($"The platform under id = {guid}  or default was not found in database");
            }
            return dbPlatform;
        }

        internal DbProjectStatus GetDbProjectStatusOrDefault(Guid guid)
        {
            var dbProjectStatus = this.context.ProjectStatuses.FirstOrDefault(ps => ps.Id == guid );
            if (dbProjectStatus is null)
            {
                dbProjectStatus = this.context.ProjectStatuses.FirstOrDefault(ps => ps.Default);
            }
            if (dbProjectStatus is null)
            {
                throw new ArgumentException($"The project status under id = {guid} or default was not found in the database");
            }
            return dbProjectStatus;
        }

        internal ICollection<DbAnalogModule> GetDbAnalogModulesOrDefault(IEnumerable<Guid> guids)
        {
            var dbAnalogModules = this.context.AnalogModules.Where(am => guids.Contains(am.Id));
            if (!dbAnalogModules.Any())
            {
                dbAnalogModules = this.context.AnalogModules.Where(am => am.Default);
            }
            if (dbAnalogModules is null)
            {
                throw new ArgumentException($"Not found analog modules by transmitted ids");
            }
            return dbAnalogModules.ToHashSet();
        }

        internal ICollection<DbArmEdit> GetDbArmEditsOrDefault(IEnumerable<Guid> guids)
        {
            var dbArmEdits = this.context.ArmEdits.Where(arm => guids.Contains(arm.Id));
            if (!dbArmEdits.Any())
            {
                dbArmEdits = this.context.ArmEdits.Where(arm => arm.Default);
            }
            if (dbArmEdits is null)
            {
                throw new ArgumentException($"Not found ArmEdits by transmitted ids");
            }
            return dbArmEdits.ToHashSet();
        }

        internal ICollection<DbAuthor> GetDbAuthorsOrDefault(IEnumerable<Guid> guids)
        {
            var dbAuthors = this.context.Authors.Where(a => guids.Contains(a.Id));
            if (!dbAuthors.Any())
            {
                dbAuthors = this.context.Authors.Where(a => a.Default);
            }
            if (dbAuthors is null)
            {
                throw new ArgumentException($"Not found authors by transmitted ids");
            }
            return dbAuthors.ToHashSet();
        }

        internal ICollection<DbPlatform> GetDbPlatformsOrDefault(IEnumerable<Guid> guids)
        {
            var dbPlatforms = this.context.Platforms.Where(p => guids.Contains(p.Id));
            if (!dbPlatforms.Any())
            {
                dbPlatforms = this.context.Platforms.Where(p => p.Default);
            }
            if (dbPlatforms is null)
            {
                throw new ArgumentException($"Not found platforms by transmitted ids");
            }
            return dbPlatforms.ToHashSet();
        }

        internal ICollection<DbProtocol> GetDbProtocolsOrDefault(IEnumerable<Guid> guids)
        {
            var dbProtocols = this.context.Protocols.Where(p => guids.Contains(p.Id));
            if (!dbProtocols.Any())
            {
                dbProtocols = this.context.Protocols.Where(p => p.Default);
            }
            if (!dbProtocols.Any())
            {
                throw new ArgumentException($"Not found informations protocols by transmitted ids or default");
            }
            return dbProtocols.ToHashSet();
        }

        internal ICollection<DbCommunicationModule> GetDbCommunicationModulesOrDefault(IEnumerable<Guid> guids)
        {
            var dbCommunications = this.context.CommunicationModules.Where(c => guids.Contains(c.Id));
            if (!dbCommunications.Any())
            {
                dbCommunications = this.context.CommunicationModules.Where(c => c.Default);
            }
            if (!dbCommunications.Any())
            {
                throw new ArgumentException($"Not found communication modules by transmitted ids or default");
            }
            return dbCommunications.ToHashSet();
        }
    }
}
