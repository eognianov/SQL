SELECT DepartmentId, MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentId
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000
