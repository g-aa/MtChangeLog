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
    internal class AnalogModuleConfiguration : IEntityTypeConfiguration<AnalogModule>
    {
        public void Configure(EntityTypeBuilder<AnalogModule> builder)
        {
            builder.ToTable("AnalogModule");
            builder.HasComment("Таблица с перечнем аналоговых модулей используемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_AnalogModule_Title").IsUnique();
            // builder.HasIndex(e => e.DIVG).HasDatabaseName("IX_AnalogModule_DIVG").IsUnique(); //точных данных по ДИВГ нет

            builder.HasMany(am => am.Platforms)
                .WithMany(p => p.AnalogModules)
                .UsingEntity(e => e.ToTable("PlatformAnalogModule"));

            builder.Property(e => e.DIVG)
                .HasDefaultValue("ДИВГ.00000-00")
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(10)
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
