using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Repositories.Realizations;
using MtChangeLog.Tests.Repositories;
using MtChangeLog.TransferObjects.Editable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MtChangeLog.Tests.Repositories.ArmEdit
{
    public partial class ArmEditsRepositoryTests : BaseRepositoryTests
    {
        private readonly IArmEditsRepository repository;
        
        public ArmEditsRepositoryTests() : base()
        {
            this.repository = new ArmEditsRepositor(this.context);
        }

        [Fact]
        public void Test_01_GetShortEntities()
        {
            // Arrange:

            // Act:
            var results = this.repository.GetShortEntities();

            // Assert:
            Assert.True(results.Count() >= 0);
        }

        [Fact]
        public void Test_02_GetTableEntities()
        {
            // Arrange:

            // Act:
            var results = this.repository.GetTableEntities();

            // Assert:
            Assert.True(results.Count() >= 0);
        }

        [Fact]
        public void Test_03_GetTemplate()
        {
            // Arrange:
            var templet = new ArmEditEditable()
            {
                Id = Guid.Empty,
                Date = DateTime.Now,
                DIVG = "ДИВГ.55101-00",
                Version = "v0.00.00.00",
                Description = "введите описание для ArmEdit"
            };

            // Act:
            var result = this.repository.GetTemplate();

            // Assert:
            Assert.Equal(templet.Version, result.Version);
        }

        [Fact]
        public void Test_04_GetEntity() 
        {
            // Arrange:
            var shortView = this.repository.GetShortEntities().First();

            // Act:
            var result = this.repository.GetEntity(shortView.Id);

            // Assert:
            Assert.Equal(shortView.Id, result.Id);
            Assert.Equal(shortView.Version, result.Version);
        }
    }
}
