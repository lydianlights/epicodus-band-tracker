using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
    public class Concert
    {
        public int BandId {get; private set;}
        public int VenueId {get; private set;}
        public DateTime Date {get; private set;}

        public Concert(int bandId, int venueId, DateTime date)
        {
            BandId = bandId;
            VenueId = venueId;
            Date = date;
        }
        public override bool Equals(object other)
        {
            if(!(other is Concert))
            {
                return false;
            }
            else
            {
                var otherConcert = (Concert)other;
                return (
                    this.BandId == otherConcert.BandId &&
                    this.VenueId == otherConcert.VenueId &&
                    this.Date == otherConcert.Date
                );
            }
        }
        public override int GetHashCode()
        {
            return this.BandId.GetHashCode() + this.VenueId.GetHashCode() + this.Date.GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO concerts (date, band_id, venue_id) VALUES (@Date, @BandId, @VenueId);";
            cmd.Parameters.Add(new MySqlParameter("@Date", this.Date));
            cmd.Parameters.Add(new MySqlParameter("@BandId", this.BandId));
            cmd.Parameters.Add(new MySqlParameter("@VenueId", this.VenueId));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Concert> GetAll()
        {
            var output = new List<Concert> {};
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM concerts;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                DateTime date = rdr.GetDateTime(0);
                int bandId = rdr.GetInt32(1);
                int venueId = rdr.GetInt32(2);
                output.Add(new Concert(bandId, venueId, date));
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM concerts;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
