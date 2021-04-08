using Microsoft.Win32;
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

namespace Projekat_52CU.Tip
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

        #region klasa TipLokala
        private string _oznaka;
        private string _opis;
        private string _nazivTipa;
        private string _slika;

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

        public string NazivTipa
        {
            get
            {
                return _nazivTipa;
            }
            set
            {
                if (value != _nazivTipa)
                {
                    _nazivTipa = value;
                    OnPropertyChanged("NazivTipa");
                }
            }
        }

        public string Slika
        {
            get
            {
                return _slika;
            }
            set
            {
                if (value != _slika)
                {
                    _slika = value;
                    OnPropertyChanged("Slika");
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

        public string TxtBlockText
        {
            get
            {
                return "Izmena tipa \"" + prva_oznaka + "\"";
            }
            set
            { }
        }

        private string prva_oznaka;
        public IzmenaPrava(TipLokala selektovani)
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = false;

            prva_oznaka = selektovani.Oznaka;
            _oznaka = selektovani.Oznaka;
            _nazivTipa = selektovani.NazivTipa;
            _opis = selektovani.Opis;
            _slika = selektovani.Slika;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Etiketa.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Etiketa.HelpProvider.ShowHelp(str);
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
                MyContent = new IzmeniTip();
            }
            else
            {

            }
        }

        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            
            foreach (TipLokala l in DodajTip.ListaTipova)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    if (l.Oznaka.Equals(prva_oznaka))
                        continue;

                    MessageBox.Show("Tip sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return;
                }
            }
            if (txt_oznaka.Text.Length > 12)
            {
                MessageBox.Show("Oznaka tipa moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return;
            }
            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku tipa!", "Upozorenje");
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                txt_oznaka.Focusable = true;
                Keyboard.Focus(txt_oznaka);
                return;
            }
            else
            {
                txt_oznaka.Background = Brushes.Transparent;
                txt_oznaka.Foreground = Brushes.Black;
            }

            if (txt_oznaka.Text.Length > 12)
            {
                MessageBox.Show("Oznaka tipa moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return;
            }

            if (txt_naziv.Text.Length > 12)
            {
                MessageBox.Show("Oznaka tipa moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return;
            }
            if (txt_naziv.Text.Equals(""))
            {
                MessageBox.Show("Unesite naziv tipa!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return;
            }
            else
            {
                txt_naziv.Background = Brushes.Transparent;
                txt_naziv.Foreground = Brushes.Black;
            }

            MessageBoxResult result = MessageBox.Show("Da li zelite da sacuvate izmenu tipa lokala \""+ prva_oznaka +"\"?", "Izmena", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        foreach (TipLokala t in DodajTip.ListaTipova)
                        {
                            if (t.Oznaka.Equals(prva_oznaka))
                            {
                                t.Oznaka = txt_oznaka.Text;
                                t.NazivTipa = txt_naziv.Text;
                                t.Opis = txt_opis.Text;
                                if (imgPhoto.Source != null)
                                    t.Slika = imgPhoto.Source.ToString();
                            }
                        }
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniTip();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniTip();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    {

                    }
                    break;
            }
            
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
            }

        }
    }
}
