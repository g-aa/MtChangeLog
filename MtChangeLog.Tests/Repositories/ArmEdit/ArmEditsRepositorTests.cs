using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MtChangeLog.Abstractions.Repositories;
using MtChangeLog.Repositories.Realizations;
using MtChangeLog.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MtChangeLog.Tests.Repositories.ArmEdit
{
    public partial class ArmEditsRepositorTests : BaseRepositorTests
    {
        private readonly IArmEditsRepository repository;
        
        public ArmEditsRepositorTests() : base()
        {
            this.repository = new ArmEditsRepositor(this.context);
        }

        [Fact]
        public void GetShortEntities()
        {
            // Arrange:

            // Act:
            var results = this.repository.GetShortEntities();

            // Assert:
            Assert.True(results.Count() >= 0);
        }

        [Fact]
        public void GetTableEntities()
        {
            // Arrange:

            // Act:
            var results = this.repository.GetTableEntities();

            // Assert:
            Assert.True(results.Count() >= 0);
        }

    }
}
