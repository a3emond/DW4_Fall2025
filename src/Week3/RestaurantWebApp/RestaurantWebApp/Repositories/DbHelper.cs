using System;
using System.Data.SqlClient;

namespace SimpleWebApp.Repositories
{
    public static class DbHelper
    {
        private static readonly string _connStr =
            System.Configuration.ConfigurationManager.ConnectionStrings["main_db"].ConnectionString;

        // Non-generic version for actions that do not return a value
        public static void WithConnection(Action<SqlConnection> work)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                work(conn);
            }
        }

        // Generic version to return a value
        public static T WithConnection<T>(Func<SqlConnection, T> work)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                conn.Open();
                return work(conn);
            }
        }
    }
}