--"Представление с перечнем информации о отдельной редакции проекта (БФПО) БМРЗ-100/120/150/160"
CREATE OR REPLACE VIEW "MT"."ProjectHistoryRecord" AS
WITH SortProjectRevisionsAlgs AS (
SELECT 	ra."Title" AS "Algorithm",
		pra."ProjectRevisionsId"
FROM "MT"."RelayAlgorithm" ra
JOIN "MT"."ProjectRevisionRelayAlgorithm" pra
ON ra."Id" = pra."RelayAlgorithmsId"
ORDER BY pra."ProjectRevisionsId" ASC, ra."Title" ASC
),
ProjectRevisionsAlgs AS (
SELECT 	spra."ProjectRevisionsId" AS "ProjectRevisionId",
 		string_agg(spra."Algorithm",', ') AS "Algorithms"
FROM SortProjectRevisionsAlgs spra
GROUP BY spra."ProjectRevisionsId"
),
SortProjectRevisionsAuthors AS (
SELECT 	concat(athr."LastName", ' ', athr."FirstName") AS "Author",
		pra."ProjectRevisionsId"
FROM "MT"."Author" athr
JOIN "MT"."ProjectRevisionAuthor" pra
ON athr."Id" = pra."AuthorsId"
ORDER BY pra."ProjectRevisionsId" ASC, "Author" ASC
),
ProjectRevisionsAthrs AS (
SELECT 	spra."ProjectRevisionsId" AS "ProjectRevisionId",
 		string_agg(spra."Author",', ') AS "Authors"
FROM SortProjectRevisionsAuthors spra
GROUP BY spra."ProjectRevisionsId"
),
SortProjectRevisionsProtocols AS (
SELECT 	cmp."CommunicationModulesId",
		prot."Title" AS "Protocol"
FROM "MT"."Protocol" prot
JOIN "MT"."CommunicationModuleProtocol" cmp
ON prot."Id" = cmp."ProtocolsId"
ORDER BY cmp."CommunicationModulesId" ASC, prot."Title" ASC
),
ProjectRevisionsProtocols AS (
SELECT 	sprp."CommunicationModulesId" AS "CommunicationModuleId",
 		string_agg(sprp."Protocol",', ') AS "Protocols"
FROM SortProjectRevisionsProtocols sprp
GROUP BY sprp."CommunicationModulesId"
)
SELECT 	pv."Id" AS "ProjectVersionId",
		pr."ParentRevisionId",
		pr."Id" AS "ProjectRevisionId",
		plat."Title" AS "Platform",
		concat(pv."Prefix", '-', pv."Title", '-', pv."Version", '_', pr."Revision") AS "Title",
		pr."Date",
		arm."Version" AS "ArmEdit",
		prAlgs."Algorithms",
		prAthrs."Authors",
		prProts."Protocols",
		pr."Reason",
		pr."Description"
FROM "MT"."ProjectRevision" pr
JOIN "MT"."ProjectVersion" pv ON pv."Id" = pr."ProjectVersionId"
JOIN "MT"."ArmEdit" arm ON arm."Id" = pr."ArmEditId"
JOIN "MT"."Platform" plat ON plat."Id" = pv."PlatformId"
JOIN ProjectRevisionsAlgs prAlgs ON prAlgs."ProjectRevisionId" = pr."Id"
JOIN ProjectRevisionsAthrs prAthrs ON prAthrs."ProjectRevisionId" = pr."Id"
JOIN ProjectRevisionsProtocols prProts ON prProts."CommunicationModuleId" = pr."CommunicationModuleId";