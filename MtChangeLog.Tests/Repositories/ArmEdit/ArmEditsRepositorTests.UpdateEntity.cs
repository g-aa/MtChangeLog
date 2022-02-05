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
        public void UpdateContainedEntity()
        {
            // Arrange:
            var baseEntity = this.repository.GetTableEntities().First();

            // Act:
            var editableEntity = this.repository.GetEntity(baseEntity.Id);
            editableEntity.Date = DateTime.Now;
            editableEntity.Description = "измененное описание";
            this.repository.UpdateEntity(editableEntity);
            var updatedEntity = this.repository.GetEntity(baseEntity.Id);

            // Assert:
            Assert.Equal<ArmEditEditable>(editableEntity, updatedEntity);
        }

        [Fact]
        public void UpdateNotContainedEntity()
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
            Assert.Throws<ArgumentException>(() =>
            {
                this.repository.UpdateEntity(editableEntity);
            });
        }
    }
}
