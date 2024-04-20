using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace VisualMech.Classes
{
    public class CardManager<T>
    {
        private List<T> cards = new List<T>();
        private readonly string filePath;
        private JavaScriptSerializer serializer;

        public CardManager(string fileName)
        {
            this.filePath = HttpContext.Current.Server.MapPath($"~/Jsons/{fileName}");
            this.serializer = new JavaScriptSerializer();

            if (!File.Exists(filePath))
            {
                // Handle the case where the file doesn't exist
                throw new FileNotFoundException($"File '{filePath}' not found.");
            }

            LoadCards();
        }

        private void LoadCards()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                cards = serializer.Deserialize<List<T>>(json);
            }
        }

        private void SaveCards()
        {
            string json = serializer.Serialize(cards);
            File.WriteAllText(filePath, json);
        }

        public void AddCard(T card)
        {
            cards.Add(card);
            SaveCards();
        }

        public void RemoveCard(T card)
        {
            cards.Remove(card);
            SaveCards();
        }

        public List<T> GetAllCards()
        {
            return cards;
        }
    }
}