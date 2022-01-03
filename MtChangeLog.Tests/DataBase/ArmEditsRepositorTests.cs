using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MtChangeLog.DataBase.Contexts;
using MtChangeLog.DataBase.Repositories.Interfaces;
using MtChangeLog.DataBase.Repositories.Realizations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MtChangeLog.Tests.DataBase
{
    public class ArmEditsRepositorTests
    {
        private readonly IArmEditsRepository repository;
        
        public ArmEditsRepositorTests() 
        {
            var builder = new ConfigurationBuilder();

            // установка пути к текущему каталогу:
            builder.SetBasePath(Directory.GetCurrentDirectory());

            // получаем конфигурацию из файла:
            builder.AddJsonFile("appsettings.json");

            // создаем конфигурацию
            var config = builder.Build();

            // получаем строку подключения:
            string sConnection = config.GetConnectionString("SqLiteDbConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            this.repository = new ArmEditsRepositor(new ApplicationContext(optionsBuilder.UseSqlite(sConnection).Options));
        }

        [Fact]
        public void GetEntities()
        {
            //// Arrange:

            //// Act:
            //var results = this.repository.GetEntities();

            //// Assert:
            //Assert.True(results.Any() || results.Count() == 0);
        }

        [Fact]
        public void AddNewEntity() 
        {
            //// Arrange:
            //var entity = new ArmEditBase()
            //{
            //    Date = DateTime.Now,
            //    DIVG = "ДИВГ.12345-67",
            //    Version = "v1.25.10.00",
            //    Description = "тестовый ArmEdit"
            //};

            //// Act:
            //this.repository.AddEntity(entity);
            //var result = this.repository.GetEntities().FirstOrDefault(e => e.Equals(entity));
                
            //// Assert:
            //Assert.Equal<ArmEditBase>(result, entity);
        }

        [Fact]
        public void AddContainedEntity() 
        {
            //// Arrange:
            //var entity = new ArmEditBase()
            //{
            //    Date = DateTime.Now,
            //    DIVG = "ДИВГ.12345-67",
            //    Version = "v1.25.10.00",
            //    Description = "тестовый ArmEdit"
            //};

            //// Assert:
            //Assert.Throws<ArgumentException>(() => 
            //{
            //    this.repository.AddEntity(entity);
            //});
        }

        [Fact]
        public void UpdateContainedEntity() 
        {
            //// Arrange:
            //var baseEntity = this.repository.GetEntities().First();

            //// Act:
            //var editableEntity = this.repository.GetEntity(baseEntity.Id);
            //editableEntity.Date = DateTime.Now;
            //editableEntity.Description = "измененное описание";
            //this.repository.UpdateEntity(editableEntity);
            //var updatedEntity = this.repository.GetEntity(baseEntity.Id);
            
            //// Assert:
            //Assert.Equal<ArmEditBase>(editableEntity, updatedEntity);
        }

        [Fact]
        public void UpdateNotContainedEntity() 
        {
            //// Arrange:
            //var editableEntity = new ArmEditBase()
            //{
            //    Date = DateTime.Now,
            //    DIVG = "ДИВГ.76543-21",
            //    Version = "v1.11.11.11",
            //    Description = "тестовый ArmEdit"
            //};

            //// Assert:
            //Assert.Throws<ArgumentException>(() => 
            //{
            //    this.repository.UpdateEntity(editableEntity);
            //});
        }

        [Fact]
        public void DeleteEntity()
        {
            //// Arrange:
            //var entity = new ArmEditBase()
            //{
            //    Date = DateTime.Now,
            //    DIVG = "ДИВГ.12345-67",
            //    Version = "v1.25.10.00",
            //    Description = "тестовый ArmEdit"
            //};

            //// Act:
            //var result = this.repository.GetEntities().FirstOrDefault(e => e.Equals(entity));
            //this.repository.DeleteEntity(result.Id);

            //// Assert:
            //Assert.Throws<ArgumentException>(() => 
            //{
            //    this.repository.GetEntity(result.Id);

            //});
        }
    }
}
