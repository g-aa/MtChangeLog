using MtChangeLog.Context.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Default
{
    public static partial class DefaultConfiguration
    {
        public static void InitializeConfiguration(ApplicationContext context)
        {
            if (context.Database.EnsureCreated())
            {
                DefaultConfiguration.CreateDefaultEntities(context);

                DefaultConfiguration.CreateLastProjectsRevisionView(context);
                DefaultConfiguration.CreateAuthorContributionView(context);
                DefaultConfiguration.CreateAuthorProjectContribution(context);
                DefaultConfiguration.CreateProjectHistoryRecord(context);

                context.SaveChanges();
            }
        }
    }
}
