using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
    public class Concert
    {
        public Band Band {get; private set;}
        public Venue Venue {get; private set;}
        public DateTime Date {get; private set;}

        public Concert(Band band, Venue venue, DateTime date)
        {
            Band = band;
            Venue = venue;
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
                    this.Band.Equals(otherConcert.Band) &&
                    this.Venue.Equals(otherConcert.Venue) &&
                    this.Date.Equals(otherConcert.Date)
                );
            }
        }
        public override int GetHashCode()
        {
            return this.Band.GetHashCode() + this.Venue.GetHashCode() + this.Date.GetHashCode();
        }
        public void Save()
        {
            AddByIds(this.Band.Id, this.Venue.Id, this.Date);
        }
        public static void AddByIds(int bandId, int venueId, DateTime date)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO concerts (date, band_id, venue_id) VALUES (@Date, @BandId, @VenueId);";
            cmd.Parameters.Add(new MySqlParameter("@Date", date.ToString("yyyy-MM-dd")));
            cmd.Parameters.Add(new MySqlParameter("@BandId", bandId));
            cmd.Parameters.Add(new MySqlParameter("@VenueId", venueId));
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
            cmd.CommandText = @"SELECT date, bands.*, venues.* FROM concerts JOIN bands ON (concerts.band_id = bands.id) JOIN venues ON (concerts.venue_id = venues.id);";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                DateTime date = rdr.GetDateTime(0);
                int bandId = rdr.GetInt32(1);
                string bandName = rdr.GetString(2);
                int venueId = rdr.GetInt32(3);
                string venueName = rdr.GetString(4);

                Band band = new Band(bandName, bandId);
                Venue venue = new Venue(venueName, venueId);
                output.Add(new Concert(band, venue, date));
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
