SELECT TOP(5) emp.EmployeeID, emp.FirstName,emp.Salary, dpt.Name
FROM Employees AS emp
JOIN Departments AS dpt ON dpt.DepartmentID = emp.DepartmentID
WHERE emp.Salary > 15000
ORDER BY emp.DepartmentID