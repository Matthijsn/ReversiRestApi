using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Models
{
    public class SpelInfo 
    {
        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public string BordPrint { get; }
        public string AanDeBeurt { get; set; }
        public string Status { get; set; }
        public string Speltoken { get; set; }
        public string Speler1token { get; set; }
        public string Speler2token { get; set; }
    }
}
