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
    /// Interaction logic for IzmenaPrava.xaml
    /// </summary>
    public partial class IzmenaPrava : UserControl, INotifyPropertyChanged
    {
        #region NotifyProperties
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
        List<Etiketa_lokalna1> objList;      // lista privremenih etiketa za moj lokal

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Etiketa.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Etiketa.HelpProvider.ShowHelp(str);
            }
        }

        private string prva_oznaka;
        public IzmenaPrava(Lokal selektovani)
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = false;

            #region prebacujem iz selektovanog u moj lokalni Lokal
            prva_oznaka = selektovani.Oznaka;
            _oznaka = selektovani.Oznaka;
            _ime = selektovani.Ime;
            _opis = selektovani.Opis;
            _datum_otvaranja = selektovani.Datum;
            _dozvoljeno_pusenje = selektovani.Pusenje;
            _hendikepirani = selektovani.Hendikepirani;
            _rezervacije = selektovani.Rezervacije;
            _alkohol = selektovani.Alkohol;
            _cene = selektovani.Cene;
            _kapacitet = selektovani.Kapacitet;
            _ikonica = selektovani.Slika;
            
            _tip = selektovani.Tip;
            _tip_str = selektovani.Tip_string;
            _etikete = selektovani.Etikete;
            
            #endregion

            BajndujEtikete();
            this.selektovani = selektovani;
        }

        #region ObservableListe Alkohol, Cene, Kapacitet
        private ObservableCollection<string> spec_alkohol;     // iz ove liste ubacujem u combo box
        private ObservableCollection<string> spec_cene;    // pravim je ovde, jer se cela tabela
        private ObservableCollection<string> spec_kapacitet;   // bind-uje na Lokal
        private ObservableCollection<string> spec_tip;

        public ObservableCollection<string> Spec_kapacitet
        {
            get
            {
                spec_kapacitet = new ObservableCollection<string>();
                spec_kapacitet.Add("Do 50 mesta");
                spec_kapacitet.Add("Od 50 do 100 mesta");
                spec_kapacitet.Add("Od 100 do 250 mesta");
                spec_kapacitet.Add("Preko 250 mesta");
                return spec_kapacitet;
            }
            set { }
        }

        public ObservableCollection<string> Spec_cene
        {
            get
            {
                spec_cene = new ObservableCollection<string>();
                spec_cene.Add("Niske");
                spec_cene.Add("Srednje");
                spec_cene.Add("Visoke");
                spec_cene.Add("Izuzetno visoke");
                return spec_cene;
            }
            set { }
        }

        public ObservableCollection<string> Spec_alkohol
        {
            get
            {
                spec_alkohol = new ObservableCollection<string>();
                spec_alkohol.Add("Ne sluzi se");
                spec_alkohol.Add("Sluzi se do 23h");
                spec_alkohol.Add("Sluzi se kasno u noc");
                return spec_alkohol;
            }
            set { }
        }

        public ObservableCollection<string> Spec_tip
        {
            get
            {
                spec_tip = new ObservableCollection<string>();
                foreach (Projekat_52CU.Tip.TipLokala t in Projekat_52CU.Tip.DodajTip.ListaTipova)
                {
                    spec_tip.Add(t.Oznaka);
                }

                return spec_tip;
            }
            set { }
        }
        
        #endregion

        #region Properties (Oznaka, Ime, Opis...)
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

        public string Ime
        {
            get
            {
                return _ime;
            }
            set
            {
                if (value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged("Ime");
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

        public string Datum
        {
            get
            {
                return _datum_otvaranja;
            }
            set
            {
                if (value != _datum_otvaranja)
                {
                    _datum_otvaranja = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public bool Pusenje
        {
            get
            {
                return _dozvoljeno_pusenje;
            }
            set
            {
                if (_dozvoljeno_pusenje != value)
                {
                    _dozvoljeno_pusenje = value;
                    OnPropertyChanged("Pusenje");
                }
            }
        }

        public bool Rezervacije
        {
            get
            {
                return _rezervacije;
            }
            set
            {
                if (value != _rezervacije)
                {
                    _rezervacije = value;
                    OnPropertyChanged("Rezervacije");
                }
            }
        }

        public bool Hendikepirani
        {
            get
            {
                return _hendikepirani;
            }
            set
            {
                if (value != _hendikepirani)
                {
                    _hendikepirani = value;
                    OnPropertyChanged("Hendikepirani");
                }
            }
        }

        public int Alkohol
        {
            get
            {
                return _alkohol;
            }
            set
            {
                if (value != _alkohol)
                {
                    _alkohol = value;
                    OnPropertyChanged("Alkohol");
                }
            }
        }

        public int Cene
        {
            get
            {
                return _cene;
            }
            set
            {
                if (value != _cene)
                {
                    _cene = value;
                    OnPropertyChanged("Cene");
                }
            }
        }

        public int Kapacitet
        {
            get
            {
                return _kapacitet;
            }
            set
            {
                if (value != _kapacitet)
                {
                    _kapacitet = value;
                    OnPropertyChanged("Kapacitet");
                }
            }
        }

        public int Tip
        {
            get
            {
                return _tip;
            }
            set
            {
                if (value != _tip)
                {
                    _tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        public string Tip_string
        {
            get
            {
                _tip_str = Projekat_52CU.Tip.DodajTip.ListaTipova[_tip].NazivTipa;
                return _tip_str;
            }
            set
            {
            }
        }

        public string Slika
        {
            get
            {
                return _ikonica;
            }
            set
            {
                if (value != _ikonica)
                {
                    _ikonica = value;
                    OnPropertyChanged("Slika");
                }
            }
        }

        public ObservableCollection<Etiketa.Etiketa> Etikete      // sve selektovane etikete
        {
            get
            {
                return _etikete;
            }
            set
            {
                if (value != _etikete)
                {
                    _etikete = value;
                    OnPropertyChanged("Etikete");
                }
            }
        }
        #endregion

        private string _oznaka;
        private string _ime;
        private string _opis;
        private string _datum_otvaranja;
        private bool _dozvoljeno_pusenje;
        private bool _hendikepirani;
        private bool _rezervacije;
        private int _alkohol;
        private int _cene;
        private int _kapacitet;
        private int _tip;
        private string _tip_str;
        private string _ikonica;
        private ObservableCollection<Projekat_52CU.Etiketa.Etiketa> _etikete;       // skup svih oznacenih etiketa mog lokala

        private Lokal selektovani;      // daje mi selektovani lokal
        private void BajndujEtikete()
        {
            objList = new List<Etiketa_lokalna1>();      //inicijalizujem "privremenu" etiketa za moj lokal
            AddElementsInList();
            BindDropDown();             // ubacujem iz liste etiketa u listBox
        }

        private void BindDropDown()
        {
            etikete.ItemsSource = objList;      // dodavanje u privremenu listu etiketa
            System.Diagnostics.Debug.WriteLine("BROJ:::  " + objList.Count);
        }

        private void etikete_TextChanged(object sender, TextChangedEventArgs e)     // za trazenje unutar listBoxa
        {
            etikete.ItemsSource = objList.Where(x => x.Oznaka1.ToLower().StartsWith(etikete.Text.Trim().ToLower()));
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
                        if (e.Oznaka.Equals(etiketa.Oznaka1) && !_etikete.Contains(e))  // ako se poklope oznake iz liste etiketa i ove moje lokalne liste
                        {
                            _etikete.Add(e);         // dodajem novog clana u listu cekiranih etiketa za taj konkretan Lokal
                            break;
                        }
                    }
                }
                else
                {                // ako je bilo true, pa skocilo na false. Treba da proverim da li ga imam u listi, ako imam, obrisi ga
                    foreach (Etiketa.Etiketa e in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        if (e.Oznaka.Equals(etiketa.Oznaka1) && _etikete.Contains(e))      // ako se poklopi ime i ako ga imam u listi (a ne bi trebao)
                        {
                            _etikete.Remove(e);      // brisem iz liste cekiranih etiketa tog lokala
                            break;
                        }
                    }
                }
            }
        }
        
        private void AddElementsInList()
        {
            int br = 100;
            foreach (Etiketa.Etiketa etiketa in Etiketa.DodajEtiketu.ListaEtiketa)
            {
                Etiketa_lokalna1 obj = new Etiketa_lokalna1();
                obj.ID = br++;
                obj.Oznaka1 = etiketa.Oznaka;

                
                if (_etikete != null) {
                    foreach (Etiketa.Etiketa et in _etikete)       // iteriram kroz njegovu listu cekiranih etiketa
                    {
                        if (etiketa.Oznaka.Equals(et.Oznaka))
                            obj.Check_Status = true;            // i oznacavam etikete unutar moje lokalne liste koju prikazujem
                    }
                }
                
                objList.Add(obj);
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
        
        public string TxtBlockText
        {
            get
            {
                return "Izmena lokala \""+ selektovani.Oznaka + "\"";
            }
            set
            { }
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
                MyContent = new Izmeni();
            }
            else
            {

            }
        }

        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            foreach (Lokal l in Dodaj.Lokali)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    if (l.Oznaka.Equals(prva_oznaka))
                        continue;

                    MessageBox.Show("Lokal sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return;
                }
            }
            if (txt_oznaka.Text.Length > 20)
            {
                MessageBox.Show("Oznaka lokala moze imati maksimalno 20 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return;
            }

            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku lokala!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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


            // validacija naziva
            if (txt_naziv.Text.Length > 20)
            {
                MessageBox.Show("Naziv lokala moze imati maksimalno 20 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return;
            }

            if (txt_naziv.Text.Equals(""))
            {
                MessageBox.Show("Unesite naziv lokala!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            
            MessageBoxResult result = MessageBox.Show("Da li zelite da sacuvate izmenu lokala \"" + prva_oznaka + "\"?", "Izmena", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        foreach (Lokal l in Dodaj.Lokali)
                        {
                            if (l.Oznaka.Equals(prva_oznaka))
                            {
                                l.Oznaka = txt_oznaka.Text;
                                l.Ime = txt_naziv.Text;
                                l.Opis = txt_opis.Text;
                                #region Datum, Pusenje, Rezervacije, Hendikepirani
                                DateTime? selectedDate = dp.SelectedDate;
                                string dat = "";
                                if (selectedDate.HasValue)
                                {
                                    dat = selectedDate.Value.ToString();
                                }
                                l.Datum = dat;
                                if ((bool)(chb_pusenje.IsChecked))
                                {                            // .isChecked vraca bool?, koji se konvertuje u bool, pa pitam da li je to selektovano na true
                                    l.Pusenje = true;
                                }
                                else
                                {
                                    l.Pusenje = false;
                                }
                                if ((bool)(chb_hendikepirani.IsChecked))
                                {
                                    l.Hendikepirani = true;
                                }
                                else
                                {
                                    l.Hendikepirani = false;
                                }

                                if ((bool)(chb_rezervacije.IsChecked))
                                {
                                    l.Rezervacije = true;
                                }
                                else
                                {
                                    l.Rezervacije = false;
                                }
                                #endregion
                                #region Alkohol, Cena, Kapacitet, Tip
                                /*--- ALKOHOL ---*/
                                String sel_alkohol = cb_alkohol.SelectedItem.ToString();        // u Stringu vrati sta sam selektovao
                                int i = 0;
                                for (; i < Spec_alkohol.Count; i++)                           // prolazim kroz listu             
                                {
                                    if (sel_alkohol.Equals(Spec_alkohol[i]))          // i poredim da li mi je ovo selektovano to nesto iz liste
                                    {
                                        break;                                      // ako jeste, uzmem njegov index i prosledim u lokal.Alkohol
                                    }
                                }
                                l.Alkohol = i;

                                /*--- CENA ---*/
                                String sel_cena = cb_cena.SelectedItem.ToString();
                                int j = 0;
                                for (; j < Spec_cene.Count; j++)
                                {
                                    if (sel_cena.Equals(Spec_cene[j]))
                                    {
                                        break;
                                    }
                                }
                                l.Cene = j;

                                /*--- KAPACITET ---*/
                                String sel_kap = cb_kap.SelectedItem.ToString();
                                int k = 0;
                                for (; k < Spec_kapacitet.Count; k++)
                                {
                                    if (sel_kap.Equals(Spec_kapacitet[k]))
                                    {
                                        break;
                                    }
                                }
                                l.Kapacitet = k;

                                /*--- TIP ---*/
                                String sel_tip = cb_tip.SelectedItem.ToString();
                                i = 0;              // index za selektovani tip
                                for (; i < Spec_tip.Count; i++)
                                {
                                    if (sel_tip.Equals(Spec_tip[i]))
                                    {
                                        break;
                                    }
                                }
                                l.Tip = i;
                                #endregion
                                l.Etikete = _etikete;           // izmeni ga ali ne prikaze odmah
                                
                                if (imgPhoto.Source != null)
                                    l.Slika = imgPhoto.Source.ToString();
                            }
                        }
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new Izmeni();
                    }
                    break;
                case MessageBoxResult.No:
                    {
                        control.Visibility = Visibility.Collapsed;
                        MyContent = new Izmeni();
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

    public class Etiketa_lokalna1
    {
        public int ID
        {
            get;
            set;
        }
        public string Oznaka1
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
