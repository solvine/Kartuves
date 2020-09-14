using System;
using System.Collections.Generic;
using System.Linq;

namespace KartuvesDatabase
{
    public class Hangman
    {
        public static void Kartuves()
        {
            using (var db = new KartuvesContext())
            {
            
                 bool zaisti = PasirenkaVeiksma(db);
                Console.WriteLine("Iveskite savo varda");
                string vardas = Console.ReadLine();

                while (zaisti)
            {
                List<char> spetosRaides = new List<char>();
                List<string> spetiZodziai = new List<string>();

                var rand = new Random();

                var pasirinkimas = ZmogusPasirenkaZodziuGrupe(out string zodziuGrupe);
     
                var zodis = ZodzioParinkimas(pasirinkimas, rand, db);

                char[] zodisCharArray = GetEmptyZodisArray(zodis.Length);

                bool spejimaiBaigti = false;
                int klaidos = 0;
                

                    while (!spejimaiBaigti)
                {
                    Console.Clear();
                    KartuviuPaveikslelis(klaidos);

                    Console.WriteLine($"Spejamas zodis is grupes: {zodziuGrupe}.");
                    PrintCharAray(zodisCharArray);

                    PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);

                    Console.WriteLine("Spekite raide arba visa zodi:");
                    var spejimas = Console.ReadLine().ToUpper();

                    while (!(ArGerasSpejimas(spejimas) &&
                        ArNesikartojaSpejimas(spejimas, spetosRaides, spetiZodziai)))
                    {
                        Console.WriteLine("Spekite raide arba visa zodi:");
                        spejimas = Console.ReadLine().ToUpper();
                    }

                    IsimintiSpejima(spejimas, spetosRaides, spetiZodziai);
                    

                    if (!SpejimoAnalize(spejimas, zodis, zodisCharArray))
                    {
                        klaidos++;
                    }

                    if (klaidos == 7)
                    {
                        Console.Clear();
                        KartuviuPaveikslelis(klaidos);
                        PrintCharAray(zodisCharArray);
                        PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);
                        Console.WriteLine();
                        Console.WriteLine($"JUS PRALAIMEJOTE =( ! ");
                        spejimaiBaigti = true;
                        PridetiZodziuStatistika(pasirinkimas, zodis, db, false);
                        string spejimai = SpejimaiToString(spetosRaides, spetiZodziai);
                        db.Spejimai.Add(new Spejimas { ZaidejoVardas = vardas, KiekKartuSpejo = spetosRaides.Count()+spetiZodziai.Count(), ArAtspejo = false, SpetasZodis = zodis, ZaidimoData = DateTime.Now, Spejimai = spejimai });
                        db.SaveChanges();
                        
                        }
                    else if (ArLaimejo(zodisCharArray))
                    {
                        Console.Clear();
                        KartuviuPaveikslelis(klaidos);
                        PrintCharAray(zodisCharArray);
                        PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);
                        Console.WriteLine();
                        Console.WriteLine($"JUS LAIMEJOTE =) !");
                        spejimaiBaigti = true;
                        PridetiZodziuStatistika(pasirinkimas, zodis, db, true);
                        string spejimai = SpejimaiToString(spetosRaides, spetiZodziai);
                        db.Spejimai.Add(new Spejimas { ZaidejoVardas = vardas, KiekKartuSpejo = spetosRaides.Count() + spetiZodziai.Count(), ArAtspejo = true, SpetasZodis = zodis, ZaidimoData = DateTime.Now, Spejimai = spejimai });
                        db.SaveChanges();
                    }

                    }
                    zaisti = ArNoriteToliauZaisti();

                    Console.Clear();


                }
            }
        }

        static bool PasirenkaVeiksma(KartuvesContext db)
        {
            Console.WriteLine("Pasirinkite veiksma:");
            Console.WriteLine("Rodyti zaidimu statistika - 1");
            Console.WriteLine("Pradeti zaisti - 2");
            var pasirinkimas = Console.ReadLine();

            while (pasirinkimas != "1" & pasirinkimas != "2")
            {
                Console.Clear();
                Console.WriteLine("Pasirinkite veiksma:");
                Console.WriteLine("Rodyti zaidimu statistika - 1");
                Console.WriteLine("Pradeti zaisti - 2");
                pasirinkimas = Console.ReadLine();
            }

            if (pasirinkimas == "1")
            {
                if (db.Spejimai.Count()>0)
                {
                    RodytiZaidimuStatistika(db);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Zaidimo statistikos dar nera");
                    Console.WriteLine();
                }
                
            }

            return true;

        }
        static void RodytiZaidimuStatistika(KartuvesContext db)
        {
            List<string> zaidejai = new List<string>();

            foreach (var zaidejas in db.Spejimai)
            {
                if (!zaidejai.Contains(zaidejas.ZaidejoVardas))
                {
                    zaidejai.Add(zaidejas.ZaidejoVardas);
                }
            }

            foreach (var item in zaidejai)
            {
                var kiekZodziuSpeta = db.Spejimai.Where(s => s.ZaidejoVardas == item).Count();
                var kiekZodziuAtspeta = db.Spejimai.Where(s => s.ZaidejoVardas == item & s.ArAtspejo == true).Count()*100/kiekZodziuSpeta;
                Console.WriteLine($"Zaidejas: {item}, spejo zodziu: {kiekZodziuSpeta}, atspejo: {kiekZodziuAtspeta}%");
            }
            
        }
        static void PridetiZodziuStatistika(int pasirinkimas, string zodis, KartuvesContext db, bool arAtspejo)
        {
            
            switch (pasirinkimas)
            {
                case 1:
                    var spetasVardasId = db.Vardai.First(v => v.Pavadinimas == zodis).VardasID;

                    foreach (var item in db.Vardai.Where(v=>v.VardasID == spetasVardasId))
                    {
                        item.KiekSpeta++;
                        if (arAtspejo)
                        {
                            item.KiekAtspeta++;
                        }
                    }
                    
                    break;
                case 2:
                    var spetasMmiestasId = db.Miestai.First(v => v.Pavadinimas == zodis).MiestasID;

                    foreach (var item in db.Miestai.Where(v => v.MiestasID == spetasMmiestasId))
                    {
                        item.KiekSpeta++;
                        if (arAtspejo)
                        {
                            item.KiekAtspeta++;
                        }
                    }
                    
                    break;
                case 3:
                    var spetaValstybeId = db.Valstybes.First(v => v.Pavadinimas == zodis).ValstybeID;

                    foreach (var item in db.Valstybes.Where(v => v.ValstybeID == spetaValstybeId))
                    {
                        item.KiekSpeta++;
                        if (arAtspejo)
                        {
                            item.KiekAtspeta++;
                        }
                    }
                    
                    break;
                case 4:
                    var spetasGyvunasId = db.Gyvunai.First(v => v.Pavadinimas == zodis).GyvunasID;

                    foreach (var item in db.Gyvunai.Where(v => v.GyvunasID == spetasGyvunasId))
                    {
                        item.KiekSpeta++;
                        if (arAtspejo)
                        {
                            item.KiekAtspeta++;
                        }
                    }
                    
                    break; ;
                case 5:
                    var spetasDaiktasId = db.Daiktai.First(v => v.Pavadinimas == zodis).DaiktasID;

                    foreach (var item in db.Daiktai.Where(v => v.DaiktasID == spetasDaiktasId))
                    {
                        item.KiekSpeta++;
                        if (arAtspejo)
                        {
                            item.KiekAtspeta++;
                        }
                    }
                    
                    break;
                default:
                    break;
            }
        }
        static string SpejimaiToString(List<char> spetosRaides, List<string> spetiZodziai)
        {
            string spejimai = string.Empty;
            foreach (var item in spetosRaides)
            {
                spejimai = $"{spejimai} {item} ";
            }

            foreach (var item in spetiZodziai)
            {
                spejimai = $"{spejimai} {item} ";
            }
            return spejimai;
        }
        static bool ArNoriteToliauZaisti()
        {
            Console.WriteLine("Ar norite zaisti dar karta? y/n");
            string ivestis = Console.ReadLine().ToLower();

            while (ivestis != "y" && ivestis != "n")
            {
                Console.Clear();
                Console.WriteLine("Ar norite zaisti dar karta? y/n");
                ivestis = Console.ReadLine().ToLower();
            }

            if (ivestis == "y")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        static bool ArLaimejo(char[] zodisCharArray)
        {
            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                if (zodisCharArray[i] == '_')
                {
                    return false;
                }
            }
            return true;
        }

        static void PrintSpetiZodziaiIrRaides(List<char> spetosRaides, List<string> spetiZodziai)
        {
            Console.WriteLine();
            Console.WriteLine("Spetos raides:");
            foreach (var raide in spetosRaides)
            {
                Console.Write($"{raide}, ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Speti zodziai:");
            foreach (var zodis in spetiZodziai)
            {
                Console.Write($"{zodis}, ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static bool ArNesikartojaSpejimas(string spejimas, List<char> spetosRaides, List<string> spetiZodziai)
        {
            if (spejimas.Length == 1)
            {
                for (int i = 0; i < spetosRaides.Count; i++)
                {
                    if (spejimas[0] == spetosRaides[i])
                    {
                        Console.WriteLine($"Raide {spejimas[0]} jau buvo speta.");
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < spetiZodziai.Count; i++)
                {
                    if (spejimas == spetiZodziai[i])
                    {
                        Console.WriteLine($"Zodis {spejimas} jau buvo spetas.");
                        return false;
                    }
                }
            }
            return true;
        }

        static bool ArGerasSpejimas(string spejimas)
        {
            if (spejimas.Length == 1)
            {
                if (spejimas[0] >= 65 && spejimas[0] <= 90)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Simbolis {spejimas[0]} nera raide!");
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < spejimas.Length; i++)
                {
                    if (!(spejimas[i] >= 65 && spejimas[i] <= 90))
                    {
                        Console.WriteLine($"{spejimas} nera zodis!");
                        return false;
                    }
                }
            }
            return true;
        }

        static void IsimintiSpejima(string spejimas, List<char> spetosRaides, List<string> spetiZodziai)
        {
            if (spejimas.Length == 1)
            {
                spetosRaides.Add(spejimas[0]);
             
            }
            else
            {
                spetiZodziai.Add(spejimas);
               
            }
        }

        static bool SpejimoAnalize(string spejimas, string zodis, char[] zodisCharArray)
        {
            bool arAtspejo = false;

            if (spejimas.Length == 1)
            {
                for (int i = 0; i < zodis.Length; i++)
                {
                    if (zodis[i] == spejimas[0])
                    {
                        zodisCharArray[i] = spejimas[0];
                        arAtspejo = true;
                    }
                }
            }
            else
            {
                if (zodis == spejimas)
                {
                    arAtspejo = true;

                    for (int i = 0; i < zodis.Length; i++)
                    {
                        zodisCharArray[i] = zodis[i];
                    }
                }
            }

            return arAtspejo;
        }

        static void PrintCharAray(char[] zodisCharArray)
        {
            string spejamasZodis = string.Empty;

            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                spejamasZodis += $"{zodisCharArray[i]} ";
            }

            Console.WriteLine(spejamasZodis);
        }

        static char[] GetEmptyZodisArray(int masyvoDydis)
        {
            char[] zodisCharArray = new char[masyvoDydis];

            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                zodisCharArray[i] = '_';
            }

            return zodisCharArray;
        }

        static string ZodzioParinkimas(int pasirinkimas, Random rand, KartuvesContext db)
        {

           string zodis = string.Empty;

            switch (pasirinkimas)
            {
                case 1:
                    var id1 = rand.Next(1, db.Vardai.Count() + 1);
                    zodis = db.Vardai.First(v => v.VardasID == id1).Pavadinimas;
                    break;
                case 2:
                    var id2 = rand.Next(1, db.Miestai.Count() + 1);
                    zodis = db.Miestai.First(v => v.MiestasID == id2).Pavadinimas;
                    break;
                case 3:
                    var id3 = rand.Next(1, db.Valstybes.Count() + 1);
                    zodis = db.Valstybes.First(v => v.ValstybeID == id3).Pavadinimas;
                    break;
                case 4:
                    var id4 = rand.Next(1, db.Gyvunai.Count() + 1);
                    zodis = db.Gyvunai.First(v => v.GyvunasID == id4).Pavadinimas;
                    break;
                case 5:
                    var id5 = rand.Next(1, db.Daiktai.Count() + 1);
                    zodis = db.Daiktai.First(v => v.DaiktasID == id5).Pavadinimas;
                    break;
                default:
                    break;
            }
                
                
               
                return zodis;
            }

        static int ZmogusPasirenkaZodziuGrupe(out string zodziuGrupe)
        {
            var pasirinkimas = ReadIntNumber();

            switch (pasirinkimas)
            {
                case 1:
                    zodziuGrupe = "VARDAI";
                    break;
                case 2:
                    zodziuGrupe = "LIETUVOS MIESTAI";
                    break;
                case 3:
                    zodziuGrupe = "VALSTYBES";
                    break;
                case 4:
                    zodziuGrupe = "GYVUNAI";
                    break;
                case 5:
                    zodziuGrupe = "DAIKTAI";
                    break;
                default:
                    zodziuGrupe = "";
                    break;
            }

            return pasirinkimas;
        }

        static int ReadIntNumber()
        {
            int skaicius;

            Console.WriteLine("Pasirinkite is kokios zodziu grupes norite zodzio:");
            Console.WriteLine("1 - VARDAI");
            Console.WriteLine("2 - MIESTAI");
            Console.WriteLine("3 - VALSTYBES");
            Console.WriteLine("4 - GYVUNAI");
            Console.WriteLine("5 - DAIKTAI");

            while (!(int.TryParse(Console.ReadLine(), out skaicius) &&
                skaicius >= 1 && skaicius <= 5))
            {
                Console.Clear();
                Console.WriteLine("Blogas pasirinkimas.");
                Console.WriteLine();
                Console.WriteLine("Pasirinkite is kokios zodziu grupes norite zodzio:");
                Console.WriteLine("1 - VARDAI");
                Console.WriteLine("2 - LIETUVOS MIESTAI");
                Console.WriteLine("3 - VALSTYBES");
                Console.WriteLine("4 - GYVUNAI");
            }

            return skaicius;
        }

        static void KartuviuPaveikslelis(int a)
        {
            switch (a)
            {
                case 0:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /");
                    Console.WriteLine(@"|/");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 1:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 2:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/     |");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 3:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/     | ");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 4:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 5:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 6:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|     /");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 7:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|     / \");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                default:
                    break;
            }


        }
    }
}
