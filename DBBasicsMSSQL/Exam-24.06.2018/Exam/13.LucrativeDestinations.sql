SELECT TOP(10) c.Id, c.Name AS Name, SUM((r.Price + h.BaseRate)) AS [Total Revenue], COUNT(t.Id) AS Trips FROM Trips AS t
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
JOIN Cities AS c ON c.Id = h.CityId
WHERE YEAR(t.BookDate) = 2016
GROUP BY c.Name, c.Id
ORDER BY [Total Revenue] DESC, Trips DESC