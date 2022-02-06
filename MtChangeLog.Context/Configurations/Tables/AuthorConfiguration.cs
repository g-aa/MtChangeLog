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
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasComment("Таблица с перечнем авторов проектов и ревизий встраиваемого ПО блоков БМРЗ-100/120/150/160");
            builder.HasIndex(e => new { e.FirstName, e.LastName }).HasDatabaseName("IX_Author_Name").IsUnique();

            builder.Property(e => e.FirstName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.LastName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Position)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(e => e.Default)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
