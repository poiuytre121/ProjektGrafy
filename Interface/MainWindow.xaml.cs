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

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private void Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if(liczbaWierzcholkow.Text.Length!=0)
            {
                Dodawanie dodawanie = new Dodawanie(Convert.ToInt32(liczbaWierzcholkow.Text));
                dodawanie.Show();

            }
        }
    }
}
