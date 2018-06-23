SELECT RIGHT(u.Email, LEN(u.Email) - CHARINDEX('@', u.email)) AS [Email Provider], COUNT(u.Email) AS [Number Of Users]
FROM Users AS u
GROUP BY RIGHT(u.Email, LEN(u.Email) - CHARINDEX('@', u.email))
ORDER BY [Number Of Users] DESC, [Email Provider]