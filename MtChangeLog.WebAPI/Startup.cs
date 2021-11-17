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
using MtChangeLog.WebAPI.Converters;
using MtChangeLog.WebAPI.Loggers;
using System;
using System.Collections.Generic;
using System.IO;
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
                switch (this.Configuration["SqlProvider"].ToLower())
                {
                    case "postgresql":
                        string sPgSqlConnection = Configuration["ConnectionStrings:PostgreSqlDbConnection"];
                        options.UseNpgsql(sPgSqlConnection);
                        break;
                    case "sqlite":
                    default:
                        string sSqLiteConnection = Configuration["ConnectionStrings:SqLiteDbConnection"];
                        options.UseSqlite(sSqLiteConnection);
                        break;
                }
            });

            services.AddTransient<IAnalogModulesRepository, AnalogModulesRepository>();
            services.AddTransient<IArmEditsRepository, ArmEditsRepositor>();
            services.AddTransient<IAuthorsRepository, AuthorsRepository>();
            services.AddTransient<ICommunicationsRepository, CommunicationsRepository>();
            services.AddTransient<IPlatformsRepository, PlatformsRepository>();
            services.AddTransient<IProjectRevisionsRepository, ProjectRevisionsRepository>();
            services.AddTransient<IProjectVersionsRepository, ProjectVersionsRepository>();
            services.AddTransient<IRelayAlgorithmsRepository, RelayAlgorithmsRepository>();

            services.AddControllers().AddJsonOptions(configure => 
            {
                configure.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile();
            
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
