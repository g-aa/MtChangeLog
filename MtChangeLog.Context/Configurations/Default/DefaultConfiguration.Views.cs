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
        private static void CreateLastProjectsRevisionView(ApplicationContext context) 
        {
            if (context.Database.IsSqlite()) 
            {
                context.Database.ExecuteSqlRaw(
                @$"CREATE VIEW ""LastProjectsRevision"" AS                
                WITH LastRevision AS(
                SELECT  pr.""ProjectVersionId"",
                        Max(pr.""Revision"") AS ""Revision""
                FROM ""ProjectRevision"" pr
                GROUP BY pr.""ProjectVersionId""
                )
                SELECT  pr.""ProjectVersionId"" AS ""ProjectVersionId"",
		                pr.""Id"" AS ""ProjectRevisionId"",
		                pv.""Prefix"" AS ""Prefix"",
		                pv.""Title"" AS ""Title"",
		                pv.""Version"" AS ""Version"",
		                pr.""Revision"",
		                pl.""Title"" AS ""Platform"",
		                arm.""Version"" AS ""ArmEdit"",
		                pr.""Date""
                FROM LastRevision lr
                JOIN ""ProjectRevision"" pr
                ON lr.""Revision"" = pr.""Revision""
                AND lr.""ProjectVersionId"" = pr.""ProjectVersionId""
                JOIN ""ArmEdit"" arm
                ON arm.""Id"" = pr.""ArmEditId""
                JOIN ""ProjectVersion"" pv
                ON pv.""Id"" = pr.""ProjectVersionId""
                JOIN ""Platform"" pl
                ON pv.""PlatformId"" = pl.""Id""");
            }
            if (context.Database.IsNpgsql()) 
            {
                context.Database.ExecuteSqlRaw(
                @$"CREATE OR REPLACE VIEW ""MT"".""LastProjectsRevision"" AS
                WITH LastRevision AS(
                SELECT  pr.""ProjectVersionId"",
                        Max(pr.""Revision"") AS ""Revision""
                FROM ""MT"".""ProjectRevision"" pr
                GROUP BY pr.""ProjectVersionId""
                )
                SELECT  pr.""ProjectVersionId"" AS ""ProjectVersionId"",
                        pr.""Id"" AS ""ProjectRevisionId"",
                        pv.""Prefix"" AS ""Prefix"",
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
                JOIN ""MT"".""Platform"" pl
                ON pv.""PlatformId"" = pl.""Id""");
            }
        }

        private static void CreateAuthorContributionView(ApplicationContext context) 
        {
            if (context.Database.IsSqlite())
            {
                context.Database.ExecuteSqlRaw(
                @$"CREATE VIEW ""AuthorContribution"" AS
                SELECT  athr.""LastName"" || ' ' || athr.""FirstName"" AS ""Author"",
                        count(athr.""Id"") AS ""Contribution""
                FROM ""Author"" athr
                JOIN ""ProjectRevisionAuthor"" pra 
                ON athr.""Id"" = pra.""AuthorsId""
                GROUP BY athr.""LastName"", athr.""FirstName""
                ORDER BY ""Contribution"" DESC");
            }
            if (context.Database.IsNpgsql())
            {
                context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE VIEW ""MT"".""AuthorContribution"" AS
                SELECT  CONCAT(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                        count(athr.""Id"") AS ""Contribution""
                FROM ""MT"".""Author"" athr
                JOIN ""MT"".""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                GROUP BY athr.""LastName"", athr.""FirstName""
                ORDER BY ""Contribution"" DESC");
            }
        }

        private static void CreateAuthorProjectContribution(ApplicationContext context) 
        {
            if (context.Database.IsSqlite()) 
            {
                context.Database.ExecuteSqlRaw(
                $@"CREATE VIEW ""AuthorProjectContribution"" AS
                SELECT  athr.""LastName"" || ' ' || athr.""FirstName"" AS ""Author"",
                        pv.""Prefix"" AS ""ProjectPrefix"",
                        pv.""Title"" AS ""ProjectTitle"",
                        pv.""Version"" AS ""ProjectVersion"",
                        count(pv.""Id"") AS ""Contribution""
                FROM ""Author"" athr
                JOIN ""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                JOIN ""ProjectRevision"" pr
                ON pr.""Id"" = pra.""ProjectRevisionsId""
                JOIN ""ProjectVersion"" pv
                ON pv.""Id"" = pr.""ProjectVersionId""
                GROUP BY athr.""LastName"", athr.""FirstName"", pv.""Prefix"", pv.""Title"", pv.""Version""
                ORDER BY ""Author"" ASC, ""ProjectTitle"" ASC, ""ProjectPrefix"" ASC, ""ProjectVersion"" ASC");
            }
            if (context.Database.IsNpgsql()) 
            {
                context.Database.ExecuteSqlRaw(
                $@"CREATE OR REPLACE VIEW ""MT"".""AuthorProjectContribution"" AS
                SELECT  CONCAT(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                        pv.""Prefix"" AS ""ProjectPrefix"",
                        pv.""Title"" AS ""ProjectTitle"",
                        pv.""Version"" AS ""ProjectVersion"",
                        count(pv.""Id"") AS ""Contribution""
                FROM ""MT"".""Author"" athr
                JOIN ""MT"".""ProjectRevisionAuthor"" pra
                ON athr.""Id"" = pra.""AuthorsId""
                JOIN ""MT"".""ProjectRevision"" pr
                ON pr.""Id"" = pra.""ProjectRevisionsId""
                JOIN ""MT"".""ProjectVersion"" pv
                ON pv.""Id"" = pr.""ProjectVersionId""
                GROUP BY athr.""LastName"", athr.""FirstName"", pv.""Prefix"", pv.""Title"", pv.""Version""
                ORDER BY ""Author"" ASC, ""ProjectTitle"" ASC, ""ProjectPrefix"" ASC, ""ProjectVersion"" ASC");
            }
        }
    }
}
