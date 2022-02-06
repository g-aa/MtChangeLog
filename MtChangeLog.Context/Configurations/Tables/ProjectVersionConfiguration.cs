using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtChangeLog.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Tables
{
    internal class ProjectVersionConfiguration : IEntityTypeConfiguration<ProjectVersion>
    {
        public void Configure(EntityTypeBuilder<ProjectVersion> builder)
        {
            builder.ToTable("ProjectVersion");
            builder.HasComment("Таблица с перечнем проектов блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.DIVG).HasDatabaseName("IX_ProjectVersion_DIVG").IsUnique();
            builder.HasIndex(e => new { e.AnalogModuleId, e.Title, e.Version }).HasDatabaseName("IX_ProjectVersion_Version").IsUnique();

            builder.Property(e => e.DIVG)
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.Version)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.AnalogModuleId)
                .IsRequired();

            builder.Property(e => e.PlatformId)
                .IsRequired();

            builder.Property(e => e.ProjectStatusId)
                .IsRequired();
        }
    }
}
