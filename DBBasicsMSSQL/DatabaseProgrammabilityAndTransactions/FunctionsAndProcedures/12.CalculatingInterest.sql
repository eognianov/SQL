CREATE PROC usp_CalculateFutureValueForAccount (@accountId INT, @interestRate FLOAT)
AS

BEGIN
	SELECT acc.Id, ah.FirstName, ah.LastName, acc.Balance, dbo.ufn_CalculateFutureValue(acc.Balance, @interestRate, 5) AS [Balance in 5 years]
	  FROM Accounts AS acc
	INNER JOIN AccountHolders AS ah ON ah.Id =acc.AccountHolderId
	WHERE acc.Id = @accountId

END

EXEC usp_CalculateFutureValueForAccount @accountId = 1, @interestRate = 0.1