using System.Data;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace db
{
    public static class Connection
    {
        private static SqlConnection Connect;

        static Connection()
        {

            string connectionString = @"Data Source=DESKTOP-85V1DFR;Initial Catalog=qlks;Integrated Security=true"; 
            Connect = new SqlConnection(connectionString);
        }
        public static SqlConnection GetConnection()
        {
            return Connect;
        }
        public static string ExecuteQueryvalue(string query)
        {
            Connect.Open();
            using (SqlCommand command = new SqlCommand(query, Connect))
            {
                object result = command.ExecuteScalar();
                Connect.Close();
                return result?.ToString(); // Convert result to string and return
            }
        }
        public static void ExecuteQuery(string query)
        {
            Connect.Open();
            using (SqlCommand command = new SqlCommand(query, Connect))
            {
                chektime();
                command.ExecuteNonQuery();
            }
            Connect.Close();
        }

        public static void chektime()
        {
            DateTime timenow = DateTime.Now;
            string checksql = $"SELECT ClientID FROM client WHERE checkout >" + timenow;

            List<int> clientIds = new List<int>();

            using (SqlCommand command = new SqlCommand(checksql, Connect))
            {
                command.Parameters.AddWithValue("@timenow", timenow);

                Connect.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int clientId = reader.GetInt32(0); // Assuming ClientID is an int
                        clientIds.Add(clientId);
                    }
                }
            }

            foreach (int clientId in clientIds)
            {
                string remove = "UPDATE Room SET ClientID = '' WHERE ClientID = @clientId";
                using (SqlCommand command = new SqlCommand(remove, Connect))
                {
                    command.Parameters.AddWithValue("@clientId", clientId);
                    command.ExecuteNonQuery();
                    Connect.Close();
                }
            }
        }

    }
}
