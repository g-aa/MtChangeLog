using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class AuthorProjectContribution
    {
        public string Author { get; set; }
        public int Contribution { get; set; }
        public string ProjectPrefix { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectVersion { get; set; }

        public AuthorProjectContributionView ToView() 
        {
            var result = new AuthorProjectContributionView()
            {
                Author = this.Author,
                Contribution = this.Contribution,
                ProjectPrefix = this.ProjectPrefix,
                ProjectTitle = this.ProjectTitle,
                ProjectVersion = this.ProjectVersion
            };
            return result;
        }
    }
}
