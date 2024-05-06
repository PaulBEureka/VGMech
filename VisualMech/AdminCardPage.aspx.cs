using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private readonly CardManager<LearnCard> learnCardManager = new CardManager<LearnCard>("Learn.json");
        private readonly CardManager<MiniGameCard> miniGameCardManager = new CardManager<MiniGameCard>("MiniGame.json");

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] contentStrings = GenerateCardPreviews();
            LearnCardsPreviewLit.Text = contentStrings[0];
            MinigameCardsPreviewLit.Text = contentStrings[1];
        }



        private string[] GenerateCardPreviews()
        {
            string tempLearnString = "";
            string tempMinigameString = "";

            List<LearnCard> tempLearnList = learnCardManager.GetAllCards();
            List<MiniGameCard> tempMinigameList = miniGameCardManager.GetAllCards();

            foreach (LearnCard card in tempLearnList)
            {
                tempLearnString += card.GetAdminCardPreviewHtml();
            }

            foreach (MiniGameCard card in tempMinigameList)
            {
                tempMinigameString += card.GetAdminCardPreviewHtml();
            }



            return new string[] { tempLearnString, tempMinigameString };
        }

        protected void AddLearnBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            AddLearnPanel.Visible = true;
            SuccessLbl.Visible = false;
            ErrorLbl.Visible = false;
            BackBtn.Visible = true;
            ClearTextboxes();
        }

        protected void AddMiniBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            AddMinigamePanel.Visible = true;
            SuccessLbl2.Visible = false;
            ErrorLbl2.Visible = false;
            BackBtn.Visible = true;
            ClearTextboxes();
        }

        protected void CreateLearnBtn_Click(object sender, EventArgs e)
        {

            string title = TitleInput.Text.ToUpper();
            string bgPath = SaveImage(BgInputFile, ErrorLbl, "First");
            string iconpath = SaveImage(IconInputFile, ErrorLbl, bgPath);
            string description = ReplaceNewlinesWithHtmlBreaks(DecriptionInput.Text);
            string unityLink = UnityLinkInput.Text;
            string genre = ReplaceNewlinesWithHtmlBreaks(CommonGenreInput.Text);
            string variation = ReplaceNewlinesWithHtmlBreaks(VariationInput.Text);
            string combination = ReplaceNewlinesWithHtmlBreaks(CombiInput.Text);
            string controls = ReplaceNewlinesWithHtmlBreaks(ControlInput.Text);
            string codeSample = ReplaceNewlinesWithHtmlBreaks(CodeSampleInput.Text);

            if (bgPath != null && iconpath != null)
            {
                LearnCard newLearnCard = new LearnCard
                {
                    Title = title,
                    Description = description,
                    ThumbSource = iconpath,
                    ImageSource = bgPath,
                    CardID = (learnCardManager.GetAllCards().Count).ToString(),
                    UnityLink = unityLink,
                    CommonGenres = genre,
                    PossibleVariations = variation,
                    PossibleCombinations = combination,
                    InteractiveControls = controls,
                    CodeText = codeSample
                };

                learnCardManager.AddCard(newLearnCard);
                SuccessLbl.Visible = true;
            }

        }


        private string ReplaceNewlinesWithHtmlBreaks(string input)
        {
            return input.Replace("\n", "<br>");
        }


        private string SaveImage(FileUpload fileUpload, Label errorLbl, string previousVal)
        {
            try
            {

                
                int maxFileSizeKB = 2000;
                int fileSizeKB = fileUpload.PostedFile.ContentLength / 1024;

                if (fileSizeKB > maxFileSizeKB)
                {
                    errorLbl.Text = $@"File size on {fileUpload.ID} exceeds the maximum limit (2MB).";
                    errorLbl.Visible = true;

                    return null;
                }
                else if (previousVal != null)
                {
                    errorLbl.Visible = false;

                    string fileName = Path.GetFileName(fileUpload.FileName);
                    string filePath = Server.MapPath("~/Images/") + fileName;
                    fileUpload.SaveAs(filePath);

                    string imagePath = "Images/" + fileName;
                    return imagePath;
                }
                return null;
                
            }
            catch (Exception ex)
            {
                errorLbl.Text = $@"Error on {fileUpload.ID}, {ex}";
                errorLbl.Visible = true;
                return null;
            }
        }

        private void RemoveLearnCard(string cardID)
        {
            List<LearnCard> temp = learnCardManager.GetAllCards();
            LearnCard selectedCard = temp.FirstOrDefault(card => card.CardID == cardID);


            File.Delete(Server.MapPath(selectedCard.ThumbSource));
            File.Delete(Server.MapPath(selectedCard.ImageSource));
            learnCardManager.RemoveCard(selectedCard);
        }

        private void RemoveMinigameCard(string cardID)
        {
            List<MiniGameCard> temp = miniGameCardManager.GetAllCards();
            MiniGameCard selectedCard = temp.FirstOrDefault(card => card.CardID == cardID);
            File.Delete(Server.MapPath(selectedCard.ThumbSource));

            miniGameCardManager.RemoveCard(selectedCard);
            //Implement in another layout
        }

        protected void CreateMiniBtn_Click(object sender, EventArgs e)
        {
            
            string title = TitleInputMini.Text.ToUpper();
            string bgPath = SaveImage(BgInputFileMini, ErrorLbl2, "First");
            string unityLink = UnityLinkInputMini.Text;

            if (bgPath != null)
            {
                MiniGameCard newMinigameCard = new MiniGameCard
                {
                    Title = title,
                    CardID = (miniGameCardManager.GetAllCards().Count).ToString(),
                    UnityLink = unityLink,
                    ThumbSource = bgPath
                };

                miniGameCardManager.AddCard(newMinigameCard);
                SuccessLbl2.Visible = true;
            }
        }

        protected void EditLearnBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            EditLearnPanel.Visible = true;
            BackBtn.Visible = true;
            ShowLearnData();
        }

        protected void EditMiniBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            EditMiniPanel.Visible = true;
            BackBtn.Visible = true;
            ShowMiniData();
        }

        protected void LearnGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LearnGridView.EditIndex = e.NewEditIndex;
            ShowLearnData();
        }

        protected void LearnGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LearnGridView.EditIndex = -1;
            ShowLearnData();
        }

        protected void ShowLearnData()
        {

            List<LearnCard> learnList = learnCardManager.GetAllCardAsLiteral();

            LearnGridView.DataSource = learnList;
            LearnGridView.DataBind();
        }

        
        protected void LearnGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = LearnGridView.Rows[e.RowIndex];
            string cardID = ((Label)row.FindControl("lblCardID")).Text;
            string imageSource = ((TextBox)row.FindControl("txtImageSource")).Text;
            string thumbSource = ((TextBox)row.FindControl("txtThumbSource")).Text;
            string title = ((TextBox)row.FindControl("txtTitle")).Text;
            string description = ((TextBox)row.FindControl("txtDescription")).Text;
            string unityLink = ((TextBox)row.FindControl("txtUnityLink")).Text;
            string codeText = ((TextBox)row.FindControl("txtCodeText")).Text;
            string commonGenres = ((TextBox)row.FindControl("txtCommonGenres")).Text;
            string possibleVariations = ((TextBox)row.FindControl("txtPossibleVariations")).Text;
            string possibleCombinations = ((TextBox)row.FindControl("txtPossibleCombinations")).Text;
            string interactiveControls = ((TextBox)row.FindControl("txtInteractiveControls")).Text;


            LearnCard cardToUpdate = learnCardManager.GetAllCards().Find(c => c.CardID == cardID);
            if (cardToUpdate != null)
            {
                cardToUpdate.ImageSource = imageSource;
                cardToUpdate.ThumbSource = thumbSource;
                cardToUpdate.Title = title;
                cardToUpdate.Description = HttpUtility.HtmlDecode(description);
                cardToUpdate.UnityLink = HttpUtility.HtmlDecode(unityLink);
                cardToUpdate.CodeText = HttpUtility.HtmlDecode(codeText);
                cardToUpdate.CommonGenres = HttpUtility.HtmlDecode(commonGenres);
                cardToUpdate.PossibleVariations = HttpUtility.HtmlDecode(possibleVariations);
                cardToUpdate.PossibleCombinations = HttpUtility.HtmlDecode(possibleCombinations);
                cardToUpdate.InteractiveControls = HttpUtility.HtmlDecode(interactiveControls);
            }


            learnCardManager.SaveCards();

            LearnGridView.EditIndex = -1;
            ShowLearnData();
        }

        protected void LearnGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[0];

                foreach (Control control in cell.Controls)
                {
                    if (control is LinkButton)
                    {
                        LinkButton button = (LinkButton)control;
                        if (button.CommandName == "Delete")
                        {
                            string cardID = LearnGridView.DataKeys[e.Row.RowIndex]["CardID"].ToString();
                            button.OnClientClick = "return confirmDelete('" + cardID + "');";
                        }
                    }
                }
            }
        }

        protected void LearnGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cardID = LearnGridView.DataKeys[e.RowIndex].Value.ToString();
            RemoveLearnCard(cardID);
            ShowLearnData();
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            EditLearnPanel.Visible = false;
            EditMiniPanel.Visible = false;
            AddLearnPanel.Visible = false;
            AddMinigamePanel.Visible = false;

            PreviewCardPanel.Visible = true;
            BackBtn.Visible = false;
        }
    
        protected void ClearTextboxes()
        {
            foreach (Control control in Page.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
                if (control.HasControls())
                {
                    ClearTextBoxesChild(control);
                }
            }
        }

        protected void ClearTextBoxesChild(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                }
                if (control.HasControls())
                {
                    ClearTextBoxesChild(control);
                }
            }
        }


        protected void ShowMiniData()
        {

            List<MiniGameCard> miniList = miniGameCardManager.GetAllCardAsLiteral();

            MiniGridView.DataSource = miniList;
            MiniGridView.DataBind();
        }

        protected void MiniGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            MiniGridView.EditIndex = -1;
            ShowMiniData();
        }

        protected void MiniGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MiniGridView.EditIndex = e.NewEditIndex;
            ShowMiniData();
        }

        protected void MiniGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = MiniGridView.Rows[e.RowIndex];
            string cardID = ((Label)row.FindControl("lblCardIDMini")).Text;
            string thumbSource = ((TextBox)row.FindControl("txtThumbSourceMini")).Text;
            string title = ((TextBox)row.FindControl("txtTitleMini")).Text;
            string unityLink = ((TextBox)row.FindControl("txtUnityLinkMini")).Text;


            MiniGameCard cardToUpdate = miniGameCardManager.GetAllCards().Find(c => c.CardID == cardID);
            if (cardToUpdate != null)
            {
                cardToUpdate.ThumbSource = thumbSource;
                cardToUpdate.Title = title;
                cardToUpdate.UnityLink = HttpUtility.HtmlDecode(unityLink);
            }


            miniGameCardManager.SaveCards();

            MiniGridView.EditIndex = -1;
            ShowMiniData();
        }

        protected void MiniGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[0];

                foreach (Control control in cell.Controls)
                {
                    if (control is LinkButton)
                    {
                        LinkButton button = (LinkButton)control;
                        if (button.CommandName == "Delete")
                        {
                            string cardID = MiniGridView.DataKeys[e.Row.RowIndex]["CardID"].ToString();
                            button.OnClientClick = "return confirmDelete('" + cardID + "');";
                        }
                    }
                }
            }
        }

        protected void MiniGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cardID = MiniGridView.DataKeys[e.RowIndex].Value.ToString();
            RemoveMinigameCard(cardID);
            ShowMiniData();
        }
    }
}