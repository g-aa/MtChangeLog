--Представление с перечнем информации о последних редакциях проектов БМРЗ-100/120/150/160
DROP VIEW IF EXISTS "LastProjectsRevision";
CREATE VIEW IF NOT EXISTS "LastProjectsRevision" AS                
WITH LastRevision AS (
SELECT  pr."ProjectVersionId",
        Max(pr."Revision") AS "Revision"
FROM "ProjectRevision" pr
GROUP BY pr."ProjectVersionId"
)
SELECT  pr."ProjectVersionId" AS "ProjectVersionId",
        pr."Id" AS "ProjectRevisionId",
        pv."Prefix" AS "Prefix",
        pv."Title" AS "Title",
        pv."Version" AS "Version",
        pr."Revision",
        pl."Title" AS "Platform",
        arm."Version" AS "ArmEdit",
        pr."Date"
FROM LastRevision lr
JOIN "ProjectRevision" pr
ON lr."Revision" = pr."Revision"
AND lr."ProjectVersionId" = pr."ProjectVersionId"
JOIN "ArmEdit" arm
ON arm."Id" = pr."ArmEditId"
JOIN "ProjectVersion" pv
ON pv."Id" = pr."ProjectVersionId"
JOIN "Platform" pl
ON pv."PlatformId" = pl."Id";