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
using Grafy.Logika;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Dodawanie.xaml
    /// </summary>
    public partial class Dodawanie : Window
    {
        
        public Dodawanie()
        {
            InitializeComponent();
        }

        public Dodawanie(int liczba)
        {
            InitializeComponent();
            testowanyGraf = new Graf();
            liczbaWierzcholkow = liczba;
            for (int i = 0; i < liczba; i++)
            {
                Panel.Children.Add(new TextBlock() { Text = "Wierzcholek numer " + (i + 1).ToString()+":", Margin=new Thickness(5) });
                Panel.Children.Add(new TextBox() { Margin = new Thickness(5) });
            }
        }

        Graf testowanyGraf { get; set; }
        int liczbaWierzcholkow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < 2*liczbaWierzcholkow; i+=2)
            {
                TextBox tmp = (TextBox)Panel.Children[i];
                string[] tablica = tmp.Text.Split(',');
                List<int> lista = ListaIntow(tablica);
                Wierzcholek wierzcholek = new Wierzcholek(i / 2 + 1, lista);
                testowanyGraf.DodajWierzcholek(wierzcholek);
            }
            testowanyGraf.PowiazWierzcholkiGrafu();
            if (testowanyGraf.CzyPelnyDwudzielny())
            {
                StringBuilder grupa1 = new StringBuilder();
                StringBuilder grupa2 = new StringBuilder();
                foreach (var wierzcholek in testowanyGraf.Wierzcholki.Where(w => w.IdentyfikatorGrupy.Value))
                {
                    grupa1.Append(wierzcholek.Numer + " ");
                }
                foreach (var wierzcholek in testowanyGraf.Wierzcholki.Where(w => !w.IdentyfikatorGrupy.Value))
                {
                    grupa2.Append(wierzcholek.Numer + " ");
                }
                MessageBox.Show("Graf jest dwudzielny pełny" 
                    + Environment.NewLine + "Grupa 1: " 
                    + grupa1.ToString() 
                    + Environment.NewLine 
                    + "Grupa 2: " 
                    + grupa2.ToString()
                    ,"Wynik"
                    ,MessageBoxButton.OK,
                     MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Graf nie jest dwudzielny pełny", "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private List<int> ListaIntow(string[] tablica)
        {
            int[] myInts = Array.ConvertAll(tablica, int.Parse);
            return myInts.ToList<int>();
        }
    }
}
