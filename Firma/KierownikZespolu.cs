using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    [Serializable]
    public class KierownikZespolu : Osoba, ICloneable
    {
        private int doswiadczenie;

        public int Doswiadczenie { get => doswiadczenie; set => doswiadczenie = value; }

        public KierownikZespolu() : base()
        {
            doswiadczenie = 0;
        }

        public KierownikZespolu(string Imie, string Nazwisko, string DataUrodzenia, string Pesel, Plcie Plec, int Doswiadczenie) : base(Imie, Nazwisko, DataUrodzenia, Pesel, Plec)
        {
            doswiadczenie = Doswiadczenie;
        }

        public override string ToString()
        {
            return Imie + " " + Nazwisko + ", " + Pesel + " " + DataUrodzenia.ToString("dd-MM-yyyy");
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
