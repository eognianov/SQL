SELECT e.FirstName, e.LastName, r.Description, FORMAT(r.OpenDate, 'yyyy-MM-dd') AS [OpenDate]
FROM Reports AS r
JOIN Employees AS e
ON e.Id = r.EmployeeId
WHERE EmployeeId IS NOT NULL
ORDER BY e.Id, r.OpenDate, r.Id