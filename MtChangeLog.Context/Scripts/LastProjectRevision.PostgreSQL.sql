--Представление с перечнем информации о последних редакциях проектов БМРЗ-100/120/150/160
CREATE OR REPLACE VIEW "MT"."LastProjectsRevision" AS
WITH LastRevision AS (
SELECT  pr."ProjectVersionId",
        Max(pr."Revision") AS "Revision"
FROM "MT"."ProjectRevision" pr
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
JOIN "MT"."ProjectRevision" pr
ON lr."Revision" = pr."Revision"
AND lr."ProjectVersionId" = pr."ProjectVersionId"
JOIN "MT"."ArmEdit" arm
ON arm."Id" = pr."ArmEditId"
JOIN "MT"."ProjectVersion" pv
ON pv."Id" = pr."ProjectVersionId"
JOIN "MT"."Platform" pl
ON pv."PlatformId" = pl."Id";