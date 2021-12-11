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
    internal class ArmEditConfiguration : IEntityTypeConfiguration<DbArmEdit>
    {
        public void Configure(EntityTypeBuilder<DbArmEdit> builder)
        {
            builder.ToTable("ArmEdit");
            builder.HasComment("Таблица с перечнем ArmEdit используемых при компиляции проектов блоков БМРЗ-100/120/150/160");

            builder.Property(e => e.DIVG)
                .HasDefaultValue("ДИВГ.55101-00")
                .HasMaxLength(13)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Version)
                .HasDefaultValue("v0.00.00.00")
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
