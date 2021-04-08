using Microsoft.Win32;
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

namespace Projekat_52CU.Tip
{
    /// <summary>
    /// Interaction logic for DodajTip.xaml
    /// </summary>
    public partial class DodajTip : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<TipLokala> ListaTipova = new ObservableCollection<TipLokala>();
        public string imgPath = "";


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

        #region Properties Osnaka, Naziv
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
        string _naziv;             // za validiranje jed oznake
        public string Naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }
#endregion

        public DodajTip()
        {
            InitializeComponent();
            //imgPhoto.Source = new BitmapImage(new Uri("grb_jpg_GXD_icon.ico", UriKind.Relative));
            this.DataContext = this;
            MainWindow.izmTab = false;
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
            if(MainWindow.demoYes == 1)
            {
                control.Visibility = Visibility.Collapsed;
                MyContent = new IzmeniTip();
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li zelite da zadrzite tip sa unetim parametrima?", "Dodavanje tipa", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        TipLokala tip = preuzmiParametre();
                        if(tip == null)
                        {
                            return;
                        }

                        if(Meni_stavke.Izmeni.dLokal == true)
                        {
                            Meni_stavke.Izmeni.dLokal = false;
                            ListaTipova.Add(tip);
                            control.Visibility = Visibility.Collapsed;
                            MyContent = new Meni_stavke.Dodaj();
                            return;
                        }

                        ListaTipova.Add(tip);
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniTip();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        if (Meni_stavke.Izmeni.dLokal == true)
                        {
                            Meni_stavke.Izmeni.dLokal = false;
                            control.Visibility = Visibility.Collapsed;
                            MyContent = new Meni_stavke.Izmeni();
                            return;
                        }

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
                imgPath = op.FileName;                      // uzimam string putanje do slike
                //C:\Users\Aleksandar Beker\Documents\HCI(HumanComputerInteraction)\Vezbe\Projekat_52CU\Projekat_52CU\bin\Debug\bela_saOpisom_centar.png
            }

        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            TipLokala tip = preuzmiParametre();
            if(tip == null)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show("Da li zelite da dodate tip lokala \""+ tip.Oznaka +"\"?", "Dodavanje tipa", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        if (Meni_stavke.Izmeni.dLokal == true)
                        {
                            Meni_stavke.Izmeni.dLokal = false;
                            ListaTipova.Add(tip);
                            control.Visibility = Visibility.Collapsed;
                            MyContent = new Meni_stavke.Dodaj();
                            return;
                        }

                        ListaTipova.Add(tip);
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new IzmeniTip();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        if (Meni_stavke.Izmeni.dLokal == true)
                        {
                            Meni_stavke.Izmeni.dLokal = false;
                            control.Visibility = Visibility.Collapsed;
                            MyContent = new Meni_stavke.Izmeni();
                            return;
                        }

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

        private TipLokala preuzmiParametre()
        {
            TipLokala tip = new TipLokala();
            tip.Oznaka = txt_oznaka.Text;
            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            foreach (TipLokala l in DodajTip.ListaTipova)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    MessageBox.Show("Tip sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return null;
                }
            }
            if (txt_oznaka.Text.Length > 12)
            {
                MessageBox.Show("Oznaka tipa moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return null;
            }

            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku tipa!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            tip.NazivTipa = txt_naziv.Text;
            if (txt_naziv.Text.Length > 12)
            {
                MessageBox.Show("Oznaka tipa moze imati maksimalno 12 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return null;
            }

            if (txt_naziv.Text.Equals(""))
            {
                MessageBox.Show("Unesite naziv tipa!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return null;
            }
            else
            {
                txt_naziv.Background = Brushes.Transparent;
                txt_naziv.Foreground = Brushes.Black;

            }

            if (imgPath.Equals(""))
            {
                MessageBox.Show("Morate uneti ikonicu tipa!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            tip.Opis = txt_opis.Text;
            tip.Slika = imgPath;                // prosledjujem ga slici
            return tip;
        }
    }
}
