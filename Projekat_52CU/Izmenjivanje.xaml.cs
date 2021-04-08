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
    /// Interaction logic for Izmenjivanje.xaml
    /// </summary>
    public partial class IzmenaPrava : Window
    {
        private void Izmeni_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Meni_stavke.Dodaj.Lokali.Count == 0)
            {
                e.CanExecute = false;
            }
            else
                e.CanExecute = true;
        }

        private void Izmeni_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Meni_stavke.Izmeni();
            //s.Show();
        }

        private void ITip_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Tip.DodajTip.ListaTipova.Count == 0)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ITip_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Tip.IzmeniTip();
            //s.Show();
        }

        private void IEtiketu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Etiketa.DodajEtiketu.ListaEtiketa.Count == 0)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void IEtiketu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var s = new Etiketa.IzmeniEtiketu();
            //s.Show();
        }

        public IzmenaPrava()
        {
            InitializeComponent();
        }
    }
}
