using System;
using System.Collections.Generic;
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
    /// Interaction logic for IzmenaPrava.xaml
    /// </summary>
    public partial class IzmenaPrava : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public IzmenaPrava(Etiketa selektovana)
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = false;

            prva_oznaka = selektovana.Oznaka;
            _oznaka = selektovana.Oznaka;
            _boja = selektovana.Boja;
            _opis = selektovana.Opis;
            System.Diagnostics.Debug.WriteLine(_oznaka);
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

        public string TxtBlockText
        {
            get
            {
                return "Izmena etikete \"" + prva_oznaka + "\"";
            }
            set
            { }
        }

        #region klasa Etiketa
        private string _oznaka;
        private string _opis;
        private Color _boja;
        private string prva_oznaka;

        
        public string Oznaka
        {
            get { return _oznaka; }
            set {
                if (value != _oznaka)
                {
                    _oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        public string Opis
        {
            get
            {
                return _opis;
            }
            set
            {
                if (value != _opis)
                {
                    _opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        public Color Boja
        {
            get
            {
                return _boja;
            }
            set
            {
                if (value != _boja)
                {
                    _boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }
        #endregion

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

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            string message = "Da li zelite da odustanete od izmene?";
            string caption = "Izmena";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                control.Visibility = Visibility.Collapsed;
                MyContent = new IzmeniEtiketu();
            }
            else
            {

            }
        }

        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (cekirajParametre() == false)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li zelite da sacuvate izmenu etikete \""+ prva_oznaka +"\"?", "Izmena", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        foreach (Etiketa et in DodajEtiketu.ListaEtiketa)
                        {
                            if (prva_oznaka.Equals(et.Oznaka))
                            {
                                et.Oznaka = txt_oznaka.Text;
                                et.Opis = txt_opis.Text;
                                et.Boja = cpicker.SelectedColor.Value;
                                break;
                            }
                        }

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

        private bool cekirajParametre()
        {
            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            foreach (Etiketa l in DodajEtiketu.ListaEtiketa)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    if (l.Oznaka.Equals(prva_oznaka))
                        continue;

                    MessageBox.Show("Etiketa sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return false;
                }
            }
            if (txt_oznaka.Text.Length > 12)
            {
                MessageBox.Show("Oznaka etikete moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return false;
            }
            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku etikete!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                txt_oznaka.Focusable = true;
                Keyboard.Focus(txt_oznaka);
                return false;
            }
            else
            {
                txt_oznaka.Background = Brushes.Transparent;
                txt_oznaka.Foreground = Brushes.Black;
            }

            return true;
        }
    }
}
