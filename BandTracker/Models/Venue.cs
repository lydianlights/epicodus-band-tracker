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
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO venues (name) VALUES (@Name);";
            cmd.Parameters.Add(new MySqlParameter("@Name", this.Name));
            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Venue> GetAll()
        {
            var output = new List<Venue> {};
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM venues;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                output.Add(new Venue(name, id));
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }
        public static Venue FindById(int targetId)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM venues WHERE id = @id;";
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

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }
        public static void UpdateAtId(int targetId, string newName)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE venues SET name = @name WHERE id = @id;";
            cmd.Parameters.Add(new MySqlParameter("@name", newName));
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        // public void AddConcert(int bandId, DateTime date)
        // {
        //     var newConcert = new Concert(bandId, this.Id, date);
        //     newConcert.Save();
        // }
        // public List<Concert> GetConcerts()
        // {
        //     var output = new List<Concert> {};
        //     MySqlConnection conn = DB.Connection;
        //     conn.Open();
        //
        //     // var cmd = conn.CreateCommand() as MySqlCommand;
        //     // cmd.CommandText = @"SELECT * FROM venues;";
        //     // var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //     // while(rdr.Read())
        //     // {
        //     //     int id = rdr.GetInt32(0);
        //     //     string name = rdr.GetString(1);
        //     //     output.Add(new Venue(name, id));
        //     // }
        //
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        //     return output;
        // }
        public static void DeleteAtId(int targetId)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM concerts WHERE venue_id = @id; DELETE FROM venues WHERE id = @id;";
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM concerts; DELETE FROM venues;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
