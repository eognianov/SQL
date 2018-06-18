CREATE PROC usp_GetEmployeesSalaryAboveNumber(@number DECIMAL(15,4))
AS
BEGIN
	SELECT e.FirstName, e.LastName
	  FROM Employees AS e
	 WHERE e.Salary >= @number
END

EXEC dbo.usp_GetEmployeesSalaryAboveNumber 35000