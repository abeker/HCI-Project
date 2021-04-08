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
    /// Interaction logic for IzmeniEtiketu.xaml
    /// </summary>
    public partial class IzmeniEtiketu : UserControl, INotifyPropertyChanged
    {
        private void Click_Obrisi(object sender, RoutedEventArgs e)
        {
            Etiketa selektovani = (Etiketa)dgrMain.SelectedItem;

            if (selektovani == null)
            {
                MessageBox.Show("Morate da selektujete etiketu koji zelite da obrisete!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string message = "Da li zelite da obrisete etiketu \"" + selektovani.Oznaka + "\"?";
            string caption = "Potvrda brisanja";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                DodajEtiketu.ListaEtiketa.Remove(selektovani);
                //MessageBox.Show("Uspesno obrisana etiketa [ " + selektovani.Oznaka + " ]!", "Brisanje etikete");
            }
            else
            {

            }

            if(DodajEtiketu.ListaEtiketa.Count == 0)        // ako padne br etiketa na 0, disable dugme Obrisi
            {
                btnObrisi.IsEnabled = false;
                btnIzmeni.IsEnabled = false;
            }
        }

        private void Click_Dodaj(object sender, RoutedEventArgs e)
        {
            //var s = new DodajEtiketu();
            control.Visibility = Visibility.Collapsed;
            MyContent = new DodajEtiketu();
        }

        /*private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            string message = "Da li zelite da zavrsite sa izmenom?";
            string caption = "Izlaz";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
            {
                var s = new DodajEtiketu();
                s.Show();
            }
            else
            {

            }
        }*/

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

        private void Click_Izmeni(object sender, RoutedEventArgs e)
        {
            Etiketa selektovana = (Etiketa)dgrMain.SelectedItem;

            if (selektovana == null)
            {
                MessageBox.Show("Morate da selektujete etiketu koju zelite da izmenite!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            //MyContent = new IzmenaPrava(selektovana);
            control.Visibility = Visibility.Collapsed;
            MyContent = new IzmenaPrava(selektovana);
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

        private ObservableCollection<Etiketa> etiketa = DodajEtiketu.ListaEtiketa;
        public ObservableCollection<Etiketa> Etikete
        {
            get
            {
                return etiketa;
            }
            set
            {
                if (value != etiketa)
                {
                    etiketa = value;
                    OnPropertyChanged("Etikete");
                }
            }
        }

        bool enable = true;
        public bool Obrisi_enable
        {
            get {
                return enable;
            }
            set {
                if (DodajEtiketu.ListaEtiketa.Count > 0)
                {
                    enable = true;
                    OnPropertyChanged("Obrisi_enable");
                }
                else
                {
                    enable = true;
                    OnPropertyChanged("Obrisi_enable");
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

        public IzmeniEtiketu()
        {
            InitializeComponent();
            this.DataContext = this;
            MainWindow.izmTab = true;

            txtAnswerOznaka.TextChanged += new TextChangedEventHandler(TextChanged);        // zakacim event handler
            View = CollectionViewSource.GetDefaultView(Etikete);

            if (DodajEtiketu.ListaEtiketa.Count == 0)
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
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
        }

        private ObservableCollection<Etiketa> prva_listaEtiketa = new ObservableCollection<Etiketa>();
        private int id = 0;
        private bool change = false;        // da li sam imao uopste neko filtriranje  

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (id == 0)
            {
                change = true;
                foreach (Etiketa et in DodajEtiketu.ListaEtiketa)
                {
                    prva_listaEtiketa.Add(et);              // prepisujem sadrzaj liste etiketa koje sam prvobitno napravio
                }
                id++;
            }

            string ozn = txtAnswerOznaka.Text;

            IzmeniEtiketu izm = new IzmeniEtiketu();

            if (ozn == null)
            {
                ozn = "";
            }

            izm.Etikete.Clear();
            foreach (Etiketa et in prva_listaEtiketa)
            {
                if (et.Oznaka.Contains(ozn))
                {
                    izm.Etikete.Add(et);
                }
            }
        }

        private void Ponisti_btn(object sender, RoutedEventArgs e)
        {
            txtAnswerOznaka.Text = "";

            IzmeniEtiketu izm = new IzmeniEtiketu();
            if (change == true)
            {
                izm.Etikete.Clear();
                DodajEtiketu.ListaEtiketa.Clear();
                foreach (Etiketa et in prva_listaEtiketa)
                {
                    DodajEtiketu.ListaEtiketa.Add(et);
                }
            }
        }

    }
}
