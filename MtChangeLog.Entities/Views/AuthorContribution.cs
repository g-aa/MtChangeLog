using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Views
{
    public class AuthorContribution
    {
        public string Author { get; set; }
        public int Contribution { get; set; }

        public AuthorContributionView ToView() 
        {
            var result = new AuthorContributionView()
            {
                Author = this.Author,
                Contribution = this.Contribution
            };
            return result;
        }
    }
}
