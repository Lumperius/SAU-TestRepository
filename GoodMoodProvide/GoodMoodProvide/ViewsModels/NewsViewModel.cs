﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.ViewsModels
{
    public class NewsViewModel
    {
        [Required]
        public string Article { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime DatePublished { get; set; }

        public string Author { get; set; }

        public string OriginSite { get; set; }

    }
}