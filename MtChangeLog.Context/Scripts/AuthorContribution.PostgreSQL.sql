--Представление, общая статистика по авторам и их вкладам в проекты БМРЗ-100/120/150/160
CREATE OR REPLACE VIEW "MT"."AuthorContribution" AS
SELECT  CONCAT(athr."LastName", ' ', athr."FirstName") AS "Author",
        count(athr."Id") AS "Contribution"
FROM "MT"."Author" athr
JOIN "MT"."ProjectRevisionAuthor" pra
ON athr."Id" = pra."AuthorsId"
GROUP BY athr."LastName", athr."FirstName"
ORDER BY "Contribution" DESC;
