using Microsoft.EntityFrameworkCore;
using MtChangeLog.Context.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Context.Configurations.Default
{
    public static partial class DefaultConfiguration
    {
        private static void CreateViews(ApplicationContext context) 
        {
            if (context.Database.IsNpgsql()) 
            {
                DefaultConfiguration.CreateNpgsqlViews(context);
            }
            if (context.Database.IsSqlite()) 
            {
                DefaultConfiguration.CreateSqliteViews(context);
            }
        }

        private static void CreateNpgsqlViews(ApplicationContext context)
        {
            context.Database.ExecuteSqlRaw(
            @$"CREATE VIEW ""MT"".""LastProjectsRevision"" AS
            WITH LastRevision AS(
            SELECT  pr.""ProjectVersionId"",
                    Max(pr.""Revision"") AS ""Revision""
            FROM ""MT"".""ProjectRevision"" pr
            GROUP BY pr.""ProjectVersionId""
            )
            SELECT  pr.""ProjectVersionId"" AS ""VersionId"",
                    pr.""Id"" AS ""RevisionId"",
                    am.""Title"" AS ""AnalogModule"",
                    pv.""Title"" AS ""Title"",
                    pv.""Version"" AS ""Version"",
                    pr.""Revision"",
                    pl.""Title"" AS ""Platform"",
                    arm.""Version"" AS ""ArmEdit"",
                    pr.""Date""
            FROM LastRevision lr
            JOIN ""MT"".""ProjectRevision"" pr
            ON lr.""Revision"" = pr.""Revision""
            AND lr.""ProjectVersionId"" = pr.""ProjectVersionId""
            JOIN ""MT"".""ArmEdit"" arm
            ON arm.""Id"" = pr.""ArmEditId""
            JOIN ""MT"".""ProjectVersion"" pv
            ON pv.""Id"" = pr.""ProjectVersionId""
            JOIN ""MT"".""AnalogModule"" am
            ON am.""Id"" = pv.""AnalogModuleId""
            JOIN ""MT"".""Platform"" pl
            ON pv.""PlatformId"" = pl.""Id""");            
        }

        private static void CreateSqliteViews(ApplicationContext context)
        {
            context.Database.ExecuteSqlRaw(
            @$"CREATE VIEW LastProjectsRevision AS
            WITH LastRevision AS(
            SELECT  pr.ProjectVersionId,
		            Max(pr.Revision) AS Revision
            FROM ProjectRevision pr
            GROUP BY pr.ProjectVersionId
            )
            SELECT  pr.ProjectVersionId AS VersionId,
		            pr.Id AS RevisionId,
		            am.Title AS AnalogModule,
		            pv.Title AS Title,
		            pv.Version AS Version,
		            pr.Revision,
		            pl.Title AS Platform,
		            arm.Version AS ArmEdit,
		            pr.Date
            FROM LastRevision lr
            JOIN ProjectRevision pr
            ON lr.Revision = pr.Revision
            AND lr.ProjectVersionId = pr.ProjectVersionId
            JOIN ArmEdit arm
            ON arm.Id = pr.ArmEditId
            JOIN ProjectVersion pv
            ON pv.Id = pr.ProjectVersionId
            JOIN AnalogModule am
            ON am.Id = pv.AnalogModuleId
            JOIN Platform pl
            ON pv.PlatformId = pl.Id");
        }
    }
}
