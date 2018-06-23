SELECT DISTINCT c.Name AS [Category Name]
FROM   Reports AS r
       JOIN Categories AS c ON c.Id = r.CategoryId
       JOIN Users AS u ON u.Id = r.UserId
	   WHERE DAY(r.OpenDate) = DAY (u.BirthDate) AND MONTH(r.OpenDate) = MONTH(u.BirthDate)
	   ORDER BY [Category Name]