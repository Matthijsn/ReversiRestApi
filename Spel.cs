using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiRestApi.Models
{
    public class Spel : ISpel
    {
        public int ID { get; set; }
        public string Omschrijving { get; set; }
        public string Token { get; set; }
        public ICollection<Speler> Spelers { get; set; }
        public Kleur[,] Bord { get; set; }
        public Kleur AandeBeurt { get; set; }
        public string BordPrint { get; }
        public string status { get; set; }
        Oplossing O1;
        List<Oplossing> Oplossingen;
        public Spel()
        {
            Bord = new Kleur[9, 9];
            Bord[3, 3] = Kleur.Wit;
            Bord[4, 4] = Kleur.Wit;
            Bord[3, 4] = Kleur.Zwart;
            Bord[4, 3] = Kleur.Zwart;
            status = "Spel Begonnen";
            Spelers = new List<Speler>();
            this.UpdateBordPrint();
        }
        public void UpdateBordPrint()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int x = 0; x <= 7; x++)
            {
                sb.Append("[");
                for (int y = 0; y <= 7; y++)
                {
                    sb.Append($"{Bord[x, y]}, ");
                }
                sb.Append("]");
            }
            sb.Append("]");
        }
        public bool Afgelopen()
        {
            bool Return = true;
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (Bord[x, y] != this.AndereKleur())
                    {
                        if (ZetMogelijk(x, y))
                        {
                            Return = false;
                        }
                    }
                }
            }
            this.AandeBeurt = this.AndereKleur();

            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (Bord[x, y] != this.AndereKleur())
                    {
                        if (ZetMogelijk(x, y))
                        {
                            Return = false;
                        }
                    }
                }
            }
            return Return;
        }

        public bool DoeZet(int rijZet, int kolomZet)
        {
            if (ZetMogelijk(rijZet, kolomZet))
            {
                foreach (Oplossing O1 in Oplossingen)
                {
                    Bord[rijZet, kolomZet] = this.AandeBeurt;
                    if (O1.RichtingX == -1)
                    {
                        rijZet--;
                    }
                    else if (O1.RichtingX == 1)
                    {
                        rijZet++;
                    }
                    if (O1.RichtingY == -1)
                    {
                        kolomZet--;
                    }
                    else if (O1.RichtingY == 1)
                    {
                        kolomZet++;
                    }
                    int i = 0;
                    while (i < O1.Aantal)
                    {
                        Bord[rijZet, kolomZet] = this.AandeBeurt;
                        if (O1.RichtingX == -1)
                        {
                            rijZet--;
                        }
                        else if (O1.RichtingX == 1)
                        {
                            rijZet++;
                        }
                        if (O1.RichtingY == -1)
                        {
                            kolomZet--;
                        }
                        else if (O1.RichtingY == 1)
                        {
                            kolomZet++;
                        }
                        i++;
                    }

                }
                this.AandeBeurt = this.AndereKleur();
                this.UpdateBordPrint();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Kleur OverwegendeKleur()
        {
            Kleur Return = Kleur.Geen;
            int aantalZwart = 0;
            int aantalWit = 0;
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (Bord[x, y] == Kleur.Zwart)
                    {
                        aantalZwart++;
                    }
                    else if (Bord[x, y] == Kleur.Wit)
                    {
                        aantalWit++;
                    }
                }
            }
            if (aantalZwart > aantalWit)
            {
                status = "Zwart heeft gewonnen";
                Return = Kleur.Zwart;
            }
            else if (aantalWit > aantalZwart)
            {
                status = "Wit heeft gewonnen";
                Return = Kleur.Wit;
            }
            return Return;
        }

        public bool Pas()
        {
            bool Return = true;
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (Bord[x, y] == this.AandeBeurt)
                    {
                        if (ZetMogelijk(x, y))
                        {
                            Return = false;
                        }
                    }
                }
            }
            if (Return)
            {
                this.AandeBeurt = this.AndereKleur();
            }
            return Return;
        }
        public Kleur AndereKleur()
        {
            if (this.AandeBeurt == Kleur.Wit)
            {
                return Kleur.Zwart;
            }
            else
            {
                return Kleur.Wit;
            }
        }
        public Oplossing AantalOmdraaien(int rijZet, int kolomZet, int RichtingX, int RichtingY)
        {
            int count = 1;
            int code = 1;
            if (RichtingX == -1)
            {
                rijZet--;
            }
            else if (RichtingX == 1)
            {
                rijZet++;
            }
            if (RichtingY == -1)
            {
                kolomZet--;
            }
            else if (RichtingY == 1)
            {
                kolomZet++;
            }
            while (BinnenHetBord(rijZet, kolomZet) && code == 1)
            {

                if (Bord[rijZet, kolomZet] == this.AandeBeurt && count >= 1)
                {
                    code = 2;
                    break;
                }
                else if (Bord[rijZet, kolomZet] == this.AndereKleur())
                {
                    count++;
                }
                else
                {
                    code = 0;
                    count = 0;
                }
                if (RichtingX == -1)
                {
                    rijZet--;
                }
                else if (RichtingX == 1)
                {
                    rijZet++;
                }
                if (RichtingY == -1)
                {
                    kolomZet--;
                }
                else if (RichtingY == 1)
                {
                    kolomZet++;
                }
            }
            if (code == 2)
            {
                return new Oplossing(RichtingX, RichtingY, count - 1);
            }
            else
            {
                return new Oplossing(RichtingX, RichtingY, 0);
            }
        }

        public bool BinnenHetBord(int rijZet, int kolomZet)
        {
            if ((rijZet >= 0 && rijZet <= 7) && (kolomZet >= 0 && kolomZet <= 7))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ZetMogelijk(int rijZet, int kolomZet)
        {
            if (this.Bord[rijZet, kolomZet] == Kleur.Geen)
            {
                Oplossing B1 = AantalOmdraaien(rijZet, kolomZet, -1, -1);
                Oplossing B2 = AantalOmdraaien(rijZet, kolomZet, -1, 0);
                Oplossing B3 = AantalOmdraaien(rijZet, kolomZet, -1, 1);
                Oplossing B4 = AantalOmdraaien(rijZet, kolomZet, 0, -1);
                Oplossing B5 = AantalOmdraaien(rijZet, kolomZet, 0, 1);
                Oplossing B6 = AantalOmdraaien(rijZet, kolomZet, 1, -1);
                Oplossing B7 = AantalOmdraaien(rijZet, kolomZet, 1, 0);
                Oplossing B8 = AantalOmdraaien(rijZet, kolomZet, 1, 1);
                Oplossing[] Getallen = new Oplossing[8];
                Getallen[0] = (B1);
                Getallen[1] = (B2);
                Getallen[2] = (B3);
                Getallen[3] = (B4);
                Getallen[4] = (B5);
                Getallen[5] = (B6);
                Getallen[6] = (B7);
                Getallen[7] = (B8);
                O1 = new Oplossing(0, 0, 0);
                Oplossingen = new List<Oplossing>();
                foreach (Oplossing o1 in Getallen)
                {
                    if (o1.Aantal > O1.Aantal && o1.Aantal > 0)
                    {
                        O1 = o1;
                        Oplossingen.Add(o1);
                    }
                }

                if (O1.Aantal > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //getallen.sum
            }
            else
            {
                return false;
            }
        }
    }
}
