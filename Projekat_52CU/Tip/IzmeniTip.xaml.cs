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
    /// Interaction logic for IzmeniTip.xaml
    /// </summary>
    public partial class IzmeniTip : UserControl, INotifyPropertyChanged
    {
        public IzmeniTip()
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = true;

            txtAnswerOznaka.TextChanged += new TextChangedEventHandler(TextChanged);        // zakacim event handler
            View = CollectionViewSource.GetDefaultView(Tipovi);

            if (DodajTip.ListaTipova.Count == 0)
            {
                btnObrisi.IsEnabled = false;
                btnIzmeni.IsEnabled = false;
            }
            else
            {
                btnObrisi.IsEnabled = true;
                btnIzmeni.IsEnabled = true;
            }
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

        private ObservableCollection<TipLokala> prva_listaTipova = new ObservableCollection<TipLokala>();
        private int id = 0;
        private bool change = false;        // da li sam imao uopste neko filtriranje     
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (id == 0)
            {
                change = true;
                foreach (TipLokala t in DodajTip.ListaTipova)
                {
                    prva_listaTipova.Add(t);              // prepisujem sadrzaj liste tipova lokala koje sam prvobitno napravio
                }
                id++;
            }

            string ozn = txtAnswerOznaka.Text;

            IzmeniTip izm = new IzmeniTip();

            if (ozn == null)
            {
                ozn = "";
            }

            izm.Tipovi.Clear();
            foreach (TipLokala l in prva_listaTipova)
            {
                if (l.Oznaka.Contains(ozn))
                {
                    izm.Tipovi.Add(l);
                }
            }
        }

        private void Ponisti_btn(object sender, RoutedEventArgs e)
        {
            txtAnswerOznaka.Text = "";

            IzmeniTip izm = new IzmeniTip();
            if (change == true)
            {
                izm.Tipovi.Clear();
                DodajTip.ListaTipova.Clear();
                foreach (TipLokala t in prva_listaTipova)
                {
                    DodajTip.ListaTipova.Add(t);
                }
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

        public int selected = 0;
        public int PromeniSelekcija
        {
            get
            {
                return selected;
            }
            set
            {
                if (value != selected)
                {
                    selected = value;
                    OnPropertyChanged("PromeniSelekcija");
                }
            }
        }

        public ObservableCollection<TipLokala> tip = DodajTip.ListaTipova;
        public ObservableCollection<TipLokala> Tipovi
        {
            get
            {
                return tip;
            }
            set
            {
                if (value != tip)
                {
                    tip = value;
                    OnPropertyChanged("Tipovi");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private ICollectionView _View;
        public ICollectionView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }

        private void Click_Obrisi(object sender, RoutedEventArgs e)
        {
            TipLokala selektovani = (TipLokala)dgrMain.SelectedItem;
            if (selektovani == null)
            {
                MessageBox.Show("Morate da selektujete tip koji zelite da obrisete!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            string mess = "Ukoliko obrisete tip lokala \"" + selektovani.NazivTipa + "\" obrisace se i lokali\nkoji pripadaju datom tipu.\n\nNastaviti?";
            string capt = "Potvrda brisanja";
            MessageBoxButton butt = MessageBoxButton.YesNo;
            MessageBoxImage ic = MessageBoxImage.Question;

            int ind_tip = Tip.DodajTip.ListaTipova.Count;
            if (MessageBox.Show(mess, capt, butt, ic) == MessageBoxResult.Yes)
            {
                foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali.ToList())          // prolazim kroz sve lokale
                {
                    ind_tip = l.Tip;            // zapamtim indeks tog tipa
                    if (DodajTip.ListaTipova[l.Tip].Oznaka.Equals(selektovani.Oznaka))      // ako je selektovani tip == nekom iz liste tipova
                    {
                        var images = MainWindow.can.Children.OfType<Image>().ToList();
                        foreach (var image in images)
                        {
                            if (image.ToolTip.ToString().Substring(11).Contains(l.Oznaka))
                            {
                                MainWindow.can.Children.Remove(image);
                            }
                        }
                        Meni_stavke.Dodaj.Lokali.Remove(l);
                    }
                }

                // dovodjenje u red indeksa ostalih lokala [ obrisem ind=1 => ind=2,3,4... su sada ind=1,2,3...]
                foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali.ToList())       
                {
                    if (l.Tip > ind_tip)
                    {
                        l.Tip --;
                    }
                }

                DodajTip.ListaTipova.Remove(selektovani);
            }
            else
            {

            }

            
            if (DodajTip.ListaTipova.Count == 0)
            {
                btnObrisi.IsEnabled = false;
                btnIzmeni.IsEnabled = false;
            }
        }

        /*private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            string message = "Da li zelite da zavrsite sa izmenom?";
            string caption = "Izlaz";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                control.Visibility = Visibility.Collapsed;
                MyContent = new IzmenaPrava(selektovana);
            }
            else
            {

            }
        }*/

        private void Click_Dodaj(object sender, RoutedEventArgs e)
        {
            control.Visibility = Visibility.Collapsed;
            MyContent = new DodajTip();
        }

        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            TipLokala selektovani = (TipLokala)dgrMain.SelectedItem;
            if (selektovani == null)
            {
                MessageBox.Show("Morate da selektujete tip koji zelite da izmenite!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            control.Visibility = Visibility.Collapsed;
            MyContent = new IzmenaPrava(selektovani);
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
