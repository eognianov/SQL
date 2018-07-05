namespace Employee147
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;

    public class StartUp
    {
        public static void Main()
        {
            const int EmployeeId = 147;

            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                var employee = dbContext
                    .Employees
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        Projects = e.EmployeesProjects
                            .Select(ep => new
                            {
                                ProjectName = ep.Project.Name
                            })
                    })
                    .FirstOrDefault(e => e.EmployeeId == EmployeeId);
                    
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                foreach (var project in employee.Projects.OrderBy(p => p.ProjectName))
                {
                    Console.WriteLine(project.ProjectName);
                }
            }
        }
    }
}