﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class News
    {
        public Guid ID { get; set; }
        public string Article { get; set; }
        public string Body { get; set; }
        public string PlainText { get; set; }
        public string SourceSite { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public double WordRating  { get; set; }
        public double UserRating { get; set; }
        public double FinalRating { get; set; }

        public List<Comment> Comments;


    }
}
