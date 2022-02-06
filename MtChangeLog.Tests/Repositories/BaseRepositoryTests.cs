using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MtChangeLog.Context.Realizations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Tests.Repositories
{
    public abstract class BaseRepositoryTests
    {
        protected ApplicationContext context;

        public BaseRepositoryTests() 
        {
            var builder = new ConfigurationBuilder();

            // установка пути к текущему каталогу:
            builder.SetBasePath(Environment.CurrentDirectory);

            // получаем конфигурацию из файла:
            builder.AddJsonFile("appsettings.json");

            // создаем конфигурацию
            var config = builder.Build();

            // получаем строку подключения:
            string sConnection = config.GetConnectionString("TestDbConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            this.context = new ApplicationContext(optionsBuilder.UseSqlite(sConnection).Options);
        }
    }
}
