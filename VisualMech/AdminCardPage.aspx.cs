using System;
using System.Collections.Generic;
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

            foreach(LearnCard card in tempLearnList)
            {
                tempLearnString += card.GetAdminCardPreviewHtml();
            }

            foreach(MiniGameCard card in tempMinigameList)
            {
                tempMinigameString += card.GetAdminCardPreviewHtml();
            }



            return new string[] {tempLearnString, tempMinigameString };
        }

        protected void AddLearnBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            AddLearnPanel.Visible = true;
        }

        protected void AddMiniBtn_Click(object sender, EventArgs e)
        {
            PreviewCardPanel.Visible = false;
            AddMinigamePanel.Visible = true;
        }

        protected void CreateLearnBtn_Click(object sender, EventArgs e)
        {
            string title = TitleInput.Text.ToUpper(); 
            string bgPath = SaveImage(BgInputFile, ErrorLbl); 
            string iconpath = SaveImage(IconInputFile, ErrorLbl); 
            string description = ReplaceNewlinesWithHtmlBreaks(DecriptionInput.Text); 
            string unityLink = UnityLinkInput.Text;
            string genre = ReplaceNewlinesWithHtmlBreaks(CommonGenreInput.Text);
            string variation = ReplaceNewlinesWithHtmlBreaks(VariationInput.Text);
            string combination = ReplaceNewlinesWithHtmlBreaks(CombiInput.Text);
            string controls = ReplaceNewlinesWithHtmlBreaks(ControlInput.Text);
            string codeSample = ReplaceNewlinesWithHtmlBreaks(CodeSampleInput.Text);

            if(bgPath != null && iconpath != null)
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


        private string SaveImage(FileUpload fileUpload, Label errorLbl)
        {
            try
            {
                int maxFileSizeKB = 2000;
                int fileSizeKB = fileUpload.PostedFile.ContentLength / 1024;

                if (fileSizeKB > maxFileSizeKB)
                {
                    errorLbl.Text = $@"File size on {fileUpload.ID} exceeds the maximum limit (2MB).";
                    errorLbl.Visible = true;
                }

                string fileName = Path.GetFileName(fileUpload.FileName);
                string filePath = Server.MapPath("~/Images/") + fileName;
                fileUpload.SaveAs(filePath);

                string imagePath = "Images/" + fileName;
                return imagePath;
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
        }

        protected void CreateMiniBtn_Click(object sender, EventArgs e)
        {
            string title = TitleInputMini.Text.ToUpper();
            string bgPath = SaveImage(BgInputFileMini, ErrorLbl2);
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
    }
}