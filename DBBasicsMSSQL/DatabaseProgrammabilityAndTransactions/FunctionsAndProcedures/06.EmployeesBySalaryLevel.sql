CREATE PROC usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(7))
AS
BEGIN
	SELECT e.FirstName, e.LastName
	  FROM Employees AS e
	WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @salaryLevel
END

EXEC usp_EmployeesBySalaryLevel @salaryLevel = 'Low'