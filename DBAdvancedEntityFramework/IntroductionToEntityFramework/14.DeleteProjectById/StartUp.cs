namespace DeleteProjectById
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
            int projectForDelete = 2;

            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                Project project = dbContext.Projects.Find(projectForDelete);

                List<EmployeeProject> employees = dbContext
                    .EmployeesProjects
                    .Where(ep => ep.Project.ProjectId == project.ProjectId)
                    .ToList();

                dbContext.EmployeesProjects.RemoveRange(employees);
                dbContext.Projects.Remove(project);
                dbContext.SaveChanges();

                var projects = dbContext
                    .Projects
                    .Select(p => new
                    {
                        p.Name
                    })
                    .Take(10)
                    .ToList();

                foreach (var pr in projects)
                {
                    Console.WriteLine(pr.Name);
                }
            }
        }
    }
}