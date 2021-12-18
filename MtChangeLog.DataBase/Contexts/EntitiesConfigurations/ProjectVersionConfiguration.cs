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
    internal class ProjectVersionConfiguration : IEntityTypeConfiguration<DbProjectVersion>
    {
        public void Configure(EntityTypeBuilder<DbProjectVersion> builder)
        {
            builder.ToTable("ProjectVersion");
            builder.HasComment("Таблица с перечнем проектов блоков БМРЗ-100/120/150/160");

            builder.HasAlternateKey(e => e.DIVG).HasName("AK_ProjectVersion_DIVG");
            builder.HasAlternateKey(e => new { e.AnalogModuleId, e.Title, e.Version }).HasName("AK_ProjectVersion_Version");

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

            builder.Property(e => e.Status)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.AnalogModuleId)
                .IsRequired();

            builder.Property(e => e.PlatformId)
                .IsRequired();
        }
    }
}
