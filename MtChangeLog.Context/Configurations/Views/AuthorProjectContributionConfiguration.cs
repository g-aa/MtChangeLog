using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtChangeLog.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Views
{
    public class AuthorProjectContributionConfiguration : IEntityTypeConfiguration<AuthorProjectContribution>
    {
        public void Configure(EntityTypeBuilder<AuthorProjectContribution> builder)
        {
            builder.ToView("AuthorProjectContribution");
            builder.HasComment("Представление, статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}
