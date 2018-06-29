using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace ChangeTownNameCasing
{
    public class Program
    {
        static void Main(string[] args)
        {
            string contryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();


                int countryId = GetCountryId(contryName, connection);
                if (countryId==0)
                {
                    Console.WriteLine("No towns were affected");
                }
                else
                {
                    int affectedTowns = UpdateNames(countryId, connection);

                    if (affectedTowns>0)
                    {
                        Console.WriteLine($"{affectedTowns} town names were affected");
                        List<string> names = GetNames(countryId, connection);
                        Console.WriteLine($"[{string.Join(", ", names)}]");
                    }
                    else
                    {
                        Console.WriteLine("No towns were affected");
                    }
                }
                connection.Close();
            }
        }

        private static List<string> GetNames(int countryId, SqlConnection connection)
        {
            string namesSql = "SELECT Name FROM Towns WHERE CountryCode = @countryId";

            List<string> names = new List<string>();

            using (SqlCommand command = new SqlCommand(namesSql, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add((string)reader[0]);
                    }

                    return names;
                }
            }
        }

        private static int UpdateNames(int countryId, SqlConnection connection)
        {
            string updateSql = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = @CountryId";

            using (SqlCommand command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.AddWithValue("@CountryId", countryId);

                return command.ExecuteNonQuery();

            }
        }

        private static int GetCountryId(string contryName, SqlConnection connection)
        {
            string coutryInfo = "SELECT Id FROM Countries WHERE Name = @name";

            using (SqlCommand command = new SqlCommand(coutryInfo, connection))
            {
                command.Parameters.AddWithValue("@name", contryName);

                if (command.ExecuteScalar()==null)
                {
                    return 0;
                }

                return (int) command.ExecuteScalar();
            }
        }
    }
}
