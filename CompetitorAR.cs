using System.Data;
using MySql.Data.MySqlClient;

namespace DPRN3_DIPL
{
    public class CompetitorAR : Competitor, ActiveRecord
    {
        private static int COL_ID = 0;
        private static int COL_FIRSTNAME = 1;
        private static int COL_LASTNAME = 2;
        private static int COL_COUNTRY = 3;
        private static int COL_CITY = 4;
        private static int COL_BIO = 5;

        private MySqlConnection conn;
        public CompetitorAR(MySqlConnection conn)
        {
            this.conn = conn;
        }

        public void Delete()
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd;

            cmd = new MySqlCommand("DELETE FROM Competitor WHERE id=@id", this.conn);

            cmd.Parameters.Add("@id", MySqlDbType.VarChar, 15).Value = this.Id;

            da.DeleteCommand = cmd;

            cmd.ExecuteNonQuery();

            this.Id = 0;
            this.FirstName = null;
            this.LastName = null;
            this.Country = null;
            this.City = null;
            this.Bio = null;
        }

        public void Fetch(int id)
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd;

            // Create the SelectCommand.
            cmd = new MySqlCommand("SELECT * FROM Competitor WHERE id=@id", this.conn);

            cmd.Parameters.Add("@id", MySqlDbType.VarChar, 15).Value = id;

            da.SelectCommand = cmd;

            DataSet competitor = new DataSet();
            da.Fill(competitor, "Competitor");

            DataRow row = competitor.Tables["Competitor"].Rows[0];
            int i = 0;
            foreach (var col in row.ItemArray)
            {
                if (col == System.DBNull.Value) continue;

                if (i == COL_ID)
                {
                    this.Id = (int)col;
                }
                else if (i == COL_FIRSTNAME)
                {
                    this.FirstName = (string)col;
                }
                else if (i == COL_LASTNAME)
                {
                    this.LastName = (string)col;
                }
                else if (i == COL_COUNTRY)
                {
                    this.Country = (string)col;
                }
                else if (i == COL_CITY)
                {
                    this.City = (string)col;
                }
                else if (i == COL_BIO)
                {
                    this.Bio = (string)col;
                }

                i++;
            }
        }

        public void Save()
        {
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd;

            if (this.Id != 0)
            {
                cmd = new MySqlCommand("UPDATE Competitor " +
                "SET firstName=@firstName, lastName=@lastName, country=@country, city=@city, bio=@bio " +
                "WHERE id=@id;", this.conn);
                cmd.Parameters.Add("@firstName", MySqlDbType.VarChar, 50).Value = this.FirstName;
                cmd.Parameters.Add("@lastName", MySqlDbType.VarChar, 50).Value = this.LastName;
                cmd.Parameters.Add("@country", MySqlDbType.VarChar, 16).Value = this.Country;
                cmd.Parameters.Add("@city", MySqlDbType.VarChar, 32).Value = this.City;
                cmd.Parameters.Add("@bio", MySqlDbType.VarChar, 250).Value = this.Bio;
                cmd.Parameters.Add("@id", MySqlDbType.VarChar, 15).Value = this.Id;

            }
            else
            {

                // Create the InsertCommand.
                cmd = new MySqlCommand("INSERT INTO Competitor (firstName, lastName, country, city, bio) " +
                "VALUES (@firstName, @lastName, @country, @city, @bio);", this.conn);
                cmd.Parameters.Add("@firstName", MySqlDbType.VarChar, 50).Value = this.FirstName;
                cmd.Parameters.Add("@lastName", MySqlDbType.VarChar, 50).Value = this.LastName;
                cmd.Parameters.Add("@country", MySqlDbType.VarChar, 16).Value = this.Country;
                cmd.Parameters.Add("@city", MySqlDbType.VarChar, 32).Value = this.City;
                cmd.Parameters.Add("@bio", MySqlDbType.VarChar, 250).Value = this.Bio;
            }


            da.InsertCommand = cmd;

            cmd.ExecuteNonQuery();

            if (this.Id == 0)
            {
                int idInserted = (int)cmd.LastInsertedId;
                this.Id = idInserted;
            }
        }
    }
}