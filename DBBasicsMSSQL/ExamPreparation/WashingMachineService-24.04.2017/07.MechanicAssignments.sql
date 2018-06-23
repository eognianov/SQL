SELECT CONCAT(m.FirstName, ' ', m.LastName) AS MEchanic, [Status], j.IssueDate
FROM Mechanics AS m
JOIN Jobs AS j ON j.MechanicId = m.MechanicId
ORDER BY m.MechanicId, j.IssueDate, j.JobId