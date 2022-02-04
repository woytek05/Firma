using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml.Serialization;

namespace Firma
{
    [Serializable]
    public abstract class Osoba : IEquatable<Osoba>
    {
        private string imie;
        private string nazwisko;
        private DateTime dataUrodzenia;
        [XmlAttribute]
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

        public int Age()
        {
            return DateTime.Now.Year - dataUrodzenia.Year;
        }

        public override string ToString()
        {
            if (numerTelefonu != null)
                return PESEL + " " + imie + " " + nazwisko + " (" + Age() + ")" + " " + numerTelefonu;
            else
                return PESEL + " " + imie + " " + nazwisko + " (" + Age() + ")";
        }

        public void Format()
        {
            imie = char.ToUpper(imie[0]) + imie.Substring(1).ToLower();
            nazwisko = char.ToUpper(nazwisko[0]) + nazwisko.Substring(1).ToLower();
        }

        public double AgeInHours(double HourOfBirth)
        {
            dataUrodzenia.AddHours(HourOfBirth);
            TimeSpan span = DateTime.Now.Subtract(dataUrodzenia);
            return Math.Round(span.TotalHours, 2);
        }

        private double CalcTheCheckDigit()
        {
            double[] numbers = new double[10];
            double sum = 0, checkDigit;

            numbers[0] = char.GetNumericValue(PESEL[0]) * 1;
            numbers[1] = char.GetNumericValue(PESEL[1]) * 3;
            numbers[2] = char.GetNumericValue(PESEL[2]) * 7;
            numbers[3] = char.GetNumericValue(PESEL[3]) * 9;
            numbers[4] = char.GetNumericValue(PESEL[4]) * 1;
            numbers[5] = char.GetNumericValue(PESEL[5]) * 3;
            numbers[6] = char.GetNumericValue(PESEL[6]) * 7;
            numbers[7] = char.GetNumericValue(PESEL[7]) * 9;
            numbers[8] = char.GetNumericValue(PESEL[8]) * 1;
            numbers[9] = char.GetNumericValue(PESEL[9]) * 3;

            for (int i = 0; i < 10; i++)
            {
                if (numbers[i] > 10)
                    numbers[i] %= 10;
                sum += numbers[i];
            }

            if (sum > 10)
                sum %= 10;
            
            checkDigit = 10 - sum;
            return checkDigit;
        }

        private bool IsTheCheckDigitCorrect()
        {
            double checkDigit = CalcTheCheckDigit();
            if (checkDigit == char.GetNumericValue(PESEL[10]))
                return true;
            else
                return false;
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
                            if (plec == Plcie.K && char.GetNumericValue(PESEL[9]) % 2 == 0)
                            {
                                if (IsTheCheckDigitCorrect())
                                    return true;
                                else
                                    return false;
                            }
                            else if (plec == Plcie.M && char.GetNumericValue(PESEL[9]) % 2 == 1)
                            {
                                if (IsTheCheckDigitCorrect())
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

        public bool Equals(Osoba x)
        {
            if (PESEL.Equals(x.PESEL))
                return true;
            else
                return false;
        }
    }
}
