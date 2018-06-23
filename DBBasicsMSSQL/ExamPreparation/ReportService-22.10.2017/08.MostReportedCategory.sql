SELECT cat.Name AS [CategoryName],
       COUNT(*) AS [ReportsNumber]
FROM   dbo.Categories cat
       JOIN dbo.Reports rep ON cat.Id = rep.CategoryId
GROUP BY cat.Name
ORDER BY ReportsNumber DESC,
         CategoryName;