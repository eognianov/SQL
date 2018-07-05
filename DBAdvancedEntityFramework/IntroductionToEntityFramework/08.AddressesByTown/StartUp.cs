namespace AddressesByTown
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
                var addresses = dbContext
                    .Addresses
                    .Select(a => new
                    {
                        Employees = a.Employees.Count,
                        a.AddressText,
                        TownName = a.Town.Name
                    })
                    .OrderByDescending(a => a.Employees)
                    .ThenBy(a => a.TownName)
                    .ThenBy(a => a.AddressText)
                    .Take(10)
                    .ToList();

                foreach (var ad in addresses)
                {
                    Console.WriteLine($"{ad.AddressText}, {ad.TownName} - {ad.Employees} employees");
                }
            }
        }
    }
}