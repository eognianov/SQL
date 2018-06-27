SELECT t.Id, 
h.[Name] AS HotelName, 
r.[Type] AS RoomType, 
CASE 
	WHEN (t.CancelDate IS NULL) THEN (r.Price+h.BaseRate)
	ELSE 0
	END AS Revenue
FROM Trips AS t
JOIN Rooms As r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
ORDER BY r.[Type],t.Id