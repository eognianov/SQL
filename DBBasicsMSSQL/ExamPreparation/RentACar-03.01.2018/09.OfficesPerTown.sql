SELECT tow.Name AS [Town Name], COUNT(*) AS [OfficesNumber]
FROM dbo.Offices [off]
JOIN dbo.Towns tow ON [off].TownId = tow.Id
GROUP BY [off].TownId, tow.Name
ORDER BY OfficesNumber DESC, tow.Name