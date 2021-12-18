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

            builder.HasAlternateKey(e => e.Title).HasName("AK_RelayAlgorithm_Title");
            //builder.HasAlternateKey(e => e.ANSI).HasName("AK_RelayAlgorithm_ANSI"); //точных данных по кодам ANSI нет
            //builder.HasAlternateKey(e => e.LogicalNode).HasName("AK_RelayAlgorithm_LN"); //точных данных по наименованию LN в 61850 нет

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
