CREATE PROC usp_GetHoldersFullName
AS
BEGIN
	SELECT ah.FirstName + ' ' + ah.LastName AS [Full Name] 
	FROM AccountHolders AS ah
END
