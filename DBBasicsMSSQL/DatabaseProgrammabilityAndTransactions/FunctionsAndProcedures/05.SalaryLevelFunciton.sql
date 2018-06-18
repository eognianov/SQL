CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(7);
	SET @salaryLevel = CASE
							WHEN @salary < 30000 THEN 'Low'
							WHEN @salary BETWEEN 30000 AND 50000 THEN 'Average'
							ELSE 'High'
					   END
	RETURN @salaryLevel
END
go

SELECT e.Salary, dbo.ufn_GetSalaryLevel(e.Salary)
 FROM Employees AS e
 go