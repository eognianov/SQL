CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
     SELECT e.FirstName,
            e.LastName
     FROM   Employees AS e
     WHERE  e.Salary > 35000;