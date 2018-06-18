--CREATE PROC usp_GetEmployeesFromTown (@townToSearch VARCHAR(50))
--AS
--BEGIN
	
--	SELECT e.FirstName, e.LastName
--	  FROM Employees AS e
--	WHERE e.AddressID = (
--						SELECT a.AddressID
--						  FROM Addresses AS a
--						 WHERE a.TownID = (SELECT t.TownID 
--											FROM Towns AS t
--											WHERE t.Name = @townToSearch))  
--END

CREATE PROC usp_GetEmployeesFromTown (@townToSearch VARCHAR(50))
AS
BEGIN

	SELECT e.FirstName, e.LastName
	  FROM Employees AS e
	INNER JOIN Addresses AS a ON a.AddressID = e.AddressID
	INNER JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.Name = @townToSearch
	
END


EXEC usp_GetEmployeesFromTown Sofia