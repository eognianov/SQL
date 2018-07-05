namespace RemoveTowns
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
            string townForDelete = Console.ReadLine();

            SoftUniContext dbContext = new SoftUniContext();

            using (dbContext)
            {
                Town town = dbContext.Towns.FirstOrDefault(t => t.Name == townForDelete);
                List<Address> addresses = dbContext
                    .Addresses
                    .Where(a => a.TownId == town.TownId)
                    .ToList();

                foreach (Employee emp in dbContext.Employees)
                {
                    if (addresses.Contains(emp.Address))
                    {
                        emp.Address = null;
                    }
                }

                dbContext.Addresses.RemoveRange(addresses);
                dbContext.Towns.Remove(town);
                dbContext.SaveChanges();

                if (addresses.Count == 1)
                {
                    Console.WriteLine($"1 address in {townForDelete} was deleted");
                }
                else
                {
                    Console.WriteLine($"{addresses.Count} addresses in {townForDelete} was deleted");
                }
            }
        }
    }
}