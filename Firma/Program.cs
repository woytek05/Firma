using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    enum Plcie
    {
        K,
        M
    }
    class Program
    {
        static void Main(string[] args)
        {
            Osoba osoba1 = new Osoba("Beata", "Nowak", "1992-10-22", "92102201347", Plcie.K);
            Osoba osoba2 = new Osoba("Jan", "Janowski", "1993-03-15", "92031507772", Plcie.M);
            Osoba ja = new Osoba("Wojciech", "Wysocki", "2005-09-23", "05292301314", Plcie.M);

            Console.WriteLine(ja.Pesel);
            if (ja.CorrectPESEL())
                Console.WriteLine("Poprawny PESEL!");
            else
                Console.WriteLine("Niepoprawny PESEL!");

            Console.ReadKey();
        }
    }
}
