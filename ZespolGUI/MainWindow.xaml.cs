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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Firma;

namespace ZespolGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Zespol zespol = new();

        public MainWindow()
        {
            InitializeComponent();
            zespol = (Zespol)Zespol.OdczytajXML("zespol.xml");
            lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            txtNazwa.Text = zespol.Nazwa;
            txtKierownik.Text = zespol.Kierownik.ToString();
        }

        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                zespol.nazwa = txtNazwa.Text;
                Zespol.ZapiszXML(filename, zespol);
            }
        }

        private void btnZmien_Click(object sender, RoutedEventArgs e)
        {
            OsobaWindow okno = new OsobaWindow(zespol.kierownik);
            okno.ShowDialog();
            txtKierownik.Text = zespol.kierownik.ToString();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            CzlonekZespolu cz = new CzlonekZespolu();
            OsobaWindow okno = new OsobaWindow(cz);
            okno.ShowDialog();
            zespol.DodajCzlonka(cz);
            //lista.Add(cz);
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = lstCzlonkowie.SelectedIndex;
            //lista.RemoveAt(zaznaczony);
            zespol.czlonkowie.RemoveAt(zaznaczony);
        }
    }
}
