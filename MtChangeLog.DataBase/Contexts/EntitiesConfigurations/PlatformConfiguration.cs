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
    internal class PlatformConfiguration : IEntityTypeConfiguration<DbPlatform>
    {
        public void Configure(EntityTypeBuilder<DbPlatform> builder)
        {
            builder.ToTable("Platform");
            builder.HasComment("Таблица с перечнем програмных платформ применяемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_Platform_Title").IsUnique();

            builder.Property(e => e.Title)
                .HasMaxLength(8)
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
