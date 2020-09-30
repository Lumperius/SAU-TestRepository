using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLibrary.Responces
{
    public class GetNewsResponse
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string PlainText { get; set; }
        public string Source { get; set; }
        public double Rating { get; set; }
    }
}
