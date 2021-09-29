using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataBase.Repositories.Realizations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtChangeLog.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
#if DEBUG
                string sSqLiteConnection = Configuration["ConnectionStrings:SqLiteDbConnection"];
                options.UseSqlite(sSqLiteConnection);
#else
                string sPgSqlConnection = Configuration["ConnectionStrings:PostgreSqlDbConnection"];
                options.UseNpgsql(sPgSqlConnection);
#endif
            });

            services.AddTransient<IAnalogModulesRepository, AnalogModulesRepository>();
            services.AddTransient<IArmEditsRepository, ArmEditsRepositor>();
            services.AddTransient<IAuthorsRepository, AuthorsRepository>();
            services.AddTransient<ICommunicationsRepository, CommunicationsRepository>();
            services.AddTransient<IPlatformsRepository, PlatformsRepository>();
            services.AddTransient<IProjectRevisionsRepository, ProjectRevisionsRepository>();
            services.AddTransient<IProjectVersionsRepository, ProjectVersionsRepository>();
            services.AddTransient<IRelayAlgorithmsRepository, RelayAlgorithmsRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
