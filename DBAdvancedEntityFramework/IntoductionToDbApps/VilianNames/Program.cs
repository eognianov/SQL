using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace VilianNames
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainsInfo = "SELECT v.Name, COUNT(mv.MinionId) AS MiniounsCount FROM Villains v JOIN MinionsVillains [mv] ON mv.VillainId = v.Id GROUP BY v.Name HAVING COUNT(mv.MinionId) >= 3 ORDER BY MiniounsCount DESC";

                using (SqlCommand command = new SqlCommand(villainsInfo, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Name:{reader[0]} -> Minions: {reader[1]}");
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
