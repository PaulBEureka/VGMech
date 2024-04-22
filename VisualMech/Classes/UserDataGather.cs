using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public static class UserDataGather
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public static string GetUserAvatarPath(string userID)
        {
            string query = @"SELECT avatar_path FROM avatar WHERE user_id = @UserId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userID);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int pathIndex = reader.GetOrdinal("avatar_path");
                            if (!reader.IsDBNull(pathIndex))
                            {
                                string path = reader.GetString(pathIndex);

                                return path;
                            }
                            else
                            {
                                return "";
                            }
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                
            }
        }
    }
}