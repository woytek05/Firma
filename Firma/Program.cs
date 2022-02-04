using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    class Program
    {
        static void Main(string[] args)
        {
            CzlonekZespolu czlonek1 = new CzlonekZespolu("Witold", "Adamski", "1992-10-22", "92102266738", Plcie.M, "sekretarz", "2020-01-01");
            CzlonekZespolu czlonek2 = new CzlonekZespolu("Jan", "Janowski", "1992-03-15", "92031532652", Plcie.M, "programista", "2020-01-01");
            CzlonekZespolu czlonek3 = new CzlonekZespolu("Beata", "Nowak", "1993-11-22", "93112225023", Plcie.K, "projektant", "2020-01-01");
            CzlonekZespolu czlonek4 = new CzlonekZespolu("Jan", "But", "1992-05-16", "92051613915", Plcie.M, "programista", "2019-06-01");
            CzlonekZespolu czlonek5 = new CzlonekZespolu("Anna", "Mysza", "1991-07-22", "91072235964", Plcie.K, "projektant", "2019-07-31");

            KierownikZespolu kierownik = new KierownikZespolu("Adam", "Kowalski", "1990-07-01", "90070142412", Plcie.M, 5);
            Zespol zespol = new Zespol("Grupa IT", kierownik);

            zespol.DodajCzlonka(czlonek1);
            zespol.DodajCzlonka(czlonek2);
            zespol.DodajCzlonka(czlonek3);
            zespol.DodajCzlonka(czlonek4);
            zespol.DodajCzlonka(czlonek5);

            Console.WriteLine(zespol.ToString());
            Zespol.ZapiszXML("zespol.xml", zespol);
            Zespol zespol2 = Zespol.OdczytajXML("zespol.xml");

            Console.WriteLine(zespol2.ToString());

            Console.ReadKey();
        }
    }
}
