﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Classes
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Username { get; set; }
        public DateTime DateCommented { get; set; }
        public string CommentContent { get; set; }
        public int ReplyCount { get; set; }
        public string AvatarPath { get; set; }
        public string AboutMe { get; set; }

        public Comment(int commentId, string username, DateTime dateCommented,string commentcontent, string avatarPath, string aboutMe, int replyCount)
        {
            CommentId = commentId;
            Username = username;
            DateCommented = dateCommented;
            CommentContent = commentcontent;
            ReplyCount = replyCount;
            AvatarPath = avatarPath;
            AboutMe = aboutMe;
        }


    }

}