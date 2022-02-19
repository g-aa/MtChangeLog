using MtChangeLog.Entities.Views;
using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Views
{
    public static class LastProjectRevisionExtensions
    {
        public static LastProjectRevisionView ToView(this LastProjectRevision entity)
        {
            var result = new LastProjectRevisionView()
            {
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version,
                Revision = entity.Revision,
                Platform = entity.Platform,
                ArmEdit = entity.ArmEdit,
                Date = entity.Date
            };
            return result;
        }

        public static ProjectRevisionHistoryShortView ToHistoryShortView(this LastProjectRevision entity)
        {
            var result = new ProjectRevisionHistoryShortView()
            {
                Id = entity.ProjectRevisionId,
                Date = entity.Date,
                Platform = entity.Platform,
                Title = entity.ToString()
            };
            return result;
        }

        public static ProjectVersionShortView ToProjectVersionShortView(this LastProjectRevision entity)
        {
            var result = new ProjectVersionShortView()
            {
                Id = entity.ProjectVersionId,
                Prefix = entity.Prefix,
                Title = entity.Title,
                Version = entity.Version
            };
            return result;
        }
    }
}
