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
                @$"DROP VIEW IF EXISTS ""LastProjectsRevision"";
                CREATE VIEW IF NOT EXISTS ""LastProjectsRevision"" AS                
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
                ON pv.""PlatformId"" = pl.""Id"";");
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
                ON pv.""PlatformId"" = pl.""Id"";");
            }
        }

        private static void CreateAuthorContributionView(ApplicationContext context) 
        {
            if (context.Database.IsSqlite())
            {
                context.Database.ExecuteSqlRaw(
                @$"DROP VIEW IF EXISTS ""AuthorContribution"";
                CREATE VIEW IF NOT EXISTS ""AuthorContribution"" AS
                SELECT  athr.""LastName"" || ' ' || athr.""FirstName"" AS ""Author"",
                        count(athr.""Id"") AS ""Contribution""
                FROM ""Author"" athr
                JOIN ""ProjectRevisionAuthor"" pra 
                ON athr.""Id"" = pra.""AuthorsId""
                GROUP BY athr.""LastName"", athr.""FirstName""
                ORDER BY ""Contribution"" DESC;");
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
                ORDER BY ""Contribution"" DESC;");
            }
        }

        private static void CreateAuthorProjectContribution(ApplicationContext context) 
        {
            if (context.Database.IsSqlite()) 
            {
                context.Database.ExecuteSqlRaw(
                $@"DROP VIEW IF EXISTS ""AuthorProjectContribution"";
                CREATE VIEW IF NOT EXISTS ""AuthorProjectContribution"" AS
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
                ORDER BY ""Author"" ASC, ""ProjectTitle"" ASC, ""ProjectPrefix"" ASC, ""ProjectVersion"" ASC;");
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
                ORDER BY ""Author"" ASC, ""ProjectTitle"" ASC, ""ProjectPrefix"" ASC, ""ProjectVersion"" ASC;");
            }
        }

        private static void CreateProjectHistoryRecord(ApplicationContext context) 
        {
            if (context.Database.IsSqlite())
            {
                context.Database.ExecuteSqlRaw(
                    $@"DROP VIEW IF EXISTS ""ProjectHistoryRecord"";
                    CREATE VIEW IF NOT EXISTS ""ProjectHistoryRecord"" AS
                    WITH SortProjectRevisionsAlgs AS(
                    SELECT  ra.""Title"" AS ""Algorithm"",
                            pra.""ProjectRevisionsId""
                    FROM ""RelayAlgorithm"" ra
                    JOIN ""ProjectRevisionRelayAlgorithm"" pra
                    ON ra.""Id"" = pra.""RelayAlgorithmsId""
                    ORDER BY pra.""ProjectRevisionsId"" ASC, ra.""Title"" ASC
                    ),
                    ProjectRevisionsAlgs AS(
                    SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                             GROUP_CONCAT(spra.""Algorithm"", ', ') AS ""Algorithms""
                    FROM SortProjectRevisionsAlgs spra
                    GROUP BY spra.""ProjectRevisionsId""
                    ),
                    SortProjectRevisionsAuthors AS(
                    SELECT  athr.""LastName"" || ' ' || athr.""FirstName"" AS ""Author"",
                            pra.""ProjectRevisionsId""
                    FROM ""Author"" athr
                    JOIN ""ProjectRevisionAuthor"" pra
                    ON athr.""Id"" = pra.""AuthorsId""
                    ORDER BY pra.""ProjectRevisionsId"" ASC, ""Author"" ASC
                    ),
                    ProjectRevisionsAthrs AS(
                    SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                             GROUP_CONCAT(spra.""Author"", ', ') AS ""Authors""
                    FROM SortProjectRevisionsAuthors spra
                    GROUP BY spra.""ProjectRevisionsId""
                    ),
                    SortProjectRevisionsProtocols AS(
                    SELECT  cmp.""CommunicationModulesId"",
                            prot.""Title"" AS ""Protocol""
                    FROM ""Protocol"" prot
                    JOIN ""CommunicationModuleProtocol"" cmp
                    ON prot.""Id"" = cmp.""ProtocolsId""
                    ORDER BY cmp.""CommunicationModulesId"" ASC, prot.""Title"" ASC
                    ),
                    ProjectRevisionsProtocols AS(
                    SELECT  sprp.""CommunicationModulesId"" AS ""CommunicationModuleId"",
                             GROUP_CONCAT(sprp.""Protocol"", ', ') AS ""Protocols""
                    FROM SortProjectRevisionsProtocols sprp
                    GROUP BY sprp.""CommunicationModulesId""
                    )
                    SELECT  pv.""Id"" AS ""ProjectVersionId"",
                            pr.""ParentRevisionId"",
                            pr.""Id"" AS ""ProjectRevisionId"",
                            plat.""Title"" AS ""Platform"",
                            pv.""Prefix"" || '-' || pv.""Title"" || '-' || pv.""Version"" || '_' || pr.""Revision"" AS ""Title"",
                            pr.""Date"",
                            arm.""Version"" AS ""ArmEdit"",
                            prAlgs.""Algorithms"",
                            prAthrs.""Authors"",
                            prProts.""Protocols"",
                            pr.""Reason"",
                            pr.""Description""
                    FROM ""ProjectRevision"" pr
                    JOIN ""ProjectVersion"" pv ON pv.""Id"" = pr.""ProjectVersionId""
                    JOIN ""ArmEdit"" arm ON arm.""Id"" = pr.""ArmEditId""
                    JOIN ""Platform"" plat ON plat.""Id"" = pv.""PlatformId""
                    JOIN ProjectRevisionsAlgs prAlgs ON prAlgs.""ProjectRevisionId"" = pr.""Id""
                    JOIN ProjectRevisionsAthrs prAthrs ON prAthrs.""ProjectRevisionId"" = pr.""Id""
                    JOIN ProjectRevisionsProtocols prProts ON prProts.""CommunicationModuleId"" = pr.""CommunicationModuleId"";");
            }
            if (context.Database.IsNpgsql()) 
            {
                context.Database.ExecuteSqlRaw(
                    $@"CREATE OR REPLACE VIEW ""MT"".""ProjectHistoryRecord"" AS
                    WITH SortProjectRevisionsAlgs AS(
                    SELECT  ra.""Title"" AS ""Algorithm"",
                            pra.""ProjectRevisionsId""
                    FROM ""MT"".""RelayAlgorithm"" ra
                    JOIN ""MT"".""ProjectRevisionRelayAlgorithm"" pra
                    ON ra.""Id"" = pra.""RelayAlgorithmsId""
                    ORDER BY pra.""ProjectRevisionsId"" ASC, ra.""Title"" ASC
                    ),
                    ProjectRevisionsAlgs AS(
                    SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                             string_agg(spra.""Algorithm"", ', ') AS ""Algorithms""
                    FROM SortProjectRevisionsAlgs spra
                    GROUP BY spra.""ProjectRevisionsId""
                    ),
                    SortProjectRevisionsAuthors AS(
                    SELECT  concat(athr.""LastName"", ' ', athr.""FirstName"") AS ""Author"",
                            pra.""ProjectRevisionsId""
                    FROM ""MT"".""Author"" athr
                    JOIN ""MT"".""ProjectRevisionAuthor"" pra
                    ON athr.""Id"" = pra.""AuthorsId""
                    ORDER BY pra.""ProjectRevisionsId"" ASC, ""Author"" ASC
                    ),
                    ProjectRevisionsAthrs AS(
                    SELECT  spra.""ProjectRevisionsId"" AS ""ProjectRevisionId"",
                             string_agg(spra.""Author"", ', ') AS ""Authors""
                    FROM SortProjectRevisionsAuthors spra
                    GROUP BY spra.""ProjectRevisionsId""
                    ),
                    SortProjectRevisionsProtocols AS(
                    SELECT  cmp.""CommunicationModulesId"",
                            prot.""Title"" AS ""Protocol""
                    FROM ""MT"".""Protocol"" prot
                    JOIN ""MT"".""CommunicationModuleProtocol"" cmp
                    ON prot.""Id"" = cmp.""ProtocolsId""
                    ORDER BY cmp.""CommunicationModulesId"" ASC, prot.""Title"" ASC
                    ),
                    ProjectRevisionsProtocols AS(
                    SELECT  sprp.""CommunicationModulesId"" AS ""CommunicationModuleId"",
                             string_agg(sprp.""Protocol"", ', ') AS ""Protocols""
                    FROM SortProjectRevisionsProtocols sprp
                    GROUP BY sprp.""CommunicationModulesId""
                    )
                    SELECT  pv.""Id"" AS ""ProjectVersionId"",
                            pr.""ParentRevisionId"",
                            pr.""Id"" AS ""ProjectRevisionId"",
                            plat.""Title"" AS ""Platform"",
                            concat(pv.""Prefix"", '-', pv.""Title"", '-', pv.""Version"", '_', pr.""Revision"") AS ""Title"",
                            pr.""Date"",
                            arm.""Version"" AS ""ArmEdit"",
                            prAlgs.""Algorithms"",
                            prAthrs.""Authors"",
                            prProts.""Protocols"",
                            pr.""Reason"",
                            pr.""Description""
                    FROM ""MT"".""ProjectRevision"" pr
                    JOIN ""MT"".""ProjectVersion"" pv ON pv.""Id"" = pr.""ProjectVersionId""
                    JOIN ""MT"".""ArmEdit"" arm ON arm.""Id"" = pr.""ArmEditId""
                    JOIN ""MT"".""Platform"" plat ON plat.""Id"" = pv.""PlatformId""
                    JOIN ProjectRevisionsAlgs prAlgs ON prAlgs.""ProjectRevisionId"" = pr.""Id""
                    JOIN ProjectRevisionsAthrs prAthrs ON prAthrs.""ProjectRevisionId"" = pr.""Id""
                    JOIN ProjectRevisionsProtocols prProts ON prProts.""CommunicationModuleId"" = pr.""CommunicationModuleId"";");
            }
        }
    }
}
