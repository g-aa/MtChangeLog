using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MtChangeLog.Tests.Repositories.ArmEdit
{
    public partial class ArmEditsRepositorTests
    {
        [Fact]
        public void DeleteContainedEntity()
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
            var result = this.repository.GetTableEntities()
                .FirstOrDefault(e => e.DIVG.Equals(entity.DIVG) && e.Version.Equals(entity.Version));

            // Assert:
            var exception = Assert.Throws<NotImplementedException>(() =>
            {
                this.repository.DeleteEntity(result.Id);
            });
            Assert.Equal("функционал по удалению ArmEdit на данный момент не доступен", exception.Message);
        }

        [Fact]
        public void DeleteNotContainedEntity() 
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55111-11",
                Version = "v1.11.11.11",
                Description = "ArmEdit не содержащийся в БД"
            };

            // Act:
            var result = this.repository.GetTableEntities()
                .FirstOrDefault(e => e.DIVG.Equals(entity.DIVG) && e.Version.Equals(entity.Version));

            // Assert:
            var exception = Assert.Throws<NotImplementedException>(() =>
            {
                this.repository.DeleteEntity(result.Id);
            });
            Assert.Equal("функционал по удалению ArmEdit на данный момент не доступен", exception.Message);
        }

        [Fact]
        public void DeleteDefaultEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Date = DateTime.MinValue,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "ArmEdit по умолчанию"
            };

            // Act:
            var result = this.repository.GetTableEntities()
                .FirstOrDefault(e => e.DIVG.Equals(entity.DIVG) && e.Version.Equals(entity.Version));

            // Assert:
            var exception = Assert.Throws<NotImplementedException>(() =>
            {
                this.repository.DeleteEntity(result.Id);
            });
            Assert.Equal("функционал по удалению ArmEdit на данный момент не доступен", exception.Message);
        }
    }
}
