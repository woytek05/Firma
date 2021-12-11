using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Firma
{
    abstract class Osoba
    {
        private string imie;
        private string nazwisko;
        private DateTime dataUrodzenia;
        private string PESEL;
        private Plcie plec;
        private string numerTelefonu;

        public string Imie { get => imie; set => imie = value; }
        public string Nazwisko { get => nazwisko; set => nazwisko = value; }
        public DateTime DataUrodzenia { get => dataUrodzenia; set => dataUrodzenia = value; }
        public string Pesel { get => PESEL; set => PESEL = value; }
        internal Plcie Plec { get => plec; set => plec = value; }
        public string NumerTelefonu { get => numerTelefonu; set => numerTelefonu = value; }

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

        public int Wiek()
        {
            return DateTime.Now.Year - dataUrodzenia.Year;
        }

        public override string ToString()
        {
            if (numerTelefonu != null)
                return PESEL + " " + imie + " " + nazwisko + " (" + this.Wiek() + ")" + " " + numerTelefonu;
            else
                return PESEL + " " + imie + " " + nazwisko + " (" + this.Wiek() + ")";
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

        public double CalcCheckDigit()
        {
            double first = Char.GetNumericValue(PESEL[0]) * 1;
            double second = Char.GetNumericValue(PESEL[1]) * 3;
            double third = Char.GetNumericValue(PESEL[2]) * 7;
            double forth = Char.GetNumericValue(PESEL[3]) * 9;
            double fifth = Char.GetNumericValue(PESEL[4]) * 1;
            double sixth = Char.GetNumericValue(PESEL[5]) * 3;
            double seventh = Char.GetNumericValue(PESEL[6]) * 7;
            double eighth = Char.GetNumericValue(PESEL[7]) * 9;
            double nineth = Char.GetNumericValue(PESEL[8]) * 1;
            double tenth = Char.GetNumericValue(PESEL[9]) * 3;

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
            if (seventh > 10)
                seventh %= 10;
            if (eighth > 10)
                eighth %= 10;
            if (nineth > 10)
                nineth %= 10;
            if (tenth > 10)
                tenth %= 10;

            double sum = first + second + third + forth + fifth + 
                         sixth + seventh + eighth + nineth + tenth;
            if (sum > 10)
                sum %= 10;
            
            double check_digit = 10 - sum;
            return check_digit;
        }

        public bool CorrectPESEL()
        {
            if (PESEL.Length == 11)
            {
                if (Convert.ToString(dataUrodzenia.Year % 100).PadLeft(2, '0') == Convert.ToString(PESEL[0]) + PESEL[1])
                {
                    int month = dataUrodzenia.Month;
                    if (dataUrodzenia.Year >= 2000)
                        month += 20;
                    if (Convert.ToString(month).PadLeft(2, '0') == Convert.ToString(PESEL[2]) + PESEL[3])
                    {
                        if (Convert.ToString(dataUrodzenia.Day).PadLeft(2, '0') == Convert.ToString(PESEL[4]) + PESEL[5])
                        {
                            if (plec == Plcie.K)
                            {
                                if (Char.GetNumericValue(PESEL[9]) % 2 == 0)
                                {
                                    double check_digit = CalcCheckDigit();
                                    if (check_digit == Char.GetNumericValue(PESEL[10]))
                                        return true;
                                    else
                                        return false;
                                }
                                else
                                    return false;
                            }
                            else if (plec == Plcie.M)
                            {
                                if (Char.GetNumericValue(PESEL[9]) % 2 == 1)
                                {
                                    double check_digit = CalcCheckDigit();
                                    if (check_digit == Char.GetNumericValue(PESEL[10]))
                                        return true;
                                    else
                                        return false;
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
}
