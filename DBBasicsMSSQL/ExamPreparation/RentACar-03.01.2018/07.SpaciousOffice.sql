SELECT t.Name AS TownName, o.Name AS OfficeName, o.ParkingPlaces FROM Offices AS o JOIN Towns as t ON o.TownId = t.Id
WHERE o.ParkingPlaces>25
ORDER BY t.Name,o.Id