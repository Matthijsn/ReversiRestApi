using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi
{
    public class Speler
    {
        public string Token { get; set; }

        public Speler(string token)
        {
            Token = token;
        }
    }
}
