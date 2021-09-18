using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Entities;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataBase.Repositories.Realizations;


using System;
using System.Collections.Generic;
using System.IO;

namespace MtChangeLog.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            try
            {
                var builder = new ConfigurationBuilder();

                // установка пути к текущему каталогу
                builder.SetBasePath(Directory.GetCurrentDirectory());

                // получаем конфигурацию из файла appsettings.json
                builder.AddJsonFile("appsettings.json");

                // создаем конфигурацию
                var config = builder.Build();

                // получаем строку подключения
                string sConnection = config.GetConnectionString("SqLiteDbConnection");

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                
                var options = optionsBuilder.UseSqlite(sConnection).Options;

                /*
                using (ApplicationContext db = new ApplicationContext(options)) 
                {
                    IAnalogModuleRepository moduleRepository = new AnalogModuleRepository(db);                    
                    IPlatformsRepository platformsRepository = new PlatformsRepository(db);
                    IProjectsRepository projectsRepository = new ProjectsRepository(db);

                    // подключение модулей:
                    var am1 = new DbAnalogModule() { Title = "БМРЗ-None", DIVG = "ДИВГ.00000-00", Description = "", NominalCurrent = "0A" };
                    var am2 = new DbAnalogModule() { Title = "БМРЗ-152", DIVG = "ДИВГ.00000-00", Description = "", NominalCurrent = "5A" };
                    var am3 = new DbAnalogModule() { Title = "БМРЗ-162", DIVG = "ДИВГ.00000-00", Description = "", NominalCurrent = "1A" };

                    moduleRepository.AddEntity(am1);
                    moduleRepository.AddEntity(am2);
                    moduleRepository.AddEntity(am3);


                    //
                    var platform1 = new DbPlatform()
                    {
                        Title = "БМРЗ-100",
                        Description = ""
                    };
                    ((HashSet<DbAnalogModule>)platform1.AnalogModules).Add(am1);
                    ((HashSet<DbAnalogModule>)platform1.AnalogModules).Add(am2);
                    ((HashSet<DbAnalogModule>)platform1.AnalogModules).Add(am3);

                    var platform2 = new DbPlatform()
                    {
                        Title = "БМРЗ-100У",
                        Description = ""
                    };
                    ((HashSet<DbAnalogModule>)platform2.AnalogModules).Add(am1);
                    ((HashSet<DbAnalogModule>)platform2.AnalogModules).Add(am2);
                    ((HashSet<DbAnalogModule>)platform2.AnalogModules).Add(am3);

                    var platform3 = new DbPlatform()
                    {
                        Title = "БМРЗ-100М",
                        Description = ""
                    };
                    ((HashSet<DbAnalogModule>)platform3.AnalogModules).Add(am1);
                    ((HashSet<DbAnalogModule>)platform3.AnalogModules).Add(am2);
                    ((HashSet<DbAnalogModule>)platform3.AnalogModules).Add(am3);

                    platformsRepository.AddEntity(platform1);
                    platformsRepository.AddEntity(platform2);
                    platformsRepository.AddEntity(platform3);

                    //
                    var project1 = new DbProject()
                    {
                        Title = "КСЗ",
                        DIVG = "ДИВГ.70013-01",
                        Version = 1,

                        Platform = platform1,
                        AnalogModule = am2
                    };

                    var project2 = new DbProject()
                    {
                        Title = "КСЗ",
                        DIVG = "ДИВГ.70013-51",
                        Version = 51,

                        Platform = platform2,
                        AnalogModule = am2
                    };

                    projectsRepository.AddEntity(project1);
                    projectsRepository.AddEntity(project2);

                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[EXCEPTION] - {0}", ex.Message));
            }

        }
    }
}
