SELECT m.MountainRange,p.PeakName, p.Elevation 
FROM Mountains AS m 
JOIN Peaks AS p ON p.MountainId = m.Id 
WHERE MountainId = 17 
ORDER BY p.Elevation DESC