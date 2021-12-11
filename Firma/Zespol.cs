using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    class Zespol
    {
        private int liczbaCzlonkow;
        private string nazwa;
        private KierownikZespolu kierownik;
        private List<CzlonekZespolu> czlonkowie;

        public int LiczbaCzlonkow { get => liczbaCzlonkow; set => liczbaCzlonkow = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }
        public KierownikZespolu Kierownik { get => kierownik; set => kierownik = value; }

        public Zespol()
        {
            liczbaCzlonkow = 0;
            nazwa = null;
            kierownik = null;
            czlonkowie = new List<CzlonekZespolu>();
        }

        public Zespol(string Nazwa, KierownikZespolu Kierownik) : this()
        {
            nazwa = Nazwa;
            kierownik = Kierownik;
        }

        public void DodajCzlonka(CzlonekZespolu czlonekZespolu)
        {
            czlonkowie.Add(czlonekZespolu);
            liczbaCzlonkow++;
        }

        public override string ToString()
        {
            string TeamInfo = "Zespół: " + nazwa + "\n" + "Kierownik: " + kierownik.ToString() + "\n";
            foreach(CzlonekZespolu czlonek in czlonkowie)
            {
                TeamInfo += czlonek.ToString() + "\n";
            }
            return TeamInfo;
        }

        public bool JestCzlonkiem(string PESEL)
        {
            return czlonkowie.Any(x => x.Pesel == PESEL);
        }

        public bool JestCzlonkiem(string imie, string nazwisko)
        {
            return czlonkowie.Any(x => x.Imie == imie && x.Nazwisko == nazwisko);
        }

        public void UsunCzlonka(string PESEL)
        {
            if (this.JestCzlonkiem(PESEL))
            {
                var memberToRemove = czlonkowie.Single(x => x.Pesel == PESEL);
                czlonkowie.Remove(memberToRemove);
                liczbaCzlonkow--;
            }
            else
                Console.WriteLine("Nie ma członka o podanym numerze PESEL");
        }

        public void UsunCzlonka(string imie, string nazwisko)
        {
            if (this.JestCzlonkiem(imie, nazwisko))
            {
                var memberToRemove = czlonkowie.Single(x => x.Imie == imie && x.Nazwisko == nazwisko);
                czlonkowie.Remove(memberToRemove);
                liczbaCzlonkow--;
            }
            else
                Console.WriteLine("Nie ma członka o podanym imieniu i nazwisku");
        }

        public void UsunWszystkich()
        {
            czlonkowie.Clear();
            liczbaCzlonkow = 0;
        }

        public List<CzlonekZespolu> WyszukajCzlonkow(string funkcja)
        {
            return czlonkowie.FindAll(x => x.Funkcja == funkcja);
        }

        public List<CzlonekZespolu> WyszukajCzlonkow(int miesiac)
        {
            return czlonkowie.FindAll(x => x.DataZapisu.Month == miesiac);
        }
    }
}
