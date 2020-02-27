using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi
{
    public class Oplossing
    {
        public int RichtingX { get; set; }
        public int RichtingY { get; set; }
        public int Aantal { get; set; }

        public Oplossing(int richtingX, int richtingY, int aantal)
        {
            RichtingX = richtingX;
            RichtingY = richtingY;
            Aantal = aantal;
        }
    }
}
