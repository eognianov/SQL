namespace IncreaseSalaries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            string[] departments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            decimal salaryIncrease = 0.12m;

            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                List<Employee> employees = dbContext
                    .Employees
                    .Where(e => departments.Contains(e.Department.Name))
                    .ToList();

                foreach (Employee employee in employees)
                {
                    employee.Salary += employee.Salary * salaryIncrease;
                }

                dbContext.SaveChanges();

                var employeesWithIncreaseSalary = dbContext
                    .Employees
                    .Where(e => departments.Contains(e.Department.Name))
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Salary
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var emp in employeesWithIncreaseSalary)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F2})");
                }
            }
        }
    }
}