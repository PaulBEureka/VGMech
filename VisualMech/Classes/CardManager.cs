using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using VisualMech.Content.Classes;

namespace VisualMech.Classes
{
    public class CardManager<T>
    {
        public static List<T> cards = new List<T>();
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

                
                foreach (T card in cards)
                {
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            string value = (string)property.GetValue(card);
                            if (value != null)
                            {
                                property.SetValue(card, HttpUtility.HtmlDecode(value));
                            }
                        }
                    }
                }
            }
        }


        


        public void SaveCards()
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


        public List<T> GetAllCardAsLiteral()
        {
            foreach (T card in cards)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        string value = (string)property.GetValue(card);
                        if (value != null)
                        {
                            property.SetValue(card, HttpUtility.HtmlEncode(value));
                        }
                    }
                }
            }
            return cards;
        }
    }
}