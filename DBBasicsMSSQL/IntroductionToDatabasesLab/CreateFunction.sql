CREATE FUNCTION f_CalculateTotalBalance (@ClientId INT)
RETURNS DECIMAL(15,2)
BEGIN
	DECLARE @result DECIMAL(15,2) = (
		SELECT SUM(Balance) 
			FROM Accounts
				WHERE ClientId = @ClientId
	)
	RETURN @result
END


--SELECT [dbo].f_CalculateTotalBalance(2)