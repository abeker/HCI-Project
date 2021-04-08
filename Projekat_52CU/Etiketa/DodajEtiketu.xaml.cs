using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Projekat_52CU.Etiketa
{
    /// <summary>
    /// Interaction logic for DodajEtiketu.xaml
    /// </summary>
    public partial class DodajEtiketu : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<Etiketa> ListaEtiketa = new ObservableCollection<Etiketa>();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public DodajEtiketu()
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = false;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
        }

        object myContent;
        public object MyContent
        {
            get
            {
                return myContent;
            }
            set
            {
                if (value != myContent)
                {
                    myContent = value;
                    OnPropertyChanged("MyContent");
                }
            }
        }

        string _oznaka;             // za validiranje jed oznake
        public string Oznaka
        {
            get
            {
                return _oznaka;
            }
            set
            {
                if (value != _oznaka)
                {
                    _oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li zelite da zadrzite etiketu sa unetim parametrima?", "Dodavanje etikete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Etiketa etiketa = preuzmiParametre();
                        if (etiketa == null)
                            return;

                        ListaEtiketa.Add(etiketa);
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniEtiketu();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniEtiketu();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    {

                    }
                    break;
            }
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            Etiketa etiketa = preuzmiParametre();
            if (etiketa == null)                        // znaci da je bila greska
                return;

            if (MainWindow.demoYes == 1)
            {
                ListaEtiketa.Add(etiketa);
                control.Visibility = Visibility.Collapsed;
                MyContent = new IzmeniEtiketu();
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li zelite da dodate etiketu \""+ etiketa.Oznaka +"\"?", "Dodavanje etikete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        ListaEtiketa.Add(etiketa);
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniEtiketu();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniEtiketu();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    {

                    }
                    break;
            }
        }

        private Etiketa preuzmiParametre()
        {
            Etiketa etiketa = new Etiketa();

            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            etiketa.Oznaka = txt_oznaka.Text;
            foreach (Etiketa l in DodajEtiketu.ListaEtiketa)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    MessageBox.Show("Etiketa sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return null;
                }
            }
            if (txt_oznaka.Text.Length > 12)
            {
                MessageBox.Show("Oznaka etikete moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return null;
            }

            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku etikete!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                txt_oznaka.Focusable = true;
                Keyboard.Focus(txt_oznaka);
                return null;
            }
            else
            {
                txt_oznaka.Background = Brushes.Transparent;
                txt_oznaka.Foreground = Brushes.Black;
            }

            etiketa.Opis = txt_opis.Text;
            etiketa.Boja = cpicker.SelectedColor.Value;
            return etiketa;
        }
    }
}
