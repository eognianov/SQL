CREATE PROC usp_GetTownsStartingWith (@inputString VARCHAR(50))
AS 
BEGIN
	DECLARE @wildCardSTR VARCHAR(51) = @inputString + '%';
	SELECT t.Name
	  FROM Towns AS t
	WHERE t.Name LIKE @wildCardSTR
END


EXEC dbo.usp_GetTownsStartingWith b