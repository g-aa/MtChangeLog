using MtChangeLog.DataObjects.Entities.Views.Shorts;
using MtChangeLog.DataObjects.Entities.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Entities.Views
{
    internal class DbLastProjectRevision
    {
        public Guid VersionId { get; set; }
        public Guid RevisionId { get; set; }
        public string AnalogModule { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }

        public LastProjectRevisionView ToView() 
        {
            return new LastProjectRevisionView()
            {
                Module = this.AnalogModule,
                Title = this.Title,
                Version = this.Version,
                Revision = this.Revision,
                Platform = this.Platform,
                ArmEdit = this.ArmEdit,
                Date = this.Date.ToString("yyyy-MM-dd")
            };
        }

        public ProjectHistoryShortView ToHistoryShortView() 
        {
            return new ProjectHistoryShortView()
            {
                Id = this.RevisionId,
                Date = this.Date,
                Platform = this.Platform,
                Title = $"{this.AnalogModule}-{this.Title}-{this.Version}_{this.Revision}"
            };
        }

        public ProjectVersionShortView ToProjectVersionShortView() 
        {
            return new ProjectVersionShortView() 
            {
                Id = this.VersionId,
                Module = this.AnalogModule,
                Title = this.Title,
                Version = this.Version
            };
        }

        public override string ToString()
        {
            return $"{this.AnalogModule}-{this.Title}-{this.Version}_{this.Revision}";
        }
    }
}
