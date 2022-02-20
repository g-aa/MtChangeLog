--Представление с перечнем информации о отдельной редакции проекта (БФПО) БМРЗ-100/120/150/160
DROP VIEW IF EXISTS "ProjectHistoryRecord";
CREATE VIEW IF NOT EXISTS "ProjectHistoryRecord" AS
WITH SortProjectRevisionsAlgs AS (
SELECT 	ra."Title" AS "Algorithm",
		pra."ProjectRevisionsId"
FROM "RelayAlgorithm" ra
JOIN "ProjectRevisionRelayAlgorithm" pra
ON ra."Id" = pra."RelayAlgorithmsId"
ORDER BY pra."ProjectRevisionsId" ASC, ra."Title" ASC
),
ProjectRevisionsAlgs AS (
SELECT 	spra."ProjectRevisionsId" AS "ProjectRevisionId",
 		GROUP_CONCAT(spra."Algorithm",', ') AS "Algorithms"
FROM SortProjectRevisionsAlgs spra
GROUP BY spra."ProjectRevisionsId"
),
SortProjectRevisionsAuthors AS (
SELECT 	athr."LastName" || ' ' || athr."FirstName" AS "Author",
		pra."ProjectRevisionsId"
FROM "Author" athr
JOIN "ProjectRevisionAuthor" pra
ON athr."Id" = pra."AuthorsId"
ORDER BY pra."ProjectRevisionsId" ASC, "Author" ASC
),
ProjectRevisionsAthrs AS (
SELECT 	spra."ProjectRevisionsId" AS "ProjectRevisionId",
 		GROUP_CONCAT(spra."Author",', ') AS "Authors"
FROM SortProjectRevisionsAuthors spra
GROUP BY spra."ProjectRevisionsId"
),
SortProjectRevisionsProtocols AS (
SELECT 	cmp."CommunicationModulesId",
		prot."Title" AS "Protocol"
FROM "Protocol" prot
JOIN "CommunicationModuleProtocol" cmp
ON prot."Id" = cmp."ProtocolsId"
ORDER BY cmp."CommunicationModulesId" ASC, prot."Title" ASC
),
ProjectRevisionsProtocols AS (
SELECT 	sprp."CommunicationModulesId" AS "CommunicationModuleId",
 		GROUP_CONCAT(sprp."Protocol",', ') AS "Protocols"
FROM SortProjectRevisionsProtocols sprp
GROUP BY sprp."CommunicationModulesId"
)
SELECT 	pv."Id" AS "ProjectVersionId",
		pr."ParentRevisionId",
		pr."Id" AS "ProjectRevisionId",
		plat."Title" AS "Platform",
		pv."Prefix" || '-' || pv."Title" || '-' || pv."Version" || '_' || pr."Revision" AS "Title",
		pr."Date",
		arm."Version" AS "ArmEdit",
		prAlgs."Algorithms",
		prAthrs."Authors",
		prProts."Protocols",
		pr."Reason",
		pr."Description"
FROM "ProjectRevision" pr
JOIN "ProjectVersion" pv ON pv."Id" = pr."ProjectVersionId"
JOIN "ArmEdit" arm ON arm."Id" = pr."ArmEditId"
JOIN "Platform" plat ON plat."Id" = pv."PlatformId"
JOIN ProjectRevisionsAlgs prAlgs ON prAlgs."ProjectRevisionId" = pr."Id"
JOIN ProjectRevisionsAthrs prAthrs ON prAthrs."ProjectRevisionId" = pr."Id"
JOIN ProjectRevisionsProtocols prProts ON prProts."CommunicationModuleId" = pr."CommunicationModuleId";