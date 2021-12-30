using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtChangeLog.DataBase.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Contexts.EntitiesConfigurations
{
    internal class ProjectStatusConfiguration : IEntityTypeConfiguration<DbProjectStatus>
    {
        public void Configure(EntityTypeBuilder<DbProjectStatus> builder)
        {
            builder.ToTable("ProjectStatus");
            builder.HasComment("Таблица со статусами проектов (БФПО)");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_ProjectStatus_Title").IsUnique();

            builder.Property(e => e.Title)
                .HasMaxLength(32)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Default)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
