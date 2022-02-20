using MtChangeLog.Entities.Views;
using MtChangeLog.TransferObjects.Views.Historical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Views
{
    public static class ProjectHistoryRecordExtensions
    {
        public static ProjectHistoryRecordShortView ToShortView(this ProjectHistoryRecord record) 
        {
            var result = new ProjectHistoryRecordShortView()
            {
                ProjectRevisionId = record.ProjectRevisionId,
                Title = record.Title,
                Date = record.Date,
                Platform = record.Platform
            };
            return result;
        }

        public static ProjectHistoryRecordView ToView(this ProjectHistoryRecord record) 
        {
            var result = new ProjectHistoryRecordView()
            {
                ProjectRevisionId = record.ProjectRevisionId,
                ParentRevisionId = record.ParentRevisionId,
                ProjectVersionId = record.ProjectVersionId,
                Title = record.Title,
                Date = record.Date,
                Platform = record.Platform,
                ArmEdit = record.ArmEdit,
                Authors = record.Authors,
                Protocols = record.Protocols,
                Algorithms = record.Algorithms,
                Reason = record.Reason,
                Description = record.Description
            };
            return result;
        }
    }
}
