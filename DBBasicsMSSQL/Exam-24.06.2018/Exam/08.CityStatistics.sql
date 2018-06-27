SELECT c.[Name] AS City, COUNT(h.Id) AS Hotels FROM Cities AS c
LEFT JOIN Hotels AS h ON h.CityId = c.Id
GROUP BY c.Name
ORDER BY Hotels DESC, City