using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Username { get; set; }
        public string DateCommented { get; set; }
        public string CommentContent { get; set; }
        public List<Comment> RepliesList { get; set; }

        public Comment(int commentId, string username, string dateCommented,string commentcontent)
        {
            CommentId = commentId;
            Username = username;
            DateCommented = dateCommented;
            CommentContent = commentcontent;
            RepliesList = new List<Comment>();
        }


    }

}