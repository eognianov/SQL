CREATE PROC usp_GetHoldersWithBalanceHigherThan (@amount DECIMAL(15,2))
AS
BEGIN
	SELECT ah.FirstName, ah.LastName 
	FROM AccountHolders AS ah
	INNER JOIN 
	(SELECT AccountHolderId, SUM(acc.Balance) AS Total
	 FROM Accounts AS acc
	 GROUP BY acc.AccountHolderId) AS a
	 ON a.AccountHolderId = ah.Id
	 WHERE a.Total > @amount
	 ORDER BY ah.FirstName, ah.LastName
END

EXEC usp_GetHoldersWithBalanceHigherThan 10000