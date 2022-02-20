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
    public class ProjectHistoryRecordConfiguration : IEntityTypeConfiguration<ProjectHistoryRecord>
    {
        public void Configure(EntityTypeBuilder<ProjectHistoryRecord> builder)
        {
            builder.ToView("ProjectHistoryRecord");
            builder.HasComment("Представление с перечнем информации о отдельной редакции проекта (БФПО) БМРЗ-100/120/150/160");
            builder.HasNoKey();
        }
    }
}
