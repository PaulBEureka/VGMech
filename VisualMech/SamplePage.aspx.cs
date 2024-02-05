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
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static SqlConnection connection;
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e)
        {
            List<Card> cardList = Session["CardList"] as List<Card>;
            int cardInt = (int)Session["CardId"];

            int temp = 0;
            foreach (Card card in cardList)
            {
                if (temp == cardInt)
                {

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
                        <div class=""row justify-content-center text-center m-auto gameMech-code-holder"">
                            {card.CodeText}
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
            
            <!-- Comment Section layout -->

            <div class=""row m-auto gameMech-layout-padding"">
                <div class=""container gameMech-comment-layout d-grid"">
                    <div class=""gameMech-outer-box m-auto d-grid"">
                        <div class=""container m-0 mx-auto"">
                            <div class=""row "">
                                <h4 class=""fw-bolder"">Share your thoughts</h4>
                            </div>
                            <div>
                                <hr />
                            </div>
                            <!--Per Comment Layout -->
                            <!--
                                Need pa ng signup para maiconnect account sa database
                                1. Create database (DONE)
                                2. Gawing Dynamic tong Sample Page 
                                3. Iconnect sa database para icollect comment records sa table then dynamically add html comment layouts
                                4. Edit the post comment button method para mag add ng comment sa database then ireflect sa comment layout
                                -->
                            <section>
                                <div class=""container""> <!--GAGAWIN TONG LITERAL KAPAG LALAGYAN NA COMMENTS GALING DATABASE -->
                                    <div class=""row"">
                                        <div class=""col-sm-5 col-md-6 col-12 pb-4 comment-section-size"">
                                            <div class=""comment mt-4 text-justify float-left"">
                                                <img src=""https://i.imgur.com/yTFUilP.jpg"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>Jhon Doe</h4>
                                                <span>- 20 October, 2018</span>
                                                <br>
                                                <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Accusamus numquam assumenda hic aliquam vero sequi velit molestias doloremque molestiae dicta?</p>
                                            </div>
                                            <div class=""text-justify darker mt-4 float-right"">
                                                <img src=""https://i.imgur.com/CFpa3nK.jpg"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>Rob Simpson</h4>
                                                <span>- 20 October, 2018</span>
                                                <br>
                                                <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Accusamus numquam assumenda hic aliquam vero sequi velit molestias doloremque molestiae dicta?</p>
                                            </div>
                                            <div class=""comment mt-4 text-justify"">
                                                <img src=""https://i.imgur.com/yTFUilP.jpg"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>Jhon Doe</h4>
                                                <span>- 20 October, 2018</span>
                                                <br>
                                                <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Accusamus numquam assumenda hic aliquam vero sequi velit molestias doloremque molestiae dicta?</p>
                                            </div>
                                            <div class=""darker mt-4 text-justify"">
                                                <img src=""https://i.imgur.com/CFpa3nK.jpg"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>Rob Simpson</h4>
                                                <span>- 20 October, 2018</span>
                                                <br>
                                                <p >Lorem ipsum dolor sit, amet consectetur adipisicing elit. Accusamus numquam assumenda hic aliquam vero sequi velit molestias doloremque molestiae dicta?</p>
                                            </div>
                                        </div>


                                        <div class=""col-lg-4 col-md-5 col-sm-4 offset-md-1 offset-sm-1 col-12 mt-4"">
                                                <div class=""form-group"">
                                                    <h4>Leave a comment</h4>
                                                    <label for=""message"">Message</label>
                                                    <textarea name=""msg"" id="""" cols=""30"" rows=""5"" class=""form-control"" style=""background-color: white; ""></textarea>
                                                </div>
                                                <div>
                                                    <button type=""button"" id=""post"" class=""btn btn-danger my-3"" onclick=""post_Click()"">Post Comment</button>

                                                </div>
                                        </div>
                                    </div>
                                </div>
                            </section>




                        </div>
                    </div>
                </div>
            </div>


        </section>
                    ";
                }


                temp++;
            }
        }
    

        protected void post_Click(object sender, EventArgs e)
        {
            
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Response.Write("<script>alert('Comment posted successfully')</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error')</script>");
                }
            }

        }

        [WebMethod]
        public static string post_Click()
        {
            string result = "";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    result = "Comment posted successfully";
                }
                catch (Exception ex)
                {
                    result = "Error";
                }
            }
            return result;
        }



    }
}