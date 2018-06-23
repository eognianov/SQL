SELECT DISTINCT u.Username FROM Reports AS r
JOIN Categories AS c
ON c.Id = r.CategoryId
JOIN Users AS u
ON u.Id = r.UserId
WHERE LEFT(u.Username,1) LIKE '[0-9]'
AND CONVERT(VARCHAR(10),c.Id) = LEFT(u.Username,1)
OR RIGHT(u.Username,1) LIKE '[0-9]'
AND CONVERT(VARCHAR(10),c.Id) = RIGHT(u.Username,1)
ORDER BY u.Username