using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public class MiniGameCard: Card
    {
        public string CardID { get; set; }
        public string Title { get; set; }
        public string ThumbSource { get; set; }
        public string UnityLink { get; set; }



        public override string GetCardHtml()
        {
            return $@"
                    <div class=""col-md-6 justify-content-center text-center mx-md-5 my-5 no_bg"">
                        <p class=""fw-bolder h3 text-light"">{Title.ToUpper()}</p>
                        <a class=""btn w-100 h-100 rcorners2 minigame_card"" href=""MiniGamePage.aspx?MiniGame={CardID}"" role=""button"">
                            <img src=""{ThumbSource}"" alt=""buttonpng"" class=""img-fluid w-100 h-100 rounded_corners"" />
                        </a>

                    </div>
                    ";
        }

        public override string GetContentHtml(string sessionUserID = null)
        {
            string temp = $@"<div class=""row align-self-center mini_game_box m-auto my-5"">
                        <div class=""row minigame_title text-center d-grid m-auto"">
                            <p class=""text-white display-5 m-auto"">{Title.ToUpper()}</p>
                        </div>";

            if (sessionUserID != null)
            {

                temp += $@"<div class=""row center_custom m-auto my-5 mt-0"" >
                                <iframe src=""{UnityLink}?current_id={sessionUserID}"" class=""mini_game_inner_box"" scrolling=""no""></iframe>
                            </div>";
            }
            else
            {
                temp += $@"<div class=""row center_custom m-auto my-5 mt-0 "">
                                <button type=""button"" id=""post_disbled"" class=""comment_button my-2 bg-danger w-100"" onclick=""sign_in_comment()"">Sign in to play mini game :></button>
                            </div>";
            }

            temp += "</div>";

            return temp;
        }

        public override string GetAdminCardPreviewHtml()
        {
            return $@"
                    <div class=""col-md-6 justify-content-center text-center mx-md-5 my-5 no_bg"">
                        <p class=""fw-bolder h3"">{Title.ToUpper()}</p>
                        <a class=""w-100 h-100 rcorners2 minigame_card"">
                            <img src=""{ThumbSource}"" alt=""buttonpng"" class=""img-fluid w-100 h-100 rounded_corners shadow"" />
                        </a>

                    </div>
                    ";
        }

    }
}