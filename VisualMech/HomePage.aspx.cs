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
                CommonGenres = "Adventure, Platformers, First Person Shooters, Racing, Open World, Role Playing Games", PossibleVariations = "Top-Down Movement, Side-on Movement, 3D Movement, Click to Move, Turn-based Movement", PossibleCombinations="This game mechanic is frequently utilized across various genres, a wide range of mechanics like shooting, gathering, and others are commonly integrated with the movement mechanism."
            , InteractiveControls = "W - Move Forward <br/>A - Move Left<br/>S - Move Backward<br/>D - Move Right"},
            new Card(){CardID="1", Title="SHOOTING MECHANIC", ImageSource = "Images/ShootingMechanic_Thumbnail.png", ThumbSource = "Images/shooting_thumb.png", Description = "Learn how to add shooting elements!", UnityLink = "https://almers5.github.io/Game-Mechanics/ShootingMechanic/",
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
                CommonGenres = "First Person Shooters, Third Person Shooters, Action Adventure, Battle Royale, Sci-Fi, Survival Horror", PossibleVariations = "Click to Shoot, Precision Shooting, Aim Down Sights, Projectile Types, Cover Based Shooting", PossibleCombinations="This game mechanic is mostly implemented on action base games which movement mechanics is needed with resource management."
                ,InteractiveControls = "Mouse Pointer - Aim<br/>Left Click Mouse Button - Shoot"},
            new Card(){CardID="2", Title="COLLECTING MECHANIC", ImageSource = "Images/CollectingMechanic_Thumbnail.png", ThumbSource = "Images/collecting_thumb.png", Description = "Want your game to have collecting mechanics? Go here!", UnityLink = "https://almers5.github.io/Game-Mechanics/CollectingMechanic/",
                CodeText = "public class Collect : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;[SerializeField] int value;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "private void OnTriggerEnter2D(Collider2D collision)<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "if (collision.gameObject.CompareTag(\"Player\"))<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "Destroy(gameObject);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StarCounter.instance.IncreaseStars(value);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}<br /><br />public class StarCounter : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "public static StarCounter instance;<br />&nbsp;&nbsp;&nbsp;&nbsp;public TMP_Text starText;<br />&nbsp;&nbsp;&nbsp;&nbsp;public int currentStars = 0;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "private void Awake()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;instance = this;<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "void Start()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;starText.text = \"STARS: \" + currentStars.ToString();<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;public void IncreaseStars(int starCount)<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "currentStars += starCount;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;starText.text = \"STARS: \" + currentStars.ToString();<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}",
                CommonGenres = "Horror, Battle Royale, Sandbox", PossibleVariations = "Key indicator to collect", PossibleCombinations="The potential combination of game mechanics integrated into this collection system would begin with a movement mechanic, followed by a building mechanic where the collected items serve a purpose, along with other action-based mechanics like melee and ranged attack systems."
                , InteractiveControls = "W - Move Forward <br/>A - Move Left<br/>S - Move Backward<br/>D - Move Right"},
            new Card(){CardID="3", Title="INTERACT MECHANIC", ImageSource = "Images/InteractMechanic_Thumbnail.png", ThumbSource = "Images/InteractMechanic_Icon.png", Description = "Want your game to have an interacting mechanics? Go here!", UnityLink = "https://almers5.github.io/Game-Mechanics/InteractMechanic/",
                CodeText = "public class NPC : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;public GameObject dialoguePanel;<br />&nbsp;&nbsp;&nbsp;&nbsp;public Text dialogueText;<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "public string[] dialogue;<br />&nbsp;&nbsp;&nbsp;&nbsp;private int index;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;public GameObject contButton;<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "public float wordSpeed;<br />&nbsp;&nbsp;&nbsp;&nbsp;public bool playerIsClose;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Update()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "if (Input.GetKeyDown(KeyCode.E) && playerIsClose)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "if (dialoguePanel.activeInHierarchy)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "zeroText();<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "else<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "dialoguePanel.SetActive(true);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StartCoroutine(Typing());<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if (dialogueText.text == dialogue[index])<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contButton.SetActive(true);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;public void zeroText()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialogueText.text = \"\";<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "index = 0;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dialoguePanel.SetActive(false);<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;IEnumerator Typing()<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;foreach(char letter in dialogue[index].ToCharArray())<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "dialogueText.text += letter;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;yield return new WaitForSeconds(wordSpeed);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;public void NextLine()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "contButton.SetActive(false);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if (index < dialogue.Length - 1)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;index++;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "dialogueText.text = \"\";<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StartCoroutine(Typing());<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;else<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "zeroText();<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;private void OnTriggerEnter2D(Collider2D collision)<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if (collision.CompareTag(\"Player\"))<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "playerIsClose = true;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;private void OnTriggerExit2D(Collider2D collision)<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if (collision.CompareTag(\"Player\"))<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "playerIsClose = false;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;zeroText();<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}",
                CommonGenres = "Visual Novel, Role Playing Games", PossibleVariations = "Text on Screen, Dialogue cloud on top of a character, Cinematic text view mode", PossibleCombinations="Variations of game mechanics that can be implmented to interact mechanic would be a turn-based system mechanic or a choose four options type of mechanic that would further enhance its gameplay features."
                , InteractiveControls = "Left Arrow Key or A - Move to Left<br/>Right Arrow Key or D - Move to Right<br/>Space Bar - Jump<br/>\"E\" - Press to interact"},
            new Card(){CardID="4", Title="HEALTH SYSTEM", ImageSource = "Images/HealthSystem_Thumbnail.png", ThumbSource = "Images/HealthSystem_Icon.png", Description = "Want your game to have health system mechanics? Go here!", UnityLink = "https://almers5.github.io/Game-Mechanics/HealthSystem/",
                CodeText = "public class playerHealth : MonoBehaviour<br />{<br />&nbsp;&nbsp;&nbsp;&nbsp;public float health;<br />&nbsp;&nbsp;&nbsp;&nbsp;public float maxHealth;<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "public Image healthBar;<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Start()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxHealth = health;<br />&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;void Update()<br />&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);<br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n" +
                "if (health <- 0)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Destroy(gameObject);<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                "}<br />&nbsp;&nbsp;&nbsp;&nbsp;}<br />}",
                CommonGenres = "First Person Shooter, Massive Online Battle Arena, Figting Games", PossibleVariations = "Health bar on top of the character, Text-based health, Health bar (symbols or icons), Red screen of danger", PossibleCombinations="The health system mechanic is a popular game mechanic that is combined in any type of action based game mechanics such as shooting mechanic, movement mechanic, combat system mechanic, etc."
                , InteractiveControls = "Left Arrow Key or A - Move to Left<br/>Right Arrow Key or D - Move to Right<br/>Space Bar - Jump"},
        };
        
        private string cardString = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (Card card in cardList)
            {
                cardString += card.GetCardHtml();
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
            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                context.Session["LearnId"] = cardId;
            }
        }
    }

}
