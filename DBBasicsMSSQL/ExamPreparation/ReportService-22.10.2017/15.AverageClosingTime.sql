SELECT d.Name, ISNULL(CONVERT(VARCHAR(10),AVG(DATEDIFF(DAY, rep.OpenDate, rep.CloseDate))), 'no info') AS [Average Duration]
FROM Departments AS d
JOIN dbo.Categories cat ON cat.DepartmentId = d.Id
JOIN dbo.Reports rep ON cat.Id = rep.CategoryId
GROUP BY d.Name