CREATE FUNCTION udf_GetCost(@jobId INT)
RETURNS DECIMAL(14,2)
BEGIN
	DECLARE @totalCost DECIMAL(14,2) = (
	SELECT SUM(par.Price * ord2.Quantity)
	  FROM dbo.Jobs job
	  JOIN dbo.Orders ord ON job.JobId = ord.JobId
	  JOIN dbo.OrderParts ord2 ON ord.OrderId = ord2.OrderId
	  JOIN dbo.Parts par ON ord2.PartId = par.PartId
	WHERE job.JobId = @jobId)

	IF (@totalCost IS NULL)
	BEGIN
		RETURN 0
	END

	RETURN @totalCost
END 

SELECT dbo.udf_GetCost(1)