using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
    public class Band
    {
        public int Id {get; private set;}
        public string Name {get; private set;}

        public Band(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }
        public override bool Equals(object other)
        {
            if(!(other is Band))
            {
                return false;
            }
            else
            {
                var otherBand = (Band)other;
                return this.Name == otherBand.Name;
            }
        }
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        public void Save()
        {
            var cmd = DB.BeginCommand(@"INSERT INTO bands (name) VALUES (@Name);");
            cmd.Parameters.Add(new MySqlParameter("@Name", this.Name));
            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;
            DB.EndCommand();
        }
        public static List<Band> GetAll()
        {
            var output = new List<Band> {};
            var cmd = DB.BeginCommand(@"SELECT * FROM bands;");
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                output.Add(new Band(name, id));
            }
            DB.EndCommand();
            return output;
        }
        public static Band FindById(int targetId)
        {
            var cmd = DB.BeginCommand(@"SELECT * FROM bands WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string name = "";
            while(rdr.Read())
            {
                id = rdr.GetInt32(0);
                name = rdr.GetString(1);
            }
            var output = new Band(name, id);
            DB.EndCommand();
            return output;
        }
        public static void UpdateAtId(int targetId, string newName)
        {
            var cmd = DB.BeginCommand(@"UPDATE bands SET name = @name WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@name", newName));
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public void AddConcert(Venue venue, DateTime date)
        {
            var newConcert = new Concert(this, venue, date);
            newConcert.Save();
        }
        public List<Concert> GetConcerts()
        {
            var output = new List<Concert> {};
            var cmd = DB.BeginCommand(@"SELECT date, venues.* FROM concerts JOIN venues ON (concerts.venue_id = venues.id) WHERE concerts.band_id = @BandId;");
            cmd.Parameters.Add(new MySqlParameter("@BandId", this.Id));
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                DateTime date = rdr.GetDateTime(0);
                int venueId = rdr.GetInt32(1);
                string venueName = rdr.GetString(2);
                Venue venue = new Venue(venueName, venueId);
                output.Add(new Concert(this, venue, date));
            }
            DB.EndCommand();
            return output;
        }
        public static void DeleteAtId(int targetId)
        {
            var cmd = DB.BeginCommand(@"DELETE FROM concerts WHERE band_id = @id; DELETE FROM bands WHERE id = @id;");
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
        public static void ClearAll()
        {
            var cmd = DB.BeginCommand(@"DELETE FROM concerts; DELETE FROM bands;");
            cmd.ExecuteNonQuery();
            DB.EndCommand();
        }
    }
}
