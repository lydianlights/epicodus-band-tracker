using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
    public class Venue
    {
        public int Id {get; private set;}
        public string Name {get; private set;}

        public Venue(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }
        public override bool Equals(object other)
        {
            if(!(other is Venue))
            {
                return false;
            }
            else
            {
                var otherVenue = (Venue)other;
                return this.Name == otherVenue.Name;
            }
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        public void Save()
        {
            var cmd = DB.BeginCommand(@"INSERT INTO venues (name) VALUES (@Name);");
            cmd.Parameters.Add(new MySqlParameter("@Name", this.Name));
            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;
            DB.EndCommand();
        }
        public static List<Venue> GetAll()
        {
            var output = new List<Venue> {};
            var cmd = DB.BeginCommand(@"SELECT * FROM venues;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                output.Add(new Venue(name, id));
            }
            DB.EndCommand();
            return output;
        }
        public static Venue FindById(int targetId)
        {
            var cmd = DB.BeginCommand(@"SELECT * FROM venues WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while(rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            var output = new Venue(name, id);
            DB.EndCommand();
            return output;
        }
        public static void UpdateAtId(int targetId, string newName)
        {
            var cmd = DB.BeginCommand(@"UPDATE venues SET name = @name WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@name", newName));
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public void AddConcert(Band band, DateTime date)
        {
            var newConcert = new Concert(band, this, date);
            newConcert.Save();
        }
        public List<Concert> GetConcerts()
        {
            var output = new List<Concert> {};
            var cmd = DB.BeginCommand(@"SELECT date, bands.* FROM concerts JOIN bands ON (concerts.band_id = bands.id) WHERE concerts.venue_id = @VenueId;");
            cmd.Parameters.Add(new MySqlParameter("@VenueId", this.Id));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                DateTime date = rdr.GetDateTime(0);
                int bandId = rdr.GetInt32(1);
                string bandName = rdr.GetString(2);
                Band band = new Band(bandName, bandId);
                output.Add(new Concert(band, this, date));
            }
            DB.EndCommand();
            return output;
        }
        public static void DeleteAtId(int targetId)
        {
            var cmd = DB.BeginCommand(@"DELETE FROM concerts WHERE venue_id = @id; DELETE FROM venues WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public static void ClearAll()
        {
            var cmd = DB.BeginCommand(@"DELETE FROM concerts; DELETE FROM venues;");
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
    }
}
