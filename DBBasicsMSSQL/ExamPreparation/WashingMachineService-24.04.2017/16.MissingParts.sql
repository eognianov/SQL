   SELECT p.PartId, 
          p.[Description],
		  SUM(pn.Quantity) AS [Required],
		  SUM(p.StockQty) AS [In Stock], 
		  ISNULL(SUM(ord2.Quantity),0) AS [Ordered]
     FROM Parts AS p
     JOIN PartsNeeded AS pn ON pn.PartId = p.PartId
     JOIN Jobs AS j ON j.JobId = pn.JobId
LEFT JOIN dbo.Orders ord ON j.JobId = ord.JobId
LEFT JOIN dbo.OrderParts ord2 ON ord.OrderId = ord2.OrderId
    WHERE j.Status <> 'Finished'
 GROUP BY p.PartId, p.[Description]
   HAVING SUM(pn.Quantity) > SUM(p.StockQty) + ISNULL(SUM(ord2.Quantity),0)
 ORDER BY p.PartId