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
        public void AddNewEntity()
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
                .FirstOrDefault(e => e.DIVG.Equals(entity.DIVG) && e.Version.Equals(entity.Version));

            // Assert:
            Assert.Equal<ArmEditEditable>(result, entity);
        }

        [Fact]
        public void AddContainedEntity()
        {
            // Arrange:
            var entity = new ArmEditEditable()
            {
                Date = DateTime.Now,
                DIVG = "ДИВГ.12345-67",
                Version = "v1.25.10.00",
                Description = "тестовый ArmEdit"
            };

            // Assert:
            Assert.Throws<ArgumentException>(() =>
            {
                this.repository.AddEntity(entity);
            });
        }

        [Fact]
        public void AddDefaultEntity() 
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
            Assert.Throws<ArgumentException>(() =>
            {
                this.repository.AddEntity(entity);
            });
        }
    }
}
