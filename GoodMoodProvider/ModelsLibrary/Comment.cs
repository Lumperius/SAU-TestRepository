using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class Comment
    {
        [Required]
        public Guid ID { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public List<News> News { get; set; }
    }
}
