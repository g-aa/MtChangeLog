--Представление, статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160
CREATE OR REPLACE VIEW "MT"."AuthorProjectContribution" AS
SELECT  CONCAT(athr."LastName", ' ', athr."FirstName") AS "Author",
        pv."Prefix" AS "ProjectPrefix",
        pv."Title" AS "ProjectTitle",
        pv."Version" AS "ProjectVersion",
        count(pv."Id") AS "Contribution"
FROM "MT"."Author" athr
JOIN "MT"."ProjectRevisionAuthor" pra
ON athr."Id" = pra."AuthorsId"
JOIN "MT"."ProjectRevision" pr
ON pr."Id" = pra."ProjectRevisionsId"
JOIN "MT"."ProjectVersion" pv
ON pv."Id" = pr."ProjectVersionId"
GROUP BY athr."LastName", athr."FirstName", pv."Prefix", pv."Title", pv."Version"
ORDER BY "Author" ASC, "ProjectTitle" ASC, "ProjectPrefix" ASC, "ProjectVersion" ASC;