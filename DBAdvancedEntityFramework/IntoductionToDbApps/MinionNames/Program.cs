using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace MinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int villainId = int.Parse(Console.ReadLine());

                string villainsName = GetVillainName(villainId, connection);

                if (villainsName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villainsName}");
                    PrintNames(villainId, connection);
                }
                
                connection.Close();
            }
        }

        private static void PrintNames(int villainId, SqlConnection connection)
        {

            string minions =
                "SELECT Name, Age FROM Minions AS m JOIN MinionsVillains AS mv ON m.Id = mv.MinionId WHERE mv.VillainId = @Id";

            using (SqlCommand command = new SqlCommand(minions, connection))
            {
                command.Parameters.AddWithValue("@Id", villainId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        int row = 1;
                        while (reader.Read())
                        {
                            
                            Console.WriteLine($"{row++}. {reader[0]} {reader[1]}");
                            
                        }
                    }
                }
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string nameSql = "SELECT Name FROM Villains AS vil WHERE vil.Id = @id";

            using (SqlCommand command = new SqlCommand(nameSql, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);
                return (string)command.ExecuteScalar();
            }
        }
    }
}
