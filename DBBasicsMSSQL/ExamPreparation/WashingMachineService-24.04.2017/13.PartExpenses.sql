SELECT job.JobId , ISNULL(SUM(par.Price * ord2.Quantity),0) AS Total
FROM dbo.Jobs job
LEFT JOIN dbo.Orders ord ON job.JobId = ord.JobId
LEFT JOIN dbo.OrderParts ord2 ON ord.OrderId = ord2.OrderId
LEFT JOIN dbo.Parts par ON ord2.PartId = par.PartId
WHERE job.Status = 'Finished'
GROUP BY job.JobId
ORDER BY Total DESC, job.JobId