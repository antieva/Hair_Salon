using System;
using MySql.Data.MySqlClient;
using HairSalonApp;

namespace HairSalonApp
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
