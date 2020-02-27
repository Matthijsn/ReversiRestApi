using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReversiRestApi.Models;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel/[action]")]
    [ApiController]
    public class ReversiController : ControllerBase
    {
        private static Spel SP1;
        //public ReversiController()
        //{
        //   Spellen = new List<Spel>();
        //}
        // GET: api/Spel
        [HttpGet("{token}")]
        public string HaalSpelOp(string token)
        {
            Spel sp = Spellen.FirstOrDefault(r => r.Token == token);
            // string sp = (from Spel in Spellen where Spel.Token == token select Spel.BordPrint).First();
            string spelJson = JsonConvert.SerializeObject(sp);
            return spelJson;
        }

        // GET: api/Beurt/5
        [HttpGet("{token}")]
        public string Beurt(string token)
        {
            Spel sp = Spellen.FirstOrDefault(r => r.Token == token);
            if (sp == null)
            {
                return "Spel kon niet gevonden worden!";
            }
            else
            {
                switch (sp.AandeBeurt)
                {
                    case Kleur.Wit:
                        return "1";
                    case Kleur.Zwart:
                        return "2";
                    case Kleur.Geen:
                        return "0";
                    default:
                        return null;
                }
            }
        }

        // POST: api/Spel
        [HttpPost]
        public string MaakSpel([FromBody] SpelInfo sp)
        {
            SP1 = new Spel();
            SP1.ID = sp.ID;
            SP1.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            SP1.Omschrijving = sp.Omschrijving;
            Speler s1 = new Speler(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            Speler s2 = new Speler(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            SP1.Spelers.Add(s1);
            SP1.Spelers.Add(s2);
            SpelInfo SPINF = new SpelInfo();
            SPINF.Speler1token = s1.Token;
            SPINF.Speler2token = s2.Token;
            SPINF.Speltoken = SP1.Token;
            Spellen.Add(SP1);
            string spelJson = JsonConvert.SerializeObject(SPINF);
            return spelJson;
        }

        // POST: api/Spel
        [HttpPost]
        public void Zet([FromBody] DoeZet value)
        {
            Spel sp = Spellen.FirstOrDefault(r => r.Token == value.Token);
            if (value.Pas)
            {
                sp.Pas();
            }
            else
            {
                sp.DoeZet(value.RijZet, value.KolomZet);
            }
        }

        // PUT: api/Spel/Zet
        [HttpPut]
        public void Post([FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public string GetToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        private static List<Spel> Spellen = new List<Spel>();
    }
}
