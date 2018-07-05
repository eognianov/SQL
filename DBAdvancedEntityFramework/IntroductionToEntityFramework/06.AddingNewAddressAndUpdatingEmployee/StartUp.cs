namespace AddingNewAddressAndUpdatingEmployee
{
    using System;
    using System.Linq;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                Address address = new Address("Vitoshka 15", 4);
                Employee employee = dbContext.Employees.FirstOrDefault(e => e.LastName == "Nakov");

                employee.Address = address;

                dbContext.SaveChanges();

                var employees = dbContext
                    .Employees
                    .Select(e => new
                    {
                        e.Address.AddressText,
                        e.AddressId
                    })
                    .OrderByDescending(e => e.AddressId)
                    .Take(10)
                    .ToList();

                foreach (var emp in employees)
                {
                    Console.WriteLine(emp.AddressText);
                }
            }
        }
    }
}