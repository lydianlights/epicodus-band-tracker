using System;
using MySql.Data.MySqlClient;
using BandTracker;

namespace BandTracker.Models
{
    public static class DB
    {
        private static MySqlConnection _currentConnection = null;
        public static MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection(DBConfiguration.ConnectionString);
            }
        }
        public static MySqlCommand BeginCommand(string query)
        {
            _currentConnection = new MySqlConnection(DBConfiguration.ConnectionString);
            _currentConnection.Open();
            var cmd = _currentConnection.CreateCommand() as MySqlCommand;
            cmd.CommandText = query;
            return cmd;
        }
        public static void EndCommand()
        {
            _currentConnection.Close();
            if (_currentConnection != null)
            {
                _currentConnection.Dispose();
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
