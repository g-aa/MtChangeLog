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
    internal class CommunicationModuleConfiguration : IEntityTypeConfiguration<DbCommunicationModule>
    {
        public void Configure(EntityTypeBuilder<DbCommunicationModule> builder)
        {
            builder.ToTable("CommunicationModule");
            builder.HasComment("Таблица с перечнем коммуникационных модулей поддерживаемых в блоках БМРЗ-100/120/150/160");
            builder.HasIndex(e => e.Title).HasDatabaseName("IX_Communication_Title").IsUnique();

            builder.HasMany(ca => ca.Protocols)
                .WithMany(p => p.CommunicationModules)
                .UsingEntity(e => e.ToTable("CommunicationModuleProtocol"));

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
