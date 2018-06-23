SELECT DISTINCT
       e.FirstName + ' ' + e.LastName AS [Name],
       COUNT(r.UserId) AS [Users Number]
FROM   Reports AS r
       RIGHT JOIN Employees AS e ON e.Id = r.EmployeeId
GROUP BY e.FirstName+' '+e.LastName
ORDER BY [Users Number] DESC,
         [Name];