UPDATE [Employees]
	SET [Salary] *= 1.12
WHERE [DepartmentId] IN 
	(SELECT [DepartmentId]
	FROM [Departments]
WHERE [Name] IN('Engineering', 'Tool Design', 'Marketing', 'Information Services'))

SELECT Salary FROM Employees