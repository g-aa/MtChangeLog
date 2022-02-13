using MtChangeLog.TransferObjects.Views.Shorts;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class LastProjectRevision
    {
        public Guid ProjectVersionId { get; set; }
        public Guid ProjectRevisionId { get; set; }
        public string Prefix { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }

        public LastProjectRevisionView ToView() 
        {
            var result = new LastProjectRevisionView()
            {
                Prefix = this.Prefix,
                Title = this.Title,
                Version = this.Version,
                Revision = this.Revision,
                Platform = this.Platform,
                ArmEdit = this.ArmEdit,
                Date = this.Date
            };
            return result;
        }

        public ProjectRevisionHistoryShortView ToHistoryShortView() 
        {
            var result = new ProjectRevisionHistoryShortView()
            {
                Id = this.ProjectRevisionId,
                Date = this.Date,
                Platform = this.Platform,
                Title = $"{this.Prefix}-{this.Title}-{this.Version}_{this.Revision}"
            };
            return result;
        }

        public ProjectVersionShortView ToProjectVersionShortView() 
        {
            var result = new ProjectVersionShortView()
            {
                Id = this.ProjectVersionId,
                Prefix = this.Prefix,
                Title = this.Title,
                Version = this.Version
            };
            return result;
        }

        public override string ToString()
        {
            return $"{this.Prefix}-{this.Title}-{this.Version}_{this.Revision}";
        }
    }
}
