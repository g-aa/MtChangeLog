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
        public void Test_08_UpdateContainedEntity()
        {
            // Arrange:
            var baseEntity = this.repository.GetTableEntities().ToArray()[3];

            // Act:
            var editableEntity = this.repository.GetEntity(baseEntity.Id);
            editableEntity.Date = DateTime.Now;
            editableEntity.Description = "измененное описание";
            this.repository.UpdateEntity(editableEntity);
            var updatedEntity = this.repository.GetEntity(baseEntity.Id);

            // Assert:
            Assert.Equal(editableEntity.Description, updatedEntity.Description);
        }

        [Fact]
        public void Test_09_UpdateNotContainedEntity()
        {
            // Arrange:
            var editableEntity = new ArmEditEditable()
            {
                Date = DateTime.Now,
                DIVG = "ДИВГ.76543-21",
                Version = "v1.11.11.11",
                Description = "ArmEdit не содержащийся в репозитории"
            };

            // Assert:
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                this.repository.UpdateEntity(editableEntity);
            });
        }

        [Fact]
        public void Test_10_UpdateDefaultEntity() 
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
                this.repository.UpdateEntity(entity);
            });
            Assert.Equal("Сущность по умолчанию \"ArmEdit: ДИВГ.55101-00, v0.00.00.00\" не может быть обновлена", exception.Message);
        }
    }
}
