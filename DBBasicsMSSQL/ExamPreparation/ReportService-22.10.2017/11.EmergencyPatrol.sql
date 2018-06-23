SELECT rep.OpenDate, rep.Description, [use].Email AS [Reporter Email]
FROM dbo.Reports rep
JOIN dbo.Categories cat ON rep.CategoryId = cat.Id
JOIN dbo.Users [use] ON rep.UserId = [use].Id
WHERE rep.CloseDate IS NULL AND LEN(rep.Description) > 20 AND rep.Description LIKE '%str%' AND cat.DepartmentId IN (1,4,5)
ORDER BY rep.OpenDate, [Reporter Email], rep.Id