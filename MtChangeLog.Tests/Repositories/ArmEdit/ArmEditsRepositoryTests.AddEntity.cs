using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MtChangeLog.Tests.Repositories.ArmEdit
{
    public partial class ArmEditsRepositoryTests
    {
        [Fact]
        public void Test_05_AddNewEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Date = DateTime.Now,
                DIVG = "ДИВГ.12345-67",
                Version = "v1.25.10.00",
                Description = "тестовый ArmEdit"
            };

            // Act:
            this.repository.AddEntity(entity);
            var result = this.repository.GetTableEntities()
                .ToArray()
                .FirstOrDefault(e => e.DIVG == entity.DIVG);

            // Assert:
            Assert.Equal(result.DIVG, entity.DIVG);
        }

        [Fact]
        public void Test_06_AddContainedEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Id = Guid.Parse("2253DF84-DB52-454F-8B71-B7A7D72105D8"),
                Date = DateTime.Parse("2022-01-01 15:33:48"),
                DIVG = "ДИВГ.55101-00",
                Version = "v1.09.00.04",
                Description = "дата не указана"
            };

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.AddEntity(entity);
            });
            Assert.Equal($"Сущность \"{entity}\" уже содержится в БД", exception.Message);
        }

        [Fact]
        public void Test_07_AddDefaultEntity() 
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию"
            };

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.AddEntity(entity);
            });
            Assert.Equal($"Сущность \"{entity}\" уже содержится в БД", exception.Message);
        }
    }
}
