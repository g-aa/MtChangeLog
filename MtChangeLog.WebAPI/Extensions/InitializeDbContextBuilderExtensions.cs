using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MtChangeLog.Context.Configurations.Default;
using MtChangeLog.Context.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.WebAPI.Extensions
{
    public static class InitializeDbContextBuilderExtensions
    {
        public static IApplicationBuilder UseInitializeDbContext(this IApplicationBuilder app) 
        {
            var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var context = scope.ServiceProvider.GetService<ApplicationContext>()) 
            {
                DefaultConfiguration.InitializeConfiguration(context);
            }
            return app;
        }
    }
}
