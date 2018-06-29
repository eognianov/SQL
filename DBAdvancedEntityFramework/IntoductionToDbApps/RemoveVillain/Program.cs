using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            int intputVillainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {

                connection.Open();

                int villainId = GetVillainId(intputVillainId, connection);

                if (villainId == 0)
                {
                    Console.WriteLine("No such villain found.");
                }
                else
                {
                    int releasedMinions = ReleaseMinions(villainId, connection);
                    string villainName = GetVillainName(villainId, connection);
                    DeleteVillain(villainId, connection);

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{releasedMinions} minions were released.");
                }

                connection.Close();
            }
        }

        private static void DeleteVillain(int villainId, SqlConnection connection)
        {
            string deleteVillain = "DELETE FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(deleteVillain, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);
                command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string villainInfo = "SELECT Name FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(villainInfo, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);

                return (string)command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection)
        {
            string realeaseSql = "DELETE FROM MinionsVillains WHERE VillainId = @id";

            using (SqlCommand command = new SqlCommand(realeaseSql, connection))
            {
                command.Parameters.AddWithValue("@id", villainId);

                return command.ExecuteNonQuery();
            }
        }

        private static int GetVillainId(int intputVillainId, SqlConnection connection)
        {
            string villainInfo = "SELECT Id FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(villainInfo, connection))
            {
                command.Parameters.AddWithValue("@id", intputVillainId);

                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                return (int)command.ExecuteScalar();
            }
        }
    }
}
