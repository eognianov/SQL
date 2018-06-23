 SELECT ISNULL(SUM(p.Price * op.Quantity),0) AS [Parts Total]
   FROM Parts AS p
  RIGHT JOIN OrderParts AS op ON op.PartId = p.PartId
  RIGHT JOIN Orders AS o ON o.OrderId = op.OrderId
  WHERE DATEDIFF(WEEK, o.IssueDate, '2017-04-24') <=3