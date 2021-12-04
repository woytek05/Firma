using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Firma
{
    class Osoba
    {
        private string imie;
        private string nazwisko;
        private DateTime dataUrodzenia;
        private string PESEL;
        private Plcie plec;
        private string numerTelefonu;

        public Osoba()
        {
            imie = "Jan";
            nazwisko = "Kowalski";
            dataUrodzenia = Convert.ToDateTime("1970-01-01");
            PESEL = "00000000000";
        }

        public Osoba(string Imie, string Nazwisko)
        {
            imie = Imie;
            nazwisko = Nazwisko;
        }

        public Osoba(string Imie, string Nazwisko, string DataUrodzenia, string Pesel, Plcie Plec)
        {
            imie = Imie;
            nazwisko = Nazwisko;
            DateTime.TryParseExact(DataUrodzenia, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy" }, null, DateTimeStyles.None, out dataUrodzenia);
            PESEL = Pesel;
            plec = Plec;
        }

        public Osoba(string Imie, string Nazwisko, string DataUrodzenia, string Pesel, Plcie Plec, string NumerTelefonu)
        {
            imie = Imie;
            nazwisko = Nazwisko;
            DateTime.TryParseExact(DataUrodzenia, new[] { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MMM-yy" }, null, DateTimeStyles.None, out dataUrodzenia);
            PESEL = Pesel;
            plec = Plec;
            numerTelefonu = NumerTelefonu;
        }

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        public string PESEL1 { get => PESEL; set => PESEL = value; }
        internal Plcie Plec { get => plec; set => plec = value; }
        public string NumerTelefonu { get => numerTelefonu; set => numerTelefonu = value; }

        public int Wiek()
        {
            return DateTime.Now.Year - dataUrodzenia.Year;
        }

        public override string ToString()
        {
            string info = PESEL + " " + imie + " " + nazwisko + " (" + this.Wiek() + ")" + " " + numerTelefonu;
            return info;
        }

        public void Format()
        {
            imie = Char.ToUpper(imie[0]) + imie.Substring(1).ToLower();
            nazwisko = Char.ToUpper(nazwisko[0]) + nazwisko.Substring(1).ToLower();
        }

        public double AgeInHours(double HourOfBirth)
        {
            dataUrodzenia.AddHours(HourOfBirth);
            return (DateTime.Now - dataUrodzenia).TotalHours;
        }

        public bool CorrectPESEL(string Pesel)
        {
            if (Pesel.Length == 11)
            {
                if (Convert.ToString(dataUrodzenia.Year % 100) == Pesel[0].ToString() + Pesel[1])
                {
                    int month = dataUrodzenia.Month;
                    if (dataUrodzenia.Year < 2000)
                        month += 20;
                    if (month.ToString().PadLeft(2, '0') == Pesel[2].ToString() + Pesel[3])
                    {
                        int day = dataUrodzenia.Day;
                        if (day.ToString().PadLeft(2, '0') == Pesel[4].ToString() + Pesel[5])
                        {
                            if (plec == Plcie.K)
                            {
                                if (Convert.ToInt32(Pesel[9] % 2) == 0)
                                {

                                }
                                else
                                    return false;
                            }
                            else
                            {
                                if (Convert.ToInt32(Pesel[9] % 2) == 1)
                                {
                                    int first = (Pesel[0] - 48) * 1;
                                    int second = (Pesel[1] - 48) * 3;
                                    int third = (Pesel[2] - 48) * 7;
                                    int forth = (Pesel[3] - 48) * 9;
                                    int fifth = (Pesel[4] - 48) * 1;
                                    int sixth = (Pesel[5] - 48) * 3;
                                    int seventh = (Pesel[6] - 48) * 7;
                                    int eighth = (Pesel[7] - 48) * 9;
                                    int nineth = (Pesel[8] - 48) * 1;
                                    int tenth = (Pesel[9] - 48) * 3;
                                    if (first > 10)
                                        first %= 10;
                                    if (second > 10)
                                        second %= 10;
                                    if (third > 10)
                                        third %= 10;
                                    if (fifth > 10)
                                        fifth %= 10;
                                    if (sixth > 10)
                                        sixth %= 10;
                                    if ()
                                    int sum = first + second + third + forth + fifth + sixth + seventh + eighth + nineth + tenth;
                                }
                                else
                                    return false;
                            }
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }
    }

    int calc_sum()
    {

    }
}
