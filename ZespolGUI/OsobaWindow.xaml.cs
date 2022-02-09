using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using Firma;

namespace ZespolGUI
{
    /// <summary>
    /// Interaction logic for OsobaWindow.xaml
    /// </summary>
    public partial class OsobaWindow : Window
    {
        Osoba _osoba;
        public bool btnAnulujClicked = false;

        public OsobaWindow()
        {
            InitializeComponent();
        }

        public OsobaWindow(Osoba osoba) : this()
        {
            _osoba = osoba;
            txtPesel.Text = osoba.Pesel;
            txtImie.Text = osoba.Imie;
            txtNazwisko.Text = osoba.Nazwisko;
            txtDataUrodzenia.Text = osoba.DataUrodzenia.ToString("dd-MMM-yyyy");
            cmbPlec.Text = (osoba.Plec == Plcie.K) ? "kobieta" : "mężczyzna";
            if (_osoba is KierownikZespolu)
            {
                var kierownik = _osoba as KierownikZespolu;
                if (kierownik != null)
                {
                    lblDodatkowy.Content = "Doświadczenie";
                    txtDodatkowy.Text = Convert.ToString(kierownik.Doswiadczenie);
                }
            }
            else if (_osoba is CzlonekZespolu)
            {
                var czlonek = _osoba as CzlonekZespolu;
                if (czlonek != null)
                {
                    lblDodatkowy.Content = "Funkcja";
                    txtDodatkowy.Text = czlonek.Funkcja;
                }
            }
        }

        private void btnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (txtPesel.Text != "" && txtImie.Text != "" && txtNazwisko.Text != "")
            {
                _osoba.Pesel = txtPesel.Text;
                _osoba.Imie = txtImie.Text;
                _osoba.Nazwisko = txtNazwisko.Text;
                string[] fdate = { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yyyy", "dd-MMM-yyyy" };
                if (DateTime.TryParseExact(txtDataUrodzenia.Text, fdate, null, DateTimeStyles.None, out DateTime date) == false)
                {
                    MessageBox.Show("Podana data jest nieprawidłowa!", "Nieprawidłowa data", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _osoba.DataUrodzenia = date;
                _osoba.Plec = (cmbPlec.Text == "kobieta") ? Plcie.K : Plcie.M;
                if (_osoba is KierownikZespolu)
                {
                    var kierownik = _osoba as KierownikZespolu;
                    if (kierownik != null)
                    {
                        kierownik.Doswiadczenie = Convert.ToInt32(txtDodatkowy.Text);
                    }
                }
                else if (_osoba is CzlonekZespolu)
                {
                    var czlonek = _osoba as CzlonekZespolu;
                    if (czlonek != null)
                    {
                        czlonek.Funkcja = txtDodatkowy.Text;
                    }
                }
            }
            DialogResult = true;
        }

        private void btnAnuluj_Click(object sender, RoutedEventArgs e)
        {
            btnAnulujClicked = true;
            DialogResult = false;
        }
    }
}
