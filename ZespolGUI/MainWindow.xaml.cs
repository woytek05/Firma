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
        Zespol zespol = new Zespol();
        public MainWindow()
        {
            InitializeComponent();
            zespol = (Zespol)Zespol.OdczytajXML("zespol.xml");
            lstCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.czlonkowie);
            txtNazwa.Text = zespol.Nazwa;
            txtKierownik.Text = zespol.Kierownik.ToString();
        }
    }
}
