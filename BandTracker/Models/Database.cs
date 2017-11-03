using System;
using MySql.Data.MySqlClient;
using BandTracker;

namespace BandTracker.Models
{
    public class DB
    {
        public static MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection(DBConfiguration.ConnectionString);
            }
        }
        public static void ClearAllTables()
        {
            Concert.ClearAll();
            Venue.ClearAll();
            Band.ClearAll();
        }
    }
}
