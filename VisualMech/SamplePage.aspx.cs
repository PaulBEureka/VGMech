using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using VisualMech.Classes;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static SqlConnection connection;
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";
        public static List<Comment> comments = new List<Comment>();
        public static string cardTitle = "";
        public string allCommentString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Card> cardList = Session["CardList"] as List<Card>;
            int cardInt = (int)Session["CardId"];

            int temp = 0;
            foreach (Card card in cardList)
            {
                if (temp == cardInt)
                {
                    cardTitle = card.Title;

                    gameMechLit.Text = $@"
                    <section class=""gameMech-bgColor"">
            <div  class=""row text-center "">
                <h1 class=""display-4 mini_custom_padding fw-bolder"">{card.Title}</h1>
            </div>

            <!--Interactive Demonstration and Coding Implementation layout -->
            
            <div class=""row d-grid"">
                <div class=""row justify-content-center text-center m-auto"">
                    <div class=""col-md-6 justify-content-center text-center mx-md-3 gameMech-section-holders my-5"">
                        <div class=""row"">
                            <h2 class=""text-light fw-bolder"">Interactive Demonstration</h2>
                        </div>
                        <div>
                            <iframe src=""{card.UnityLink}"" class=""unityLayout"" scrolling=""no""></iframe>
                        </div>
                    </div>
                    <div class=""col-md-6 d-grid gameMech-section-holders mx-md-3 my-5 "">
                        <div class=""row"">
                            <h2 class=""text-light fw-bolder"">Coding Implementation</h2>
                        </div>
                        <div class=""row justify-content-center m-auto gameMech-code-holder"">
                            <p class= ""text-start"">{card.CodeText}</>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Information layout -->
            <div class=""row d-grid gameMech-layout"">
                <div class=""container gameMech-information-holder m-auto p-3"">
                    <!-- Game genres layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Commonly Used Game Genres:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{card.CommonGenres}</p>
                    
                    <!-- Possible Variation layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Possible Variation of this Game Mechanic:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{card.PossibleVariations}</p>

                    <!-- Possible Combination layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Possible Game Mechanics Combination:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{card.PossibleCombinations}</p>

                </div>
            </div>

            </section> ";

                    get_Comments(card.Title);
                }


                temp++;
            }
        }
    

        
        

        public void get_Comments(string mechanicTitle)
        {
            Response.Write("Nag-update");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Step 2: Write SQL Query
                string query = $@"
                        SELECT CommentTable.*, UserTable.username 
                        FROM CommentTable 
                        INNER JOIN UserTable ON CommentTable.user_id = UserTable.user_id 
                        WHERE CommentTable.mechanic_title = '{mechanicTitle}'
                    ";


                // Step 3: Execute Query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Step 4: Process Data
                        while (reader.Read())
                        {
                            string username = reader["username"].ToString();
                            string dateCommented = reader["comment_date"].ToString();
                            string comment = reader["comment"].ToString();

                            allCommentString += $@"
                                <div class=""comment mt-4 text-justify float-left"">
                                    <img src=""https://i.imgur.com/yTFUilP.jpg"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                    <h4>{username}</h4>
                                    <span>- {dateCommented}</span>
                                    <br>
                                    <p>{comment}</p>
                                </div>
                                <div>
                                    <hr />
                                </div>";
                            
                        }
                    }
                }
            }

            CommentHtml.Text = allCommentString;
        }

        [WebMethod]
        public static string post_Click(string message)
        {
            string result = "";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO CommentTable (user_id, mechanic_title ,comment, comment_date) VALUES (@UserId, @MechanicTitle, @CommentText, @CommentDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", 1);
                        command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                        command.Parameters.AddWithValue("@CommentText", message);
                        command.Parameters.AddWithValue("@CommentDate", DateTime.Now);

                        command.ExecuteNonQuery();
                    }


                    result = "Comment posted successfully";

                    connection.Close();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }



    }
}