using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtChangeLog.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Contexts.EntitiesConfigurations
{
    internal class AnalogModuleConfiguration : IEntityTypeConfiguration<DbAnalogModule>
    {
        public void Configure(EntityTypeBuilder<DbAnalogModule> builder)
        {
            builder.ToTable("AnalogModule");
            builder.HasComment("Таблица с перечнем аналоговых модулей используемых в блоках БМРЗ-100/120/150/160");

            builder.HasMany(am => am.Platforms)
                .WithMany(p => p.AnalogModules)
                .UsingEntity(e => e.ToTable("PlatformAnalogModule"));

            builder.Property(e => e.DIVG)
                .HasDefaultValue("ДИВГ.00000-00")
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasDefaultValue("БМРЗ-000")
                .HasMaxLength(8)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Current)
                .HasDefaultValue("0A")
                .HasMaxLength(2)
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
