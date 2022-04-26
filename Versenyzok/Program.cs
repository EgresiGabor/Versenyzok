using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Versenyzok
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Versenyzo> lista = new List<Versenyzo>();

            //2. feladat
            using (StreamReader fajl = new StreamReader("pilotak.csv", Encoding.UTF8))
            {
                fajl.ReadLine();
                while (!fajl.EndOfStream)
                {
                    lista.Add(new Versenyzo(fajl.ReadLine()));
                }
            }

            //3. feladat
            Console.WriteLine($"3. feladat: {lista.Count}");

            //4. feladat
            Console.WriteLine($"4. feladat: {lista.Last().Nev}");

            //5. feladat
            Console.WriteLine("5. feladat:");
            lista.Where(x => x.SzuletesiDatum.CompareTo(DateTime.Parse("1901.01.01.")) == -1).ToList().ForEach(x => Console.WriteLine($"\t{x.Nev} ({(x.SzuletesiDatum.ToString("yyyy. MM. dd."))})"));

            //6. feladat
            Console.WriteLine($"6. feladat: {lista.Where(y => y.Rajtszam != null).ToList().OrderBy(x => x.Rajtszam).First().Nemzetiseg}");

            //7. feladat
            var rajtszamCsoport = lista.GroupBy(x => x.Rajtszam).Where(g=>g.Key != null && g.Count() > 1).Select(g=>new { 
                Rajtszam = (int)g.Key,
                Elofordulsa = g.Count()
            }).ToList();
            Console.Write("7. feladat: ");
            for (int i = 0; i < rajtszamCsoport.Count; i++)
            {
                if (i == rajtszamCsoport.Count - 1)
                {
                    Console.Write(rajtszamCsoport[i].Rajtszam);
                }
                else
                {
                    Console.Write(rajtszamCsoport[i].Rajtszam + ", ");
                }
            }
            Console.ReadKey();
        }
        class Versenyzo
        {
            string nev;
            DateTime szuletesiDatum;
            string nemzetiseg;
            int? rajtszam;

            public string Nev { get => nev; set => nev = value; }
            public DateTime SzuletesiDatum { get => szuletesiDatum; set => szuletesiDatum = value; }
            public string Nemzetiseg { get => nemzetiseg; set => nemzetiseg = value; }
            public int? Rajtszam { get => rajtszam; set => rajtszam = value; }
            public Versenyzo(string nev, DateTime szuletesiDatum, string nemzetiseg, int? rajtszam)
            {
                Nev = nev;
                SzuletesiDatum = szuletesiDatum;
                Nemzetiseg = nemzetiseg;
                Rajtszam = rajtszam;
            }

            public Versenyzo(string adatsor)
            {
                string[] adatok = adatsor.Split(';');

                Nev = adatok[0];
                SzuletesiDatum = DateTime.Parse(adatok[1]);
                Nemzetiseg = adatok[2];
                if (string.IsNullOrEmpty(adatok[3].Trim()))
                {
                    Rajtszam = null;
                }
                else
                {
                    Rajtszam = int.Parse(adatok[3].Trim());
                }
            }
        }
    }
}
