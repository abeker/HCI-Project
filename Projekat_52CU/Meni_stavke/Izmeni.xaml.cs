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

namespace Projekat_52CU.Meni_stavke
{
    /// <summary>
    /// Interaction logic for Izmeni.xaml
    /// </summary>
    public partial class Izmeni : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        List<Etiketa_lokalna> objList;

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Etiketa.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Etiketa.HelpProvider.ShowHelp(str);
            }
        }

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

        public ObservableCollection<Lokal> lok = Dodaj.Lokali;
        public ObservableCollection<Lokal> Lokali
        {
            get
            {
                return lok;
            }
            set
            {
                if (value != lok)
                {
                    lok = value;
                    OnPropertyChanged("Lokali");
                }
            }
        }

        private Lokal selektovani;            
        public Izmeni()
        {
            InitializeComponent();
            BajndujEtikete();
            

            this.DataContext = this;
            MainWindow.izmTab = true;

            txtAnswerOznaka.TextChanged += new TextChangedEventHandler(TextChanged);        // zakacim event handler
            txtAnswerNaziv.TextChanged += new TextChangedEventHandler(TextChanged);

            View = CollectionViewSource.GetDefaultView(Lokali);

            if (Dodaj.Lokali.Count == 0)
            {
                btnIzmeni.IsEnabled = false;
                btnObrisi.IsEnabled = false;
            }
            else
            {
                btnIzmeni.IsEnabled = true;
                btnObrisi.IsEnabled = true;
            }
        }

        private ObservableCollection<Lokal> prva_lista = new ObservableCollection<Lokal>();
        private int id = 0;
        private bool change = false;        // da li sam imao uopste neko filtriranje   
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (id == 0)             // samo jednom da se inicijalizuje (jer se lista Lokali menja u ovoj f-ji)
            {
                change = true;
                foreach (Lokal l in Dodaj.Lokali)
                {
                    prva_lista.Add(l);              // prepisujem sadrzaj liste lokala koje sam prvobitno napravio
                }
                id++;
            }

            string ozn = txtAnswerOznaka.Text;
            string naz = txtAnswerNaziv.Text;

            if (ozn == null)
            {
                ozn = "";
            }
            if (naz == null)
            {
                naz = "";
            }

            Izmeni izm = new Izmeni();

            izm.Lokali.Clear();
            foreach (Meni_stavke.Lokal l in prva_lista)
            {
                if (l.Oznaka.Contains(ozn) && l.Ime.Contains(naz))
                {
                    izm.Lokali.Add(l);
                }
            }
        }

        private void Ponisti_btn(object sender, RoutedEventArgs e)
        {
            txtAnswerOznaka.Text = "";
            txtAnswerNaziv.Text = "";

            Izmeni izm = new Izmeni();

            if (change == true)         // ako sam imao neko filtriranje
            {
                izm.Lokali.Clear();                 // ocistim prvo 
                Dodaj.Lokali.Clear();
                foreach (Lokal l in prva_lista)
                {
                    Dodaj.Lokali.Add(l);                // vratim u static listu onu "staru", jer izm.Lokali ukazuje na referencu
                                                        // => dovoljno je samo ovu jednu da vratim na staro
                }
            }

        }

            // jako bitno!!
            private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)      // event reaguje na promenu selekcije u datagrid-u
        {
            if (e.OriginalSource != null)
            {
                Type type = e.OriginalSource.GetType();
                if (type == typeof(DataGrid))           // preventiva       
                {
                    selektovani = (Lokal)dgrMain.SelectedItem;      // uzmem selektovani Lokal
                    //MessageBox.Show(selektovani.Oznaka);        
                    BajndujEtikete();               // ponovo inicijalizujem elemente unutar listBoxa i cekiram potrebne
                }
                else
                {
                    MessageBox.Show("Doslo je do greske prilikom ucitavanja Izmene", "Greska");
                }
            }
        }

        private void BajndujEtikete()
        {
            objList = new List<Etiketa_lokalna>();      //inicijalizujem "privremenu" listu svih etiketa koje su kreirane
            AddElementsInList();
            BindDropDown();             // ubacujem iz liste etiketa u listBox
        }

        private void BindDropDown()
        {
            etikete.ItemsSource = objList;      // dodavanje u privremenu listu etiketa 
        }

        private void etikete_TextChanged(object sender, TextChangedEventArgs e)     // za trazenje unutar listBoxa
        {
            etikete.ItemsSource = objList.Where(x => x.Oznaka.ToLower().StartsWith(etikete.Text.Trim().ToLower()));
        }

        private void etikete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)     // za promenu cekiranih u list boxu
        {
            BindListBOX();
        }

        private void BindListBOX()
        {
            foreach (var etiketa in objList)
            {
                if (etiketa.Check_Status == true)       // ako je etiketa cekirana
                {
                    foreach (Etiketa.Etiketa e in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        if (e.Oznaka.Equals(etiketa.Oznaka) && !selektovani.Etikete.Contains(e))  // ako se poklope oznake iz liste etiketa i ove moje lokalne liste
                        {
                            selektovani.Etikete.Add(e);         // dodajem novog clana u listu cekiranih etiketa za taj konkretan Lokal
                            break;
                        }
                    }
                }
                else
                {                // ako je bilo true, pa skocilo na false. Treba da proverim da li ga imam u listi, ako imam, obrisi ga
                    foreach (Etiketa.Etiketa e in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        if (e.Oznaka.Equals(etiketa.Oznaka) && selektovani.Etikete.Contains(e))      // ako se poklopi ime i ako ga imam u listi (a ne bi trebao)
                        {
                            selektovani.Etikete.Remove(e);      // brisem iz liste cekiranih etiketa tog lokala
                            break;
                        }
                    }
                }
            }
        }

        private void AddElementsInList()
        {
            int br = 10;
            foreach (Etiketa.Etiketa etiketa in Etiketa.DodajEtiketu.ListaEtiketa)
            {
                Etiketa_lokalna obj = new Etiketa_lokalna();
                obj.ID = br++;
                obj.Oznaka = etiketa.Oznaka;

                if (selektovani != null)
                {
                    foreach (Etiketa.Etiketa et in selektovani.Etikete)       // iteriram kroz njegovu listu cekiranih etiketa
                    {
                        if (etiketa.Oznaka.Equals(et.Oznaka))
                            obj.Check_Status = true;            // i oznacavam etikete unutar moje lokalne liste koju prikazujem
                    }
                }
                objList.Add(obj);
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

        public static bool izmena = false;
        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            if (selektovani == null)
            {
                MessageBox.Show("Morate da selektujete lokal koji zelite da izmenite!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            control.Visibility = Visibility.Collapsed;
            MyContent = new IzmenaPrava(selektovani);
        }

        public static bool dLokal = false;          // da li treba da vratim na prozor Lokal[true] ili IzmeniTip[false] u DodajTip
        private void Click_Dodaj(object sender, RoutedEventArgs e)
        {
            if(Tip.DodajTip.ListaTipova.Count == 0)
            {
                MessageBox.Show("Mora da postoji dodat barem jedan tip lokala.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                dLokal = true;
                var t = new Tip.DodajTip();
                //t.Show();
                //this.Close();
                return;
            }

            control.Visibility = Visibility.Collapsed;
            MyContent = new Dodaj();
        }

        /*private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            string message = "Da li zelite da odustanete od izmene?";
            string caption = "Izmena-odustani";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                //this.Close();
            }
            else
            {

            }
        }*/

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

        private void Click_Obrisi(object sender, RoutedEventArgs e)
        {
            if (selektovani == null)
            {
                MessageBox.Show("Morate da selektujete lokal koji zelite da obrisete!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string oznaka = selektovani.Oznaka;

            string message = "Da li zelite da obrisete tip lokala \"" + selektovani.Oznaka + "\"?";
            string caption = "Potvrda brisanja";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                var images = MainWindow.can.Children.OfType<Image>().ToList();
                foreach (var image in images)
                {
                    if (image.ToolTip.ToString().Substring(11).Contains(selektovani.Oznaka))
                    {
                        MainWindow.can.Children.Remove(image);
                    }
                }
                Dodaj.Lokali.Remove(selektovani);
                //MessageBox.Show("Uspesno obrisan lokal  [ " + oznaka + " ]!", "Brisanje lokala");
            }
            else
            {

            }

            if (Dodaj.Lokali.Count == 0)
            {
                btnIzmeni.IsEnabled = false;
                btnObrisi.IsEnabled = false;
            }
            else
            {
                btnIzmeni.IsEnabled = true;
                btnObrisi.IsEnabled = true;
            }
        }
    }


    public class Etiketa_lokalna
    {
        public int ID
        {
            get;
            set;
        }
        public string Oznaka
        {
            get;
            set;
        }
        public Boolean Check_Status
        {
            get;
            set;
        }
    }
}
