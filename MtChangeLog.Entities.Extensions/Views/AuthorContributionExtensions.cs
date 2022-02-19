using MtChangeLog.Entities.Views;
using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Entities.Extensions.Views
{
    public static class AuthorContributionExtensions
    {
        public static AuthorContributionView ToView(this AuthorContribution entity)
        {
            var result = new AuthorContributionView()
            {
                Author = entity.Author,
                Contribution = entity.Contribution
            };
            return result;
        }

        public static AuthorProjectContributionView ToView(this AuthorProjectContribution entity)
        {
            var result = new AuthorProjectContributionView()
            {
                Author = entity.Author,
                Contribution = entity.Contribution,
                ProjectPrefix = entity.ProjectPrefix,
                ProjectTitle = entity.ProjectTitle,
                ProjectVersion = entity.ProjectVersion
            };
            return result;
        }
    }
}
