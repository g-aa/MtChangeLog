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
    internal class ArmEditConfiguration : IEntityTypeConfiguration<DbArmEdit>
    {
        public void Configure(EntityTypeBuilder<DbArmEdit> builder)
        {
            builder.ToTable("ArmEdit");
            builder.HasComment("Таблица с перечнем ArmEdit используемых при компиляции проектов блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Version).HasDatabaseName("IX_ArmEdit_Version").IsUnique();
            //builder.HasIndex(e => e.DIVG).HasDatabaseName("IX_ArmEdit_DIVG").IsUnique(); //точных данных по ДИВГ нет

            
            builder.Property(e => e.DIVG)
                .HasDefaultValue("ДИВГ.55101-00")
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Version)
                .HasMaxLength(11)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Date)
                .HasDefaultValue(DateTime.MinValue)
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
