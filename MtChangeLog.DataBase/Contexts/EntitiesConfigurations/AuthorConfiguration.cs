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
    internal class AuthorConfiguration : IEntityTypeConfiguration<DbAuthor>
    {
        public void Configure(EntityTypeBuilder<DbAuthor> builder)
        {
            builder.ToTable("Author");
            builder.HasComment("Таблица с перечнем авторов проектов и ревизий встраиваемого ПО блоков БМРЗ-100/120/150/160");

            builder.HasAlternateKey(e => new { e.FirstName, e.LastName }).HasName("AK_Author_Name");

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
