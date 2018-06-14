SELECT emp.EmployeeID, emp.FirstName, emp.LastName, dpt.Name
FROM Employees AS emp
JOIN Departments AS dpt ON dpt.DepartmentID = emp.DepartmentID
WHERE dpt.Name = 'Sales'
ORDER BY emp.EmployeeID