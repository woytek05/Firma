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
        private Zespol zespol = new();
        private bool isDataDirty = false;

        public bool IsDataDirty { get => isDataDirty; set => isDataDirty = value; }

        public MainWindow()
        {
            InitializeComponent();
            OdczytajXML("zespol.xml");
        }

        private void OdczytajXML(string filename)
        {
            zespol = (Zespol)Zespol.OdczytajXML(filename);
            lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            txtNazwa.Text = zespol.Nazwa;
            txtKierownik.Text = zespol.Kierownik.ToString();
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
            if (okno.btnAnulujClicked == false)
            {
                zespol.DodajCzlonka(cz);
                lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            }
        }

        private void btnUsun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = lstCzlonkowie.SelectedIndex;
            if (zaznaczony != -1)
            {
                zespol.czlonkowie.RemoveAt(zaznaczony);
                lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            }
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

        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                OdczytajXML(filename);
            }
        }

        private void MenuWyjdz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsDataDirty)
            {
                string msg = "Zapisać zmiany?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Zespół",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    Nullable<bool> wynik = dlg.ShowDialog();
                    if (wynik == true)
                    {
                        string filename = dlg.FileName;
                        zespol.nazwa = txtNazwa.Text;
                        Zespol.ZapiszXML(filename, zespol);
                    }
                }
            }
        }

        private void txtNazwa_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsDataDirty = true;
        }

        private void txtKierownik_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsDataDirty = true;
        }

        private void btnZmienCzlonka_Click(object sender, RoutedEventArgs e)
        {
            if(lstCzlonkowie.SelectedItem != null)
            {
                OsobaWindow okno = new OsobaWindow((CzlonekZespolu)lstCzlonkowie.SelectedItem);
                okno.ShowDialog();
                lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            }  
        }
    }
}
