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
    internal class ProjectRevisionConfiguration : IEntityTypeConfiguration<ProjectRevision>
    {
        public void Configure(EntityTypeBuilder<ProjectRevision> builder)
        {
            builder.ToTable("ProjectRevision");
            builder.HasComment("Таблица с перечнем ревизий (редакций) проектов блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => new { e.ProjectVersionId, e.Revision }).HasDatabaseName("IX_ProjectRevision_Revision").IsUnique();

            builder.HasMany(pr => pr.Authors)
                .WithMany(a => a.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionAuthor"));

            builder.HasMany(pr => pr.RelayAlgorithms)
                .WithMany(ra => ra.ProjectRevisions)
                .UsingEntity(e => e.ToTable("ProjectRevisionRelayAlgorithm"));

            builder.Property(e => e.Date)
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
