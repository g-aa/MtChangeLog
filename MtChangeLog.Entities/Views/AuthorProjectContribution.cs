using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class AuthorProjectContribution
    {
        public string Name { get; set; }
        public string Project { get; set; }
        public int ContributionCount { get; set; }
    }
}
