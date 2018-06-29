using System;
using System.Data.SqlClient;
using _01.InitialSetup;

namespace InitialSetup
{
    public class Program
    {
        public static void Main()
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();


                //CreateDB
                ExecNonQuery(connection, SqlQueries.databaseSql);

                //UseDB
                connection.ChangeDatabase("MinionsDB");

                //CreateTables
                ExecNonQuery(connection, SqlQueries.tableCountries);
                ExecNonQuery(connection, SqlQueries.tableTowns);
                ExecNonQuery(connection, SqlQueries.tableMinions);
                ExecNonQuery(connection, SqlQueries.tableEvillnesFactor);
                ExecNonQuery(connection, SqlQueries.tableVilians);
                ExecNonQuery(connection, SqlQueries.tableMinionsVilians);


                //InsertIntoDB
                ExecNonQuery(connection, SqlQueries.insertCountries);
                ExecNonQuery(connection, SqlQueries.insertTowns);
                ExecNonQuery(connection, SqlQueries.insertMinions);
                ExecNonQuery(connection, SqlQueries.insertEvilnessFactors);
                ExecNonQuery(connection, SqlQueries.insertVillains);
                ExecNonQuery(connection, SqlQueries.insertMinionsVillains);


                connection.Close();
            }
        }

        private static void ExecNonQuery(SqlConnection connection, string databaseSql)
        {
            using (SqlCommand command = new SqlCommand(databaseSql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
