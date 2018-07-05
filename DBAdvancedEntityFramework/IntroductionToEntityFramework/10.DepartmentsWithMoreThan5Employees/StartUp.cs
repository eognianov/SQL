namespace DepartmentsWithMoreThan5Employees
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                var deparments = dbContext
                    .Departments
                    .Where(d => d.Employees.Count > 5)
                    .Select(d => new
                    {
                        DeptName = d.Name,
                        DeptManagerFirstName = d.Manager.FirstName,
                        DeptManagerLastName = d.Manager.LastName,
                        Employees = d.Employees
                            .Select(e => new
                            {
                                EmpFirstName = e.FirstName,
                                EmpLastName = e.LastName,
                                EmpJobTitle = e.JobTitle
                            })
                    })
                    .OrderBy(d => d.Employees.Count())
                    .ThenBy(d => d.DeptName)
                    .ToList();

                foreach (var dept in deparments)
                {
                    Console.WriteLine($"{dept.DeptName} - {dept.DeptManagerFirstName} {dept.DeptManagerLastName}");
                    foreach (var emp in dept.Employees.OrderBy(e => e.EmpFirstName).ThenBy(e => e.EmpLastName))
                    {
                        Console.WriteLine($"{emp.EmpFirstName} {emp.EmpLastName} - {emp.EmpJobTitle}");
                    }
                    Console.WriteLine(new string('-', 10));
                }
            }
        }
    }
}