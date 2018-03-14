using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DPRN3_DIPL
{
    class Program
    {
        static void Main(string[] args)
        {
            string databaseConnectionConfig = "server=localhost;user=dotnet;database=CyberOlympics;port=3306;password=dotnet";

            MySqlConnection mySqlConnection = new MySqlConnection(databaseConnectionConfig);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                mySqlConnection.Open();

                Console.WriteLine("Tabla Inicial...");
                PrintCompetitorsTable(mySqlConnection);

                Competitor nextCompetitor = new CompetitorAR(mySqlConnection);
                nextCompetitor.FirstName = "Adam";
                nextCompetitor.LastName = "Smith";
                nextCompetitor.Country = "UK";
                nextCompetitor.City = "Edinburgh";
                nextCompetitor.Bio = null;

                ((ActiveRecord)nextCompetitor).Save();

                Console.WriteLine("\n\n Despues de insertar un elemento...");
                PrintCompetitorsTable(mySqlConnection);

                Competitor savedCompetitor = new CompetitorAR(mySqlConnection);
                ((ActiveRecord)savedCompetitor).Fetch(nextCompetitor.Id);
                Console.WriteLine("Prueba de uso de select, nombre del competidor con el id "
                 + savedCompetitor.Id + " : " + savedCompetitor.FirstName);

                ((ActiveRecord)nextCompetitor).Delete();


                Console.WriteLine("\n\n Despues de borrar el mismo elemento...");
                PrintCompetitorsTable(mySqlConnection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }

        }

        static void PrintCompetitorsTable(MySqlConnection mySqlConnection)
        {
            string selectDesastresQuery =
               "SELECT * From Competitor";

            MySqlDataAdapter adapter = new MySqlDataAdapter(selectDesastresQuery, mySqlConnection);

            DataSet competitors = new DataSet();
            adapter.Fill(competitors, "Competitor");

            Console.WriteLine("| ID | Nombre          | Apellido            | Pais            "
            + "| Ciudad        | Biografia             ");

            foreach (DataRow row in competitors.Tables["Competitor"].Rows)
            {
                string rowFormatted = "";
                foreach (var col in row.ItemArray)
                {
                    rowFormatted += col + " | ";
                }
                Console.WriteLine(rowFormatted);
            }
        }
    }
}
