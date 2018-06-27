SELECT TripId, SUM(Luggage), 
CASE 
	WHEN (SUM(Luggage)<=5) THEN '$0'
	ELSE CONCAT('$', (CAST(SUM(Luggage)*5 AS varchar(2))))
END AS Luggage 
FROM AccountsTrips
GROUP BY TripID
ORDER BY Luggage DESC
