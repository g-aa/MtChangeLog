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
        public Guid Id { get; set; }
        public string AnalogModule { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Revision { get; set; }
        public string Platform { get; set; }
        public string ArmEdit { get; set; }
        public DateTime Date { get; set; }

        public ProjectHistoryShortView ToHistoryShortView() 
        {
            return new ProjectHistoryShortView()
            {
                Id = this.Id,
                Date = this.Date,
                Platform = this.Platform,
                Title = $"{this.AnalogModule}-{this.Title}-{this.Version}_{this.Revision}"
            };
        }

        public override string ToString()
        {
            return $"{this.AnalogModule}-{this.Title}-{this.Version}_{this.Revision}";
        }
    }
}
