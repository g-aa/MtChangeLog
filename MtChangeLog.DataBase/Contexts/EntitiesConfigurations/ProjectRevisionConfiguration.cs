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
    internal class ProjectRevisionConfiguration : IEntityTypeConfiguration<DbProjectRevision>
    {
        public void Configure(EntityTypeBuilder<DbProjectRevision> builder)
        {
            builder.ToTable("ProjectRevision");
            builder.HasComment("Таблица с перечнем ревизий (редакций) проектов блоков БМРЗ-100/120/150/160");

            builder.HasMany(pr => pr.Authors)
                .WithMany(a => a.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionAuthor"));

            builder.HasMany(pr => pr.RelayAlgorithms)
                .WithMany(ra => ra.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionRelayAlgorithm"));

            builder.Property(e => e.Date)
                .HasDefaultValue(DateTime.MinValue)
                .IsRequired();

            builder.Property(e => e.Revision)
                .HasMaxLength(2)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Reason)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(5000)
                .IsRequired();
        }
    }
}
