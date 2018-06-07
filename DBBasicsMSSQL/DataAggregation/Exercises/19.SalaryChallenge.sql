SELECT TOP 10 e1.FirstName, 
       e1.LastName, 
       e1.DepartmentID 
  FROM Employees AS e1, 
        (SELECT DepartmentID, 
	            AVG(Salary) AS AverageSalary 
	       FROM Employees 
	   GROUP BY DepartmentID) AS e2
 WHERE e1.DepartmentID = e2.DepartmentID AND e1.Salary > e2.AverageSalary
ORDER BY e1.DepartmentID