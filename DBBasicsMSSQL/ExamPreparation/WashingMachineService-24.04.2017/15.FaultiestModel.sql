SELECT TOP(1) WITH TIES [mod].Name, COUNT(job.JobId) AS [Times Serviced], (SELECT ISNULL(SUM(p.Price * op.Quantity),0)
															FROM Orders AS o
															JOIN OrderParts AS op ON o.OrderId = op.OrderId
															JOIN Parts AS p ON op.PartId = p.PartId
															JOIN Jobs AS j ON o.JobId = j.JobId
															WHERE j.ModelId = [mod].ModelId) AS [Parts Total]
FROM dbo.Models [mod]
JOIN dbo.Jobs job ON [mod].ModelId = job.ModelId
GROUP BY [mod].Name,[mod].ModelId
ORDER BY [Times Serviced] DESC