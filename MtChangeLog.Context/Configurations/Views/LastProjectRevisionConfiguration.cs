using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MtChangeLog.Entities.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Views
{
    internal class LastProjectRevisionConfiguration : IEntityTypeConfiguration<LastProjectRevision>
    {
        public void Configure(EntityTypeBuilder<LastProjectRevision> builder)
        {
            builder.ToView("LastProjectsRevision");
            builder.HasComment("Представление с перечнем информации о последних редакциях проектов БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}
