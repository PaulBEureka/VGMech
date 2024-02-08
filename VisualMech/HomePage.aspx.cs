using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class _Default : Page
    {
       
        private List<Card> cardList = new List<Card>() // Add Cards here and edit contents based on field, also increment the CardID each entry of card
        {
            new Card(){CardID="0", Title="MOVEMENT MECHANIC", ImageSource = "Images/movement_bg.png", ThumbSource = "Images/movement_icon.png", Description = "Get to know movement integration, variations, and more!", UnityLink = "https://almers5.github.io/Game-Mechanics/MovementMechanic",
                CodeText = "public class Player : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "[SerializeField] float moveSpeed = 10f;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Update()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />" +
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Move();<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "private void Move()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "// Time.deltaTime makes it the same movement for every computers FPS<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "var deltaX = Input.GetAxis(\"Horizontal\") * Time.deltaTime * moveSpeed;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "var deltaY = Input.GetAxis(\"Vertical\") * Time.deltaTime * moveSpeed;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "transform.position = new Vector2(newXPosition, newYPosition);<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="1", Title="SHOOTING MECHANIC", ImageSource = "Images/shooting_bg.png", ThumbSource = "Images/shooting_thumb.png", Description = "Learn how to add shooting elements!", UnityLink = "https://almers5.github.io/Game-Mechanics/ShootingMechanic/",
                CodeText = "public class Player : MonoBehaviour<br />{<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "[SerializeField] Rigidbody2D rb;<br />&nbsp;&nbsp;&nbsp;&nbsp;[SerializeField] Weapon weapon;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Vector2 mousePosition;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Update()<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "if (Input.GetButtonDown(\"Fire1\"))<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "weapon.Fire();<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;private void FixedUpdate()<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Vector2 aimDirection = mousePosition - rb.position;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "rb.rotation = aimAngle;<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}<br /><br />" +
                "public class Player : MonoBehaviour<br />{<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "[SerializeField] Rigidbody2D rb;<br />&nbsp;&nbsp;&nbsp;&nbsp;[SerializeField] Weapon weapon;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Vector2 mousePosition;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Update()<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "if (Input.GetButtonDown(\"Fire1\"))<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "weapon.Fire();<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "private void FixedUpdate()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Vector2 aimDirection = mousePosition - rb.position;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "rb.rotation = aimAngle;<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}<br /><br />" +
                "public class Bullet : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "private void OnCollisionEnter2D(Collision2D collision)<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Destroy(collision.gameObject);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Destroy(gameObject);<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="2", Title="COLLECTING MECHANIC", ImageSource = "Images/jump_card_bg.png", ThumbSource = "Images/collecting_thumb.png", Description = "Want your game to have collecting mechanics? Go here!", UnityLink = "https://paulbeureka.github.io/UnityGame1/Game_1/",
                CodeText = "CodeText here", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
        };
        
        private string cardString = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (Card card in cardList)
            {
                cardString += card.GetHtml();
            }

            Session["CardList"] = cardList;

            if (!IsPostBack)
            {
                litCardHtml.Text = cardString;
            }
        }


        [WebMethod]
        public static void ProcessIT(int cardId)
        {
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;

            // Check if HttpContext is available
            if (context != null)
            {
                // Use the session from the context to set the variable
                context.Session["CardId"] = cardId;
            }
        }
    }

}
