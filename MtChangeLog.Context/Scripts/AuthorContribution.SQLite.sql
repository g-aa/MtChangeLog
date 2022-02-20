--Представление, общая статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160
DROP VIEW IF EXISTS "AuthorContribution";
CREATE VIEW IF NOT EXISTS "AuthorContribution" AS
SELECT  athr."LastName" || ' ' || athr."FirstName" AS "Author",
        count(athr."Id") AS "Contribution"
FROM "Author" athr
JOIN "ProjectRevisionAuthor" pra 
ON athr."Id" = pra."AuthorsId"
GROUP BY athr."LastName", athr."FirstName"
ORDER BY "Contribution" DESC;