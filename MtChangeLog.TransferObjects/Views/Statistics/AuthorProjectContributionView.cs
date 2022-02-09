using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Views.Statistics
{
    public class AuthorProjectContributionView
    {
        public string Author { get; set; }
        public int Contribution { get; set; }
        public string AnalogModule { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectVersion { get; set; }
    }
}
