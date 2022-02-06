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
    internal class ProtocolConfiguration : IEntityTypeConfiguration<Protocol>
    {
        public void Configure(EntityTypeBuilder<Protocol> builder)
        {
            builder.ToTable("Protocol");
            builder.HasComment("Таблица с перечнем протоколов информационного обмена поддерживаемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_Protocol_Title").IsUnique();

            builder.Property(e => e.Title)
                .HasMaxLength(32)
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
