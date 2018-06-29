using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace PrintAllMinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.ConnectionString);
            connection.Open();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT Name FROM Minions", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<string> names = new List<string>();
                while (reader.Read())
                {
                    string name = (string) reader["Name"];
                    names.Add(name);
                }

                for (int i = 0; i < names.Count / 2; i++)
                {
                    Console.WriteLine(names[i]);
                    Console.WriteLine(names[names.Count - 1 - i]);
                }

                if (names.Count % 2 == 1)
                {
                    Console.WriteLine(names[names.Count / 2]);
                }
            }
        }
    }
}
