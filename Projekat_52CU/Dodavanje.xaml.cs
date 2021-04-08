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

namespace Projekat_52CU
{
    /// <summary>
    /// Interaction logic for Dodavanje.xaml
    /// </summary>
    public partial class Dodavanje : Window
    {
       
        private void Dodaj_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Projekat_52CU.Tip.DodajTip.ListaTipova.Count == 0 || Projekat_52CU.Etiketa.DodajEtiketu.ListaEtiketa.Count == 0)
            {
                e.CanExecute = false;
            }
            else
                e.CanExecute = true;
        }

        private void Dodaj_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Projekat_52CU.Meni_stavke.Dodaj();
            //s.Show();
        }

        private void DTip_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DTip_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Projekat_52CU.Tip.DodajTip();
            //s.Show();
        }

        private void DEtiketu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DEtiketu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Projekat_52CU.Etiketa.DodajEtiketu();
            //s.Show();
        }

         public Dodavanje()
        {
            InitializeComponent();
        }

    }
}
