using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class WordRating
    {
        public Guid ID { get; set; }
        public string Word { get; set; }
        public double Rating { get; set; }
    }
}
