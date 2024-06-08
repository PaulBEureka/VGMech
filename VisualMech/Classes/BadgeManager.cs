using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace VisualMech.Classes
{
    public class BadgeManager
    {
        private readonly string filePath;
        private  List<Badge> badges;
        private JavaScriptSerializer serializer;

        public BadgeManager(string fileName)
        {

            this.filePath = HttpContext.Current.Server.MapPath($"~/Jsons/{fileName}");
            this.serializer = new JavaScriptSerializer();

            if (!File.Exists(filePath))
            {
                // Handle the case where the file doesn't exist
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }
            LoadBadges();
        }

        private void LoadBadges()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                badges = serializer.Deserialize<List<Badge>>(jsonData);
            }
        }

        public List<Badge> GetBadges()
        {
            return badges;
        }

        public void SaveBadges()
        {
            string json = serializer.Serialize(badges);
            File.WriteAllText(filePath, json);
        }

        public void AddBadge(Badge badge)
        {
            badges.Add(badge);
            SaveBadges();
        }

        public void RemoveBadge(string badgeTitle)
        {
            Badge badgeToRemove = badges.FirstOrDefault(b => b.Title == badgeTitle);
            if (badgeToRemove != null)
            {
                badges.Remove(badgeToRemove);
                SaveBadges();
            }
            else
            {
                Console.WriteLine($"Badge with title '{badgeTitle}' not found.");
            }
        }

        public void UpdateBadge(string badgeTitle, Badge updatedBadge)
        {
            Badge badgeToUpdate = badges.FirstOrDefault(b => b.Title == badgeTitle);
            if (badgeToUpdate != null)
            {
                badgeToUpdate.Description = updatedBadge.Description;
                badgeToUpdate.Path = updatedBadge.Path;
                SaveBadges();
            }
            else
            {
                Console.WriteLine($"Badge with title '{badgeTitle}' not found.");
            }
        }

        
    }
}