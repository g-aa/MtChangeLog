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
    internal class CommunicationConfiguration : IEntityTypeConfiguration<DbCommunication>
    {
        public void Configure(EntityTypeBuilder<DbCommunication> builder)
        {
            builder.ToTable("Communication");
            builder.HasComment("Таблица с перечнем коммуникационных модулей поддерживаемых в блоках БМРЗ-100/120/150/160");

            builder.Property(e => e.Protocols)
                .HasDefaultValue("ModBus-MT")
                .HasMaxLength(250)
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
