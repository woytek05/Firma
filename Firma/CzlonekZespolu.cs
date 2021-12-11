using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Firma
{
    class CzlonekZespolu : Osoba
    {
        private DateTime dataZapisu;
        private string funkcja;

        public DateTime DataZapisu { get => dataZapisu; set => dataZapisu = value; }
        public string Funkcja { get => funkcja; set => funkcja = value; }

        public CzlonekZespolu(string Imie, string Nazwisko, string DataUrodzenia, string Pesel, Plcie Plec, string Funkcja, string DataZapisu) : base(Imie, Nazwisko, DataUrodzenia, Pesel, Plec)
        {
            funkcja = Funkcja;
            DateTime.TryParseExact(DataZapisu, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy" }, null, DateTimeStyles.None, out dataZapisu);
        }

        public override string ToString()
        {
            return base.ToString() + " " + funkcja + " (" + dataZapisu.ToString("yyyy-MM-dd") + ")";
        }
    }
}
