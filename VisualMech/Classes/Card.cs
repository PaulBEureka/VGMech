using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public abstract class Card
    {
        public abstract string GetCardHtml();
        public abstract string GetContentHtml(string sessionUserID = null);
        public abstract string GetAdminCardPreviewHtml();


    }
}