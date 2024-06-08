using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace VisualMech.Classes
{
    public class Badge
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public string BadgeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }


        public bool RecordBadgeToUser(string userID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM owned_badges WHERE user_id = @UserId AND badge_name = @BadgeName";
                    int count;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", userID);
                        selectCommand.Parameters.AddWithValue("@BadgeName", Title);
                        count = Convert.ToInt32(selectCommand.ExecuteScalar());
                    }

                    if (count == 0) // No similar record found
                    {
                        DateTime timestampUtc = DateTime.UtcNow;

                        string insertQuery = "INSERT INTO owned_badges (badge_name, obtained_date, user_id) VALUES (@BadgeName, @ObtainedDate, @UserId)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@BadgeName", Title);
                            insertCommand.Parameters.AddWithValue("@ObtainedDate", timestampUtc);
                            insertCommand.Parameters.AddWithValue("@UserId", userID);
                            insertCommand.ExecuteNonQuery();
                        }

                        return true;
                    }
                    connection.Close();

                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void ShowBadgeToast(ClientScriptManager clientScriptManager, System.Type type)
        {
            string script = $@"
                            toastr.options = {{
                              ""closeButton"": false,
                              ""debug"": false,
                              ""newestOnTop"": false,
                              ""progressBar"": true,
                              ""positionClass"": ""toast-top-right"",
                              ""preventDuplicates"": false,
                              ""onclick"": null,
                              ""showDuration"": ""300"",
                              ""hideDuration"": ""1000"",
                              ""timeOut"": ""10000"",
                              ""extendedTimeOut"": ""1000"",
                              ""showEasing"": ""swing"",
                              ""hideEasing"": ""linear"",
                              ""showMethod"": ""fadeIn"",
                              ""hideMethod"": ""fadeOut""
                            }}
                            
                            toastr['success']('<div class=""text-center d-grid""><img class=""badge-toast shadow text-center mx-auto mb-3"" src=""{Path}""></div><div class=""text-center""><p class=""text-white text-center""><strong>You have obtained the badge:</strong><br/>{Title}</p></div>');
                        ";
            clientScriptManager.RegisterClientScriptBlock(type, $@"BadgeScript{BadgeID}", script, true);
        }

        public string GetToastString()
        {
            return $@"
                    toastr.options = {{
                        ""closeButton"": false,
                        ""debug"": false,
                        ""newestOnTop"": false,
                        ""progressBar"": true,
                        ""positionClass"": ""toast-top-left"",
                        ""preventDuplicates"": false,
                        ""onclick"": null,
                        ""showDuration"": ""300"",
                        ""hideDuration"": ""1000"",
                        ""timeOut"": ""10000"",
                        ""extendedTimeOut"": ""1000"",
                        ""showEasing"": ""swing"",
                        ""hideEasing"": ""linear"",
                        ""showMethod"": ""fadeIn"",
                        ""hideMethod"": ""fadeOut""
                    }}
                            
                    toastr['success']('<div class=""text-center d-grid""><img class=""badge-toast shadow text-center mx-auto mb-3"" src=""{Path}""></div><div class=""text-center""><p class=""text-white text-center""><strong>You have obtained the badge:</strong><br/>{Title}</p></div>');
                ";
        }

        public string CreateBadgeHTMLActive(string dateObtained)
        {
            return $@"<div class=""col"">
                        <div class=""card-badge"">
                                <div class=""container-image"">
                                    <img class=""image-circle"" src=""{Path}"">
                                </div>
                                <div class=""content"">
                                    <div class=""detail"">
                                        <p class=""text-center""><strong>Date obtained:</strong><br/>{dateObtained}</p>
                                        <p><strong>Description:</strong><br />{Description}</p>
                                    </div>
                                </div>
                        </div>
                        <div clas=""container"">
                            <p class=""fw-bold text-center"">{Title}</p>
                        </div>
                    </div>";
        }

        public string CreateBadgeHTMLDisabled()
        {
            return $@"<div class=""col"">
                        <div class=""card-badge-inactive"">
                                <div class=""container-image"">
                                    <img class=""image-circle"" src=""{Path}"">
                                </div>
                                <div class=""content"">
                                    <div class=""detail"">
                                        <p class=""text-center fw-bold"">Badge not yet obtained</p>
                                        <p><strong>Description:</strong><br />{Description}</p>
                                    </div>
                                </div>
                        </div>
                        <div clas=""container"">
                            <p class=""fw-bold text-center"">{Title}</p>
                        </div>
                    </div>";
        }
    }
}