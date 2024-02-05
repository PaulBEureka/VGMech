using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }

}