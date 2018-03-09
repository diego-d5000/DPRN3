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


                string selectDesastresQuery =
                "SELECT Benchmark.id AS ID," +
                "Event.name AS Evento, Sport.name AS Deporte, CONCAT(Competitor.firstName, \" \", Competitor.lastName) AS Competidor, " +
                "Competitor.country AS Pais, CONCAT(Goal.measure, \" \", Benchmark.score) AS Puntaje " +
                "FROM Benchmark " +
                "INNER JOIN Event ON Benchmark.event = Event.id " +
                "INNER JOIN Competitor ON Benchmark.competitor = Competitor.id " +
                "INNER JOIN Sport ON Event.sport = Sport.id " +
                "INNER JOIN Goal ON Event.goal = Goal.id " +
                "ORDER BY Evento;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(selectDesastresQuery, mySqlConnection);

                DataSet desastres = new DataSet();
                adapter.Fill(desastres, "Desastre");

                Console.WriteLine("| ID | Evento        | Deporte              | Competidor                 "
                + "| País     | Puntaje          ");

                foreach (DataRow row in desastres.Tables["Desastre"].Rows)
                {
                    string rowFormatted = "";
                    foreach (var col in row.ItemArray)
                    {
                        rowFormatted += col + " | ";
                    }
                    Console.WriteLine(rowFormatted);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }
    }
}
