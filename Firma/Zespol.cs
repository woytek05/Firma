using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Firma
{
    [Serializable]
    class Zespol : ICloneable, IZapisywalna
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
            kierownik = new KierownikZespolu();
            czlonkowie = new List<CzlonekZespolu>();
        }

        public Zespol(string Nazwa, KierownikZespolu x) : this()
        {
            nazwa = Nazwa;
            kierownik.Imie = x.Imie;
            kierownik.Nazwisko = x.Nazwisko;
            kierownik.DataUrodzenia = x.DataUrodzenia;
            kierownik.Pesel = x.Pesel;
            kierownik.Plec = x.Plec;
            kierownik.Doswiadczenie = x.Doswiadczenie;
        }

        public void DodajCzlonka(CzlonekZespolu czlonekZespolu)
        {
            czlonkowie.Add(czlonekZespolu);
            liczbaCzlonkow++;
        }

        public override string ToString()
        {
            string TeamInfo = "Zespół: " + nazwa + "\n" + "Kierownik: " + kierownik.ToString() + "\n";
            foreach (CzlonekZespolu czlonek in czlonkowie)
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
            if (JestCzlonkiem(PESEL))
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
            if (JestCzlonkiem(imie, nazwisko))
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

        public object Clone()
        {
            Zespol clone = (Zespol)MemberwiseClone();
            clone.czlonkowie = new List<CzlonekZespolu>();

            foreach (CzlonekZespolu czlonek in czlonkowie)
            {
                clone.czlonkowie.Add((CzlonekZespolu)czlonek.Clone());
            }

            return clone;
        }

        public void Sortuj()
        {
            czlonkowie.Sort((x, y) => x.CompareTo(y));
        }

        public void SortujPoPESEL()
        {
            czlonkowie.Sort(new PESELComparer());
        }

        public void ZapiszBin(string nazwa)
        {
            FileStream fs = new FileStream(nazwa, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Zapis danych do pliku binarnego nie udał się. Przyczyna: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public object OdczytajBin(string nazwa)
        {
            Zespol zespol = null;
            FileStream fs = new FileStream(nazwa, FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                zespol = (Zespol)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Odczyt danych z pliku binarnego nie udał się. Przyczyna: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return zespol;
        }

        public static void ZapiszXML(string nazwa, Zespol z)
        {
            using (var stream = new FileStream(nazwa, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(Zespol));
                XML.Serialize(stream, z);
            }
        }
    }
}
