SELECT t.Id,
CASE 
	WHEN (a.MiddleName IS NULL) THEN CONCAT(a.FirstName, ' ', a.LastName)
	ELSE CONCAT(a.FirstName,' ', a.MiddleName,' ', a.LastName) 
END AS [Full Name],
ht.[Name] AS [From],
c.[Name] AS [To],
CASE
	WHEN (t.CancelDate IS NULL) THEN CONCAT(CAST(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate) AS VARCHAR(5)), ' days')
	ELSE 'Canceled' 
END AS Duration
FROM AccountsTrips AS [at]
JOIN Accounts AS a ON a.Id = [at].AccountId
JOIN Trips AS t ON [at].TripId = t.Id
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
JOIN Cities AS c ON c.Id = h.CityId
JOIN Cities AS ht ON ht.Id = a.CityId
ORDER BY [Full Name], t.Id
