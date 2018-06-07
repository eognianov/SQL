SELECT * INTO EmployeesWithSalariesAbove30000 FROM Employees
WHERE Salary>30000

DELETE FROM EmployeesWithSalariesAbove30000
WHERE ManagerID = 42

UPDATE EmployeesWithSalariesAbove30000
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM EmployeesWithSalariesAbove30000
GROUP BY DepartmentId