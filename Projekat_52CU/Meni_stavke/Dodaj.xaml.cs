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
    /// Interaction logic for Dodaj.xaml
    /// </summary>
    public partial class Dodaj : UserControl, INotifyPropertyChanged
    {
        public static ObservableCollection<Lokal> Lokali = new ObservableCollection<Lokal>();
        private string photoPath = "";
        List<Etiketa_lok> objList;
        ObservableCollection<Etiketa.Etiketa> listaOznacenih;

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = Etiketa.HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Etiketa.HelpProvider.ShowHelp(str);
            }
        }

        #region Kolekcije [Tipovi, Etikete, Specif_alkohol, Specif_cena, Specif_kapacitet]
        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            String sel_tip = cb_tip.SelectedItem.ToString();
            int i = 0;              // index za selektovani tip
            for (; i < Projekat_52CU.Tip.DodajTip.ListaTipova.Count; i++)
            {
                if (sel_tip.Equals(Projekat_52CU.Tip.DodajTip.ListaTipova[i].Oznaka))
                {
                    break;
                }
            }

            if (Projekat_52CU.Tip.DodajTip.ListaTipova[i].Slika.Equals(""))    // ako nisam nista zadao, onda mi postavi belu pozadinu
            {
                imgPhoto.Source = new BitmapImage(new Uri("white1.jpg", UriKind.Relative));
            }
            else {
                imgPhoto.Source = new BitmapImage(new Uri(Projekat_52CU.Tip.DodajTip.ListaTipova[i].Slika));
            }

            photoPath = Projekat_52CU.Tip.DodajTip.ListaTipova[i].Slika;
        }

        public ObservableCollection<string> Tipovi
        {
            get
            {
                ObservableCollection<string> tip = new ObservableCollection<string>();
                foreach (Projekat_52CU.Tip.TipLokala t in Projekat_52CU.Tip.DodajTip.ListaTipova)
                {
                    tip.Add(t.Oznaka);
                }

                return tip;
            }
            set { }
        }

        /*public ObservableCollection<string> Etikete
        {
            get
            {
                ObservableCollection<string> etiketa = new ObservableCollection<string>();
                foreach (Projekat_52CU.Etiketa.Etiketa t in Projekat_52CU.Etiketa.DodajEtiketu.ListaEtiketa)
                {
                    etiketa.Add(t.Oznaka);
                }

                return etiketa;
            }
            set { }
        }*/

        public ObservableCollection<string> Specif_alkohol      // ovo mi je lista koju stavljam u combo box u DODAJ.XAML
        {
            get;
            set;
        }
        public ObservableCollection<string> Specif_kapacitet        
        {
            get;
            set;
        }
        public ObservableCollection<string> Specif_cena
        {
            get;
            set;
        }
        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Dodaj()
        {
            InitializeComponent();
            objList = new List<Etiketa_lok>();
            listaOznacenih = new ObservableCollection<Etiketa.Etiketa>();
            AddElementsInList();
            BindDropDown();             // ubacujem iz liste etiketa u listBox
            this.DataContext = this;

            MainWindow.izmTab = false;
            #region Inicijalizacija lista [Specif_alkohol, Specif_kapacitet, Specif_cena]
            Specif_alkohol = new ObservableCollection<string>();
                Specif_alkohol.Add("Ne sluzi se");
                Specif_alkohol.Add("Sluzi se do 23h");
                Specif_alkohol.Add("Sluzi se kasno u noc");

                Specif_cena = new ObservableCollection<string>();
                Specif_cena.Add("Niske");
                Specif_cena.Add("Srednje");
                Specif_cena.Add("Visoke");
                Specif_cena.Add("Izuzetno visoke");

                Specif_kapacitet = new ObservableCollection<string>();
                Specif_kapacitet.Add("Do 50 mesta");
                Specif_kapacitet.Add("Od 50 do 100 mesta");
                Specif_kapacitet.Add("Od 100 do 250 mesta");
                Specif_kapacitet.Add("Preko 250 mesta");
            #endregion
            
            dp.SelectedDate = DateTime.Today.AddDays(0);        // selektuje danasnji dan
            //int god = 2019, mon = 4, day = 4;
            //dp.BlackoutDates.Add(new CalendarDateRange(new DateTime(god,mon,day), new DateTime(2019, 4, 25)));
        }

        private void BindDropDown()
        {
            etikete.ItemsSource = objList;
        }

        private void etikete_TextChanged(object sender, TextChangedEventArgs e)
        {
            etikete.ItemsSource = objList.Where(x => x.Oznaka.ToLower().StartsWith(etikete.Text.Trim().ToLower()));
        }

        private void etikete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
            BindListBOX();
        }

        private void BindListBOX()
        {
            foreach (var etiketa in objList)
            {
                if (etiketa.Check_Status == true)
                {
                    foreach (Etiketa.Etiketa e in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        if (e.Oznaka.Equals(etiketa.Oznaka) && !listaOznacenih.Contains(e))  // ako se poklope oznake iz liste etiketa i ove moje lokalne liste
                        {
                            listaOznacenih.Add(e);
                            break;
                        }
                    }
                }
                else
                {                // ako je bilo true, pa skocilo na false. Treba da proverim da li ga imam u listi, ako imam, obrisi ga
                    foreach (Etiketa.Etiketa e in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        if (e.Oznaka.Equals(etiketa.Oznaka) && listaOznacenih.Contains(e))      // ako se poklopi ime i ako ga imam u listi (a ne bi trebao)
                        {
                            listaOznacenih.Remove(e);
                            break;
                        }
                    }
                }
            }
        }

        private void AddElementsInList()
        {
            // 1 element 
            int br = 10;
            foreach (Etiketa.Etiketa etiketa in Etiketa.DodajEtiketu.ListaEtiketa)
            {
                Etiketa_lok obj = new Etiketa_lok();
                obj.ID = br++;
                obj.Oznaka = etiketa.Oznaka;
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
                photoPath = op.FileName;
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
            MessageBoxResult result = MessageBox.Show("Da li zelite da zadrzite lokal sa unetim parametrima?", "Dodavanje lokala", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Lokal lokal = preuzmiParametre();
                        if (lokal == null)
                        {
                            return;
                        }

                        Lokali.Add(lokal);
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

        #region Properties Oznaka, Naziv

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

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            Lokal lokal = preuzmiParametre();
            if(lokal == null)
            {
                return;
            }

            MessageBoxResult result;
            if (MainWindow.demoYes == 1)
            {
                Lokali.Add(lokal);
                control.Visibility = Visibility.Collapsed;
                MyContent = new Izmeni();
                return;
            }
            else
            {
                result = MessageBox.Show("Da li zelite da dodate lokal \"" + lokal.Oznaka + "\"?", "Dodavanje lokala", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            }


                switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Lokali.Add(lokal);
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

        #region preuzmiParametre()
        private Lokal preuzmiParametre()
        {
            Lokal lokal = new Lokal();
            lokal.Oznaka = txt_oznaka.Text;
            
            SolidColorBrush fokus_boja = Brushes.Red;       // boja fokusiranog polja
            SolidColorBrush boja_slova = Brushes.White;     // boja slova u njemu
            foreach (Lokal l in Lokali)
            {
                if (l.Oznaka.Equals(txt_oznaka.Text))
                {
                    MessageBox.Show("Lokal sa unesenom oznakom vec postoji!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txt_oznaka.Focusable = true;
                    txt_oznaka.Background = fokus_boja;
                    txt_oznaka.Foreground = boja_slova;
                    Keyboard.Focus(txt_oznaka);
                    return null;
                }
            }
            if (txt_oznaka.Text.Length > 20)
            {
                MessageBox.Show("Oznaka lokala moze imati maksimalno 20 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_oznaka.Focusable = true;
                txt_oznaka.Background = fokus_boja;
                txt_oznaka.Foreground = boja_slova;
                Keyboard.Focus(txt_oznaka);
                return null;
            }

            if (txt_oznaka.Text.Equals(""))
            {
                MessageBox.Show("Unesite oznaku lokala!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            lokal.Ime = txt_naziv.Text;
            if (txt_naziv.Text.Length > 20)
            {
                MessageBox.Show("Naziv lokala moze imati maksimalno 20 karaktera!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_naziv.Focusable = true;
                txt_naziv.Background = fokus_boja;
                txt_naziv.Foreground = boja_slova;
                Keyboard.Focus(txt_naziv);
                return null;
            }
            if (txt_naziv.Text.Equals(""))
            {
                MessageBox.Show("Unesite naziv lokala!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            lokal.Opis = txt_opis.Text;
            lokal.Slika = photoPath;
            lokal.Etikete = listaOznacenih;         // ovde su mi sve oznacene etikete (to iskoristim za tabelu)

            if (listaOznacenih.Count == 0)
                System.Diagnostics.Debug.WriteLine("PRAZNO JE BRT");

            foreach (var et in listaOznacenih)
            {
                System.Diagnostics.Debug.WriteLine("oznaka: " + et.Oznaka);
            }

            #region ComboBoxes Alkohol, Cena, Kapacitet, Tip
            /*--- ALKOHOL ---*/
            String sel_alkohol = cb_alkohol.SelectedItem.ToString();        // u Stringu vrati sta sam selektovao
            int i = 0;
            for (; i < Specif_alkohol.Count; i++)                           // prolazim kroz listu             
            {
                if (sel_alkohol.Equals(Specif_alkohol[i]))          // i poredim da li mi je ovo selektovano to nesto iz liste
                {
                    break;                                      // ako jeste, uzmem njegov index i prosledim u lokal.Alkohol
                }
            }
            lokal.Alkohol = i;

            /*--- CENA ---*/
            String sel_cena = cb_cena.SelectedItem.ToString();
            int j = 0;
            for (; j < Specif_cena.Count; j++)
            {
                if (sel_cena.Equals(Specif_cena[j]))
                {
                    break;
                }
            }
            lokal.Cene = j;

            /*--- KAPACITET ---*/
            String sel_kap = cb_kap.SelectedItem.ToString();
            int k = 0;
            for (; k < Specif_kapacitet.Count; k++)
            {
                if (sel_kap.Equals(Specif_kapacitet[k]))
                {
                    break;
                }
            }
            lokal.Kapacitet = k;

            /*--- TIP ---*/
            String sel_tip = cb_tip.SelectedItem.ToString();
            i = 0;              // index za selektovani tip
            for (; i < Tipovi.Count; i++)
            {
                if (sel_tip.Equals(Tipovi[i]))
                {
                    break;
                }
            }
            lokal.Tip = i;

            #endregion

            DateTime? selectedDate = dp.SelectedDate;
            string dat = "";
            if (selectedDate.HasValue)
            {
                dat = selectedDate.Value.ToString();
            }
            lokal.Datum = dat;          // kao string vracam selektovani datum

            #region CheckBoxes Pusenje, Hendikepirani, Rezervacije
            if ((bool)(chb_pusenje.IsChecked))
            {                            // .isChecked vraca bool?, koji se konvertuje u bool, pa pitam da li je to selektovano na true
                lokal.Pusenje = true;
            }
            else
            {
                lokal.Pusenje = false;
            }

            if ((bool)(chb_hendikepirani.IsChecked))
            {
                lokal.Hendikepirani = true;
            }
            else
            {
                lokal.Hendikepirani = false;
            }

            if ((bool)(chb_rezervacije.IsChecked))
            {
                lokal.Rezervacije = true;
            }
            else
            {
                lokal.Rezervacije = false;
            }
            #endregion

            return lokal;
        }
        #endregion

    }


    public class Etiketa_lok
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
