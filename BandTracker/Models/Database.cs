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
    }
}
