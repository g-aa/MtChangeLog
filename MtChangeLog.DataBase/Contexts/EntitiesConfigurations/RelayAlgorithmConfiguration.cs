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
    internal class RelayAlgorithmConfiguration : IEntityTypeConfiguration<DbRelayAlgorithm>
    {
        public void Configure(EntityTypeBuilder<DbRelayAlgorithm> builder)
        {
            builder.ToTable("RelayAlgorithm");
            builder.HasComment("Таблица с перечнем алгоритмов РЗиА поддерживаемых в блоках БМРЗ-100/120/150/160");

            builder.Property(e => e.Group)
                .HasMaxLength(32)
                .IsRequired();
            
            builder.Property(e => e.Title)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.ANSI)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.LogicalNode)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
