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

namespace Projekat_52CU
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : Window, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string type;
        public Filter(string type)
        {
            InitializeComponent();
            
            this.type = type;
            
            txtAnswerOznaka.TextChanged += new TextChangedEventHandler(TextChanged);        // zakacim event handler
            txtAnswerNaziv.TextChanged += new TextChangedEventHandler(TextChanged);

            lblQuestionOznaka.Content = "Unesite oznaku " + type + " po kojoj se vrši filtriranje: ";
            lblQuestionNaziv.Content = "Unesite naziv " + type + " po kojem se vrši filtriranje: ";

            if (type.Equals("etikete"))     // ako je tab za etikete, ne mogu filtrirati po nazivu
            {
                txtAnswerNaziv.Visibility = Visibility.Collapsed;
                lblQuestionNaziv.Visibility = Visibility.Collapsed;
                img2.Visibility = Visibility.Collapsed;
            }

            Closing += this.OnWindowClosing;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            string message = "Da li želite da završite filtriranje?";
            string caption = "Završi filtriranje";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                vrati_na_staro();
            }
            else
            {
                e.Cancel = true;
            }
        }


        // lista koja ce mi cuvati pocetne vrednosti liste lokala (ono sta zelim da filtriram filtriram)
        private ObservableCollection<Meni_stavke.Lokal> prva_lista = new ObservableCollection<Meni_stavke.Lokal>();        
        private ObservableCollection<Etiketa.Etiketa> prva_listaEtiketa = new ObservableCollection<Etiketa.Etiketa>();
        private ObservableCollection<Tip.TipLokala> prva_listaTipova = new ObservableCollection<Tip.TipLokala>();

        private int id = 0;
        private bool change = false;        // da li sam imao uopste neko filtriranje        
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (type.Equals("lokala"))
            {

                if(id == 0)             // samo jednom da se inicijalizuje (jer se lista Lokali menja u ovoj f-ji)
                {
                    change = true;
                    foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali)
                    {
                        prva_lista.Add(l);              // prepisujem sadrzaj liste lokala koje sam prvobitno napravio
                    }
                    id++;
                }

                string ozn = txtAnswerOznaka.Text;
                string naz = txtAnswerNaziv.Text;

                Meni_stavke.Izmeni izm = new Meni_stavke.Izmeni();

                    if (ozn == null)
                    {
                        ozn = "";
                    }
                    if (naz == null)
                    {
                        naz = "";
                    }

                    izm.Lokali.Clear();
                    foreach (Meni_stavke.Lokal l in prva_lista)
                    {
                        if (l.Oznaka.Contains(ozn) && l.Ime.Contains(naz))
                        {
                            izm.Lokali.Add(l);
                        }
                    }
                
            }
            else if (type.Equals("tipa lokala"))
            {
                if (id == 0)            
                {
                    change = true;
                    foreach (Tip.TipLokala t in Tip.DodajTip.ListaTipova)
                    {
                        prva_listaTipova.Add(t);              // prepisujem sadrzaj liste tipova lokala koje sam prvobitno napravio
                    }
                    id++;
                }

                string ozn = txtAnswerOznaka.Text;
                string naz = txtAnswerNaziv.Text;

                Tip.IzmeniTip izm = new Tip.IzmeniTip();

                if (ozn == null)
                {
                    ozn = "";
                }
                if (naz == null)
                {
                    naz = "";
                }

                izm.Tipovi.Clear();
                foreach (Tip.TipLokala l in prva_listaTipova)
                {
                    if (l.Oznaka.Contains(ozn) && l.NazivTipa.Contains(naz))
                    {
                        izm.Tipovi.Add(l);
                    }
                }

            }
            else
            {
                if (id == 0)             
                {
                    change = true;
                    foreach (Etiketa.Etiketa et in Etiketa.DodajEtiketu.ListaEtiketa)
                    {
                        prva_listaEtiketa.Add(et);              // prepisujem sadrzaj liste etiketa koje sam prvobitno napravio
                    }
                    id++;
                }

                string ozn = txtAnswerOznaka.Text;
                string naz = txtAnswerNaziv.Text;

                Etiketa.IzmeniEtiketu izm = new Etiketa.IzmeniEtiketu();

                if (ozn == null)
                {
                    ozn = "";
                }

                izm.Etikete.Clear();
                foreach (Etiketa.Etiketa et in prva_listaEtiketa)
                {
                    if (et.Oznaka.Contains(ozn))
                    {
                        izm.Etikete.Add(et);
                    }
                }

            }

            return;
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            string message = "Da li želite da završite filtriranje?";
            string caption = "Završi filtriranje";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                vrati_na_staro();
                this.Hide();
            }
            else
            {
                
            }

            return;
        }

        public void vrati_na_staro()
        {
            if (type.Equals("lokala")) {
                Meni_stavke.Izmeni izm = new Meni_stavke.Izmeni();

                if (change == true)         // ako sam imao neko filtriranje
                {
                    izm.Lokali.Clear();                 // ocistim prvo 
                    Meni_stavke.Dodaj.Lokali.Clear();
                    foreach (Meni_stavke.Lokal l in prva_lista)
                    {
                        Meni_stavke.Dodaj.Lokali.Add(l);    // vratim u static listu onu "staru", jer izm.Lokali ukazuje na referencu
                                                            // => dovoljno je samo ovu jednu da vratim na staro
                    }
                }
            }
            else if (type.Equals("tipa lokala"))
            {
                Tip.IzmeniTip izm = new Tip.IzmeniTip();
                if(change == true)
                {
                    izm.Tipovi.Clear();
                    Tip.DodajTip.ListaTipova.Clear();
                    foreach (Tip.TipLokala t in prva_listaTipova)
                    {
                        Tip.DodajTip.ListaTipova.Add(t);
                    }
                }
            }
            else
            {
                Etiketa.IzmeniEtiketu izm = new Etiketa.IzmeniEtiketu();
                if (change == true)
                {
                    izm.Etikete.Clear();
                    Etiketa.DodajEtiketu.ListaEtiketa.Clear();
                    foreach (Etiketa.Etiketa et in prva_listaEtiketa)
                    {
                        Etiketa.DodajEtiketu.ListaEtiketa.Add(et);
                    }
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswerOznaka.SelectAll();
            txtAnswerOznaka.Focus();
        }
        
    }
}
