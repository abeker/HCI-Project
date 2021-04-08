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
    /// Interaction logic for Pretrazi.xaml
    /// </summary>
    public partial class Pretrazi : Window, INotifyPropertyChanged
    {
        private string type;
        public Pretrazi(string type)
        {
            InitializeComponent();
            this.DataContext = this;

            //this.Owner = App.Current.MainWindow;        // da bude modalan (uvek je prikazan)
            this.type = type;
            lblQuestion.Content = "Unesite oznaku " + type + " koju tražite: ";
            lblQuestionNaziv.Content = "Unesite naziv " + type + " koji tražite: ";

            if (type.Equals("lokala"))
            {
                tipPanel.Visibility = Visibility.Collapsed;         // ukidam naziv
                etiketaCpicker.Visibility = Visibility.Collapsed;   // ukidam color picker
            }
            else if (type.Equals("tipa lokala"))
            {
                lokalPanel1.Visibility = Visibility.Collapsed;      // ukidam checkBoxove
                lokalPanel2.Visibility = Visibility.Collapsed;      // ukidam comboBoxove
                etiketaCpicker.Visibility = Visibility.Collapsed;
            }
            else
            {
                lokalPanel1.Visibility = Visibility.Collapsed;
                lokalPanel2.Visibility = Visibility.Collapsed;
                tipPanel.Visibility = Visibility.Collapsed;
            }

            Closing += this.OnWindowClosing;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            string message = "Da li želite da završite pretraživanje?";
            string caption = "Završi pretraživanje";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                
            }
            else
            {
                e.Cancel = true;
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

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            bool nasao = false;

            if (type.Equals("lokala"))
            {
                string oznaka = ( txtAnswer.Text != null ? txtAnswer.Text : "" );
                bool hend = (bool)hendikepirani.IsChecked;
                bool pus = (bool)pusenje.IsChecked;
                bool rez = (bool)rezervacije.IsChecked;
                string c = ( cene.SelectedItem != null ? cene.SelectedItem.ToString() : "" );
                string alk = ( alkohol.SelectedItem != null ? alkohol.SelectedItem.ToString() : "" );
                string kap = ( kapacitet.SelectedItem != null ? kapacitet.SelectedItem.ToString() : "" );

                Meni_stavke.Izmeni izm = new Meni_stavke.Izmeni();
                izm.Lokali.Clear();

                foreach (Meni_stavke.Lokal l in MainWindow.stari_lokali)
                {
                    bool check_boxes = (hend == l.Hendikepirani && pus == l.Pusenje && rez == l.Rezervacije);
                    bool combo_boxes = (Spec_cene[l.Cene].Contains(c) && Spec_alkohol[l.Alkohol].Contains(alk)
                        && Spec_kapacitet[l.Kapacitet].Contains(kap));

                    if (l.Oznaka.Contains(oznaka) && check_boxes && combo_boxes)
                    {
                        izm.Lokali.Add(l);
                        nasao = true;
                    }
                    
                }

                this.Hide();           // prvo skloni upitnik, pa ispisi/oznaci_pronadjenog
                if (!nasao)
                {
                    MessageBox.Show("Lokal sa unešenim parametrima nije pronađen", "Neuspešna pretraga", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            else if (type.Equals("tipa lokala"))
            {
                string oznaka = ( txtAnswer.Text != null ? txtAnswer.Text : "" );
                string naziv = ( txtAnswerNaziv.Text != null ? txtAnswerNaziv.Text : "" );

                Tip.IzmeniTip izm = new Tip.IzmeniTip();
                izm.Tipovi.Clear();

                foreach (Tip.TipLokala t in MainWindow.stari_tipovi)
                {
                    if (t.Oznaka.Contains(oznaka) && t.NazivTipa.Contains(naziv))
                    {
                        izm.Tipovi.Add(t);
                        nasao = true;
                    }
                    
                }

                this.Hide();
                if (!nasao)
                {
                    MessageBox.Show("Unešena oznaka tipa lokala ne postoji.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else 
            {
                string oznaka = ( txtAnswer.Text != null ? txtAnswer.Text : "" );
                bool upisana_boja = ( cpicker.SelectedColor != null ? true : false ); // moram znati da li zeli da trazi po boji ili ne

                Etiketa.IzmeniEtiketu izm = new Etiketa.IzmeniEtiketu();
                izm.Etikete.Clear();

                foreach (Etiketa.Etiketa et in MainWindow.stare_etikete)
                {
                    if (et.Oznaka.Contains(oznaka))
                    {
                        if (upisana_boja == true)
                        {
                            if (cpicker.SelectedColor.Value == et.Boja)
                            {
                                izm.Etikete.Add(et);
                                nasao = true;
                            }
                        }
                        else
                        {
                            izm.Etikete.Add(et);
                            nasao = true;
                        }
                    }
                    
                }

                this.Hide();
                if (!nasao)
                {
                    MessageBox.Show("Nije pronađena etiketa koja sadrži unete parametre.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            
            return;
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            return;
        }

        private void btnPonisti(object sender, RoutedEventArgs e)
        {
            if (type.Equals("lokala"))
            {
                Meni_stavke.Izmeni izm = new Meni_stavke.Izmeni();

                izm.Lokali.Clear();                 // ocistim prvo 
                Meni_stavke.Dodaj.Lokali.Clear();
                foreach (Meni_stavke.Lokal l in MainWindow.stari_lokali)
                {
                    Meni_stavke.Dodaj.Lokali.Add(l);    // vratim u static listu onu "staru", jer izm.Lokali ukazuje na referencu
                                                        // => dovoljno je samo ovu jednu da vratim na staro
                }
            }
            else if (type.Equals("tipa lokala"))
            {
                Tip.IzmeniTip izm = new Tip.IzmeniTip();
                
                izm.Tipovi.Clear();
                Tip.DodajTip.ListaTipova.Clear();
                foreach (Tip.TipLokala t in MainWindow.stari_tipovi)
                {
                    Tip.DodajTip.ListaTipova.Add(t);
                }
                
            }
            else
            {
                Etiketa.IzmeniEtiketu izm = new Etiketa.IzmeniEtiketu();
               
                izm.Etikete.Clear();
                Etiketa.DodajEtiketu.ListaEtiketa.Clear();
                foreach (Etiketa.Etiketa et in MainWindow.stare_etikete)
                {
                    Etiketa.DodajEtiketu.ListaEtiketa.Add(et);
                }
                
            }
            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }

        private ObservableCollection<string> spec_alkohol;     
        private ObservableCollection<string> spec_cene;    
        private ObservableCollection<string> spec_kapacitet;
        
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
    }
}
