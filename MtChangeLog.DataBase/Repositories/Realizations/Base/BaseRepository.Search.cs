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
        internal DbAnalogModule SearchInDataBase(DbAnalogModule other)
        {
            var result = this.context.AnalogModules
                .FirstOrDefault(e => e.Id == other.Id || e.DIVG == e.DIVG && e.Title == other.Title && e.Current == other.Current);
            return result;
        }

        internal DbArmEdit SearchInDataBase(DbArmEdit other)
        {
            var result = this.context.ArmEdits.
                FirstOrDefault(e => e.Id == other.Id || e.DIVG == other.DIVG && e.Version == other.Version);
            return result;
        }

        internal DbAuthor SearchInDataBase(DbAuthor other) 
        {
            var result = this.context.Authors
                .FirstOrDefault(e => e.Id == other.Id || e.FirstName == other.FirstName && e.LastName == other.LastName);
            return result;
        }

        internal DbCommunicationModule SearchInDataBase(DbCommunicationModule other) 
        {
            var result = this.context.CommunicationModules
                .FirstOrDefault(e => e.Id == other.Id || e.Title == other.Title);
            return result;
        }

        internal DbPlatform SearchInDataBase(DbPlatform other) 
        {
            var result = this.context.Platforms
                .FirstOrDefault(e => e.Id == other.Id || e.Title == other.Title);
            return result;
        }

        internal DbProjectRevision SearchInDataBase(DbProjectRevision other) 
        {
            var result = this.context.ProjectRevisions
                .Include(e => e.ProjectVersion)
                .FirstOrDefault(e => e.Id == other.Id 
                || e.Date == other.Date && e.Revision == other.Revision && e.Reason == other.Reason
                && (e.ProjectVersion.Id == other.ProjectVersion.Id 
                || e.ProjectVersion.DIVG == other.ProjectVersion.DIVG 
                && e.ProjectVersion.Title == other.ProjectVersion.Title 
                && e.ProjectVersion.Version == other.ProjectVersion.Version));
            return result;
        }

        internal DbProjectStatus SearchInDataBase(DbProjectStatus other) 
        {
            var result = this.context.ProjectStatuses
                .FirstOrDefault(e => e.Id == other.Id || e.Title == other.Title);
            return result;
        }

        internal DbProjectVersion SearchInDataBase(DbProjectVersion other) 
        {
            var result = this.context.ProjectVersions
                .FirstOrDefault(e => e.Id == other.Id || e.DIVG == other.DIVG && e.Title == other.Title && e.Version == other.Version);
            return result;
        }

        internal DbProtocol SearchInDataBase(DbProtocol other) 
        {
            var result = this.context.Protocols
                .FirstOrDefault(e => e.Id == other.Id || e.Title == other.Title);
            return result;
        }

        internal DbRelayAlgorithm SearchInDataBase(DbRelayAlgorithm other) 
        {
            var result = this.context.RelayAlgorithms
                .FirstOrDefault(e => e.Id == other.Id || e.Group == other.Group && e.Title == other.Title && e.ANSI == other.ANSI && e.LogicalNode == other.LogicalNode);
            return result;
        }
    }
}
