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
        public void Test_11_DeleteContainedEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Id = Guid.Parse("8128265D-4DB3-4F65-9546-EA2A24CA27F7"),
                Date = DateTime.Parse("2015-08-21 00:00:00"),
                DIVG = "ДИВГ.55101-00",
                Version = "v1.09.04.00",
                Description = ""
            };

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.DeleteEntity(entity.Id);
            });
            Assert.Equal("Сущность \"ArmEdit: ДИВГ.55101-00, v1.09.04.00\" используется в редакциях БФПО и неможет быть удалена из БД", exception.Message);
        }

        [Fact]
        public void Test_12_DeleteNotContainedEntity() 
        {
            // Arrange:
            var id = Guid.NewGuid();

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.DeleteEntity(id);
            });
            Assert.Equal($"Сущность с ID = \"{id}\" не найдена в БД", exception.Message);
        }

        [Fact]
        public void Test_13_DeleteDefaultEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Id = Guid.Parse("3E4DF70F-63EC-4101-8119-762B32464A27"),
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию"
            };

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.DeleteEntity(entity.Id);
            });
            Assert.Equal("Сущность по умолчанию \"ArmEdit: ДИВГ.55101-00, v0.00.00.00\" нельзя удалить из БД", exception.Message);
        }
    }
}
