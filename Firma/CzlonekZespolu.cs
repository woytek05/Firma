using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Firma
{
    [Serializable]
    public class CzlonekZespolu : Osoba, ICloneable, IComparable<CzlonekZespolu>
    {
        private DateTime dataZapisu;
        private string funkcja;

        public DateTime DataZapisu { get => dataZapisu; set => dataZapisu = value; }
        public string Funkcja { get => funkcja; set => funkcja = value; }

        public CzlonekZespolu() : base()
        {
            dataZapisu = DateTime.Now;
            funkcja = null;
        }

        public CzlonekZespolu(string Imie, string Nazwisko, string DataUrodzenia, string Pesel, Plcie Plec, string Funkcja, string DataZapisu) : base(Imie, Nazwisko, DataUrodzenia, Pesel, Plec)
        {
            funkcja = Funkcja;
            DateTime.TryParseExact(DataZapisu, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy" }, null, DateTimeStyles.None, out dataZapisu);
        }

        public override string ToString()
        {
            return base.ToString() + " " + funkcja + " (" + dataZapisu.ToString("yyyy-MM-dd") + ")";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(CzlonekZespolu other)
        {
            if (other == null)
                return 1;
            var lastNameResult = Nazwisko.CompareTo(other.Nazwisko);
            if (lastNameResult != 0)
                return lastNameResult;
            return Imie.CompareTo(other.Imie);
        }
    }
}
