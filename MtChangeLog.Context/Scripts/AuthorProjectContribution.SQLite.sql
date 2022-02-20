--Представление, статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160
DROP VIEW IF EXISTS "AuthorProjectContribution";
CREATE VIEW IF NOT EXISTS "AuthorProjectContribution" AS
SELECT  athr."LastName" || ' ' || athr."FirstName" AS "Author",
        pv."Prefix" AS "ProjectPrefix",
        pv."Title" AS "ProjectTitle",
        pv."Version" AS "ProjectVersion",
        count(pv."Id") AS "Contribution"
FROM "Author" athr
JOIN "ProjectRevisionAuthor" pra
ON athr."Id" = pra."AuthorsId"
JOIN "ProjectRevision" pr
ON pr."Id" = pra."ProjectRevisionsId"
JOIN "ProjectVersion" pv
ON pv."Id" = pr."ProjectVersionId"
GROUP BY athr."LastName", athr."FirstName", pv."Prefix", pv."Title", pv."Version"
ORDER BY "Author" ASC, "ProjectTitle" ASC, "ProjectPrefix" ASC, "ProjectVersion" ASC;