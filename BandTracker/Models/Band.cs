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
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO bands (name) VALUES (@Name);";
            cmd.Parameters.Add(new MySqlParameter("@Name", this.Name));
            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Band> GetAll()
        {
            var output = new List<Band> {};
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM bands;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                output.Add(new Band(name, id));
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }
        public static Band FindById(int targetId)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM bands WHERE id = @id;";
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
            cmd.CommandText = @"UPDATE bands SET name = @name WHERE id = @id;";
            cmd.Parameters.Add(new MySqlParameter("@name", newName));
            cmd.Parameters.Add(new MySqlParameter("@id", targetId));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static void DeleteAtId(int targetId)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM concerts WHERE band_id = @id; DELETE FROM bands WHERE id = @id;";
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
            cmd.CommandText = @"DELETE FROM concerts; DELETE FROM bands;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
