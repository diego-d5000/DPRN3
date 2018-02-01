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
            string databaseConnectionConfig = "server=localhost;user=dotnet;database=DesastresNaturales;port=3306;password=dotnet";

            MySqlConnection mySqlConnection = new MySqlConnection(databaseConnectionConfig);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                mySqlConnection.Open();


                string selectDesastresQuery =
                "SELECT Desastre.id, "
                + "TipoEvento.titulo AS tipoEvento, Causa.descripcion AS causa, Efectos.descripcion AS efectos, "
                + "Ubicacion.latitud AS latitud, Ubicacion.longitud AS logitud, "
                + "Desastre.fechaInicio, Desastre.fechaFinal, Desastre.nota "
                + "FROM Desastre "
                + "INNER JOIN TipoEvento ON Desastre.tipoEvento = TipoEvento.id "
                + "INNER JOIN Causa ON Desastre.causa = Causa.id "
                + "INNER JOIN Efectos ON Desastre.efectos = Efectos.id "
                + "INNER JOIN Ubicacion ON Desastre.ubicacion = Ubicacion.id;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(selectDesastresQuery, mySqlConnection);

                DataSet desastres = new DataSet();
                adapter.Fill(desastres, "Desastre");

                Console.WriteLine("| id | tipoEvento        | causa              | efectos                 " 
                + "| latitud     | logitud       | fechaInicio         | fechaFinal          | nota                                   |");

                foreach (DataRow row in desastres.Tables["Desastre"].Rows)
                {
                    string rowFormatted = "";
                    foreach(var col in row.ItemArray) 
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
