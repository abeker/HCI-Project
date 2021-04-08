using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xaml;
using System.Xml.Serialization;

namespace Projekat_52CU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Point startPoint = new Point();
        private int selectedTab = 0;
        
        public MainWindow()
        {
            ucitajLok();            // inicijalno ucitavanje svih lokala
            InitializeComponent();
            this.DataContext = this;
            
            ucitajPozicije();
            //Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);       // izlazak iz main window-a
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

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MainWindow.isDemoMode)
            {
             /*   Meni_stavke.Dodaj.Lokali.Clear();
                foreach (Meni_stavke.Lokal l in MainWindow.vrati_stare)
                {
                    Meni_stavke.Dodaj.Lokali.Add(l);
                }*/
                MainWindow.timer.Stop();
                MainWindow.isDemoMode = false;
            }
            else {
                string message = "Da li želite da zatvorite aplikaciju?";
                string caption = "Izlazak iz aplikacije";
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
        }

        ObservableCollection<Meni_stavke.Lokal> dod = new ObservableCollection<Meni_stavke.Lokal>();
        public ObservableCollection<Meni_stavke.Lokal> Dodati_lokali
        {
            get
            {
                dod = Meni_stavke.Dodaj.Lokali;
                return dod;
            }
            set
            {
                if (value != dod)
                {
                    dod = value;
                    OnPropertyChanged("Dodati_lokali");
                }
            }
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);       // uzmimam startnu poziciju odakle pocinjem da vucem
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))  // neka minimalna distanca[tipa 10px], da bih ga smatrao drag-om
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                if (listViewItem == null)
                {
                    return;
                }
                Meni_stavke.Lokal lokal = (Meni_stavke.Lokal)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);        // ili prostije Student student = listView.getSelectedItem();

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", lokal);      // "myFormat" je format u kojem pakujem Studenta u toj mojoj listi (npr. Ime, Prezime, Mail adresa)
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        Image prosledjenaSlika = new Image();           // ovo koristim prilikom drop-a, da bih znao koju sliku drop-ujem
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))  // neka minimalna distanca[tipa 10px], da bih ga smatrao drag-om
            {
                // uzmem sliku koja je drag-ovana
                Image slika = sender as Image;
                prosledjenaSlika = slika;
                
                Image slikaItem =
                    FindAncestor<Image>((DependencyObject)e.OriginalSource);

                Meni_stavke.Lokal lokal = null;
                // Initialize the drag & drop operation

                // Oznaka:    456     substring(11)
                foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali)
                {
                    if ((slika.ToolTip as string).Substring(11).StartsWith(l.Oznaka))
                    {
                        lokal = l;
                        break;
                    }
                }
                DataObject dragData = new DataObject("myFormat", lokal);      // "myFormat" je format u kojem pakujem Lokal u toj mojoj listi (npr. Ime, Prezime, Mail adresa)
                DragDrop.DoDragDrop(slikaItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)           // ako sam dosao do obj. tipa <T>, vrati ga gore
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current); // ako nisam, uzmi njegovog roditelja(npr. WrapPanel) i proveri da li je on ListViewItem
            }
            while (current != null);
            return null;
        }


        // dropujem na canvas
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Meni_stavke.Lokal lokal = e.Data.GetData("myFormat") as Meni_stavke.Lokal;      // preuzmem lokal
                var images = canvas.Children.OfType<Image>().ToList();          // sve slike na canvasu

                int dim = 20;       // dimenzija slike (npr. 20x20)
                Point trenutna = e.GetPosition(canvas);
                foreach (Image img in images)
                {
                    Point p = new Point();
                    string ime = img.Name.Substring(1);       // izbaci "a" iz imena slike (ime sadrzi koordinate [x, y])
                    string[] ceo = ime.Split('_');
                    p.X = double.Parse(ceo[0]);
                    p.Y = double.Parse(ceo[1]);
                    trenutna.Y = (int)trenutna.Y;           // convert jer u name-u mi je y-coord kao int (name ne sme da sadrzi
                                                            // nista osim slova, brojeva i underscore-a)

                    // provera da li postoji slika na toj poziciji (proveravam gornji levi ugao, donji desni, donji levi)
                    if ((((trenutna.X > p.X) && (trenutna.X < (p.X + dim))) && ((trenutna.Y > p.Y) && (trenutna.Y < p.Y + dim)))
                        || (((trenutna.X + dim > p.X) && (trenutna.X + dim < (p.X + dim))) && (((trenutna.Y + dim > p.Y) && (trenutna.Y + dim < p.Y + dim)) 
                        || ((trenutna.Y > p.Y) && (trenutna.Y < p.Y + dim)) ))
                        || ( ((trenutna.X > p.X) && (trenutna.X < p.X + dim)) && ( (trenutna.Y + dim > p.Y ) && (trenutna.Y + dim < p.Y + dim))))
                    {
                        MessageBox.Show("Na datoj poziciji vec postoji lokal.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // brisem sa stare pozicije ako ima neka slika sa istim ToolTip-om (oznaka lokala)

                if (startPoint.X > 200) {               // ako vrsim drag-ovanje na canvasu (200px je sirok panel gde je listView)
                    foreach (var image in images)
                    {
                        if(prosledjenaSlika == image)
                        {
                            canvas.Children.Remove(image);
                            break;
                        }
                    }
                }
                
                int coordY = (int)e.GetPosition(canvas).Y;      // jer mi je Y koordinata u double, a Name ne sme da sadrzi nista osim 
                                                                // slova, brojeva i underscore-a
                string name = "a" + e.GetPosition(canvas).X.ToString() + "_" + coordY.ToString();       // "a" jer mora poceti slovom ili "_" underscore-om
                Image BodyImage = new Image
                {
                    Width = dim,
                    Height = dim,

                    ToolTip = "Oznaka:    " + lokal.Oznaka 
                              + "\nNaziv lokala:    " + lokal.Ime 
                              + "\nTip lokala:    " + Tip.DodajTip.ListaTipova[lokal.Tip].NazivTipa
                              + "\nDatum otvaranja:    " + lokal.Datum
                              + "\nSluženje alkohola:    " + lokal.Spec_alkohol[lokal.Alkohol] 
                              + "\nKategorija cena:    " + lokal.Spec_cene[lokal.Cene]
                              + "\nKategorija kapaciteta:    " + lokal.Spec_kapacitet[lokal.Kapacitet] 
                              + "\nDostupan hendikepiranim osobama:    " + ( lokal.Hendikepirani == true ? "DA" : "NE")
                              + "\nDozvoljeno pušenje:    " + ( lokal.Pusenje == true ? "DA" : "NE" )
                              + "\nMogućnost rezervacije:    " + ( lokal.Rezervacije == true ? "DA" : "NE" ),
                    Name = name,
                    Source = new BitmapImage(new Uri(lokal.Slika, UriKind.Absolute)),
                };

                canvas.Children.Add(BodyImage);

                // dodajem osluskivace na MouseMove i PreviewMouseLeftButtonDown
                canvas.Children[canvas.Children.Count - 1].MouseMove += Canvas_MouseMove;  
                canvas.Children[canvas.Children.Count - 1].PreviewMouseLeftButtonDown += ListView_PreviewMouseLeftButtonDown;

                Canvas.SetTop(BodyImage, e.GetPosition(canvas).Y);
                Canvas.SetLeft(BodyImage, e.GetPosition(canvas).X);
                lokal.Pozicija.Add(new Point(e.GetPosition(canvas).X, e.GetPosition(canvas).Y));
            }
        }

        public static Canvas can;
        private void ucitajPozicije()
        {
            int dim = 20;      
            foreach (Meni_stavke.Lokal lokal in Meni_stavke.Dodaj.Lokali)
            {
                if(lokal.Pozicija.Count != 0)
                {
                    foreach (Point p in lokal.Pozicija) {

                        int coordY = (int)p.Y;      // jer mi je Y koordinata u double, a Name ne sme da sadrzi nista osim 
                                                                 // slova, brojeva i underscore-a
                        string name = "a" + p.X.ToString() + "_" + coordY.ToString();       // "a" jer mora poceti slovom ili "_" underscore-om
                        Image BodyImage = new Image
                        {
                            Width = dim,
                            Height = dim,

                            ToolTip = "Oznaka:    " + lokal.Oznaka
                                      + "\nNaziv lokala:    " + lokal.Ime
                                      + "\nTip lokala:    " + Tip.DodajTip.ListaTipova[lokal.Tip].NazivTipa
                                      + "\nDatum otvaranja:    " + lokal.Datum
                                      + "\nSluženje alkohola:    " + lokal.Spec_alkohol[lokal.Alkohol]
                                      + "\nKategorija cena:    " + lokal.Spec_cene[lokal.Cene]
                                      + "\nKategorija kapaciteta:    " + lokal.Spec_kapacitet[lokal.Kapacitet]
                                      + "\nDostupan hendikepiranim osobama:    " + (lokal.Hendikepirani == true ? "DA" : "NE")
                                      + "\nDozvoljeno pušenje:    " + (lokal.Pusenje == true ? "DA" : "NE")
                                      + "\nMogućnost rezervacije:    " + (lokal.Rezervacije == true ? "DA" : "NE"),
                            Name = name,
                            Source = new BitmapImage(new Uri(lokal.Slika, UriKind.Absolute)),
                        };

                        canvas.Children.Add(BodyImage);

                        // dodajem osluskivace na MouseMove i PreviewMouseLeftButtonDown
                        canvas.Children[canvas.Children.Count - 1].MouseMove += Canvas_MouseMove;
                        canvas.Children[canvas.Children.Count - 1].PreviewMouseLeftButtonDown += ListView_PreviewMouseLeftButtonDown;

                        Canvas.SetTop(BodyImage, p.Y);
                        Canvas.SetLeft(BodyImage, p.X);
                    }
                }

                can = canvas;
            }
        }


        // postavljanje background-a za canvas
        private ImageSource _BGImage = new BitmapImage(new Uri("mapaBela.png", UriKind.Relative));
        public ImageSource BGImage
        {
            get { return _BGImage; }
            set
            {
                if (value != _BGImage)
                {
                    _BGImage = value;
                    OnPropertyChanged("BGImage");
                }
            }
        }

        private void btnPreviousTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tcSample.SelectedIndex - 1;
            if (newIndex < 0)
                newIndex = tcSample.Items.Count - 1;
            tcSample.SelectedIndex = newIndex;
        }

        private void btnNextTab_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tcSample.SelectedIndex + 1;
            if (newIndex >= tcSample.Items.Count)
                newIndex = 0;
            tcSample.SelectedIndex = newIndex;
        }

        private void btnSelectedTab_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Treutno selektovani tab je \"" + (tcSample.SelectedItem as TabItem).Header + "\"", 
                "Selektovani tab", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Tab1.IsSelected)
            {
                selectedTab = 0;
                Tab1.Height = 35;
                pretrazi_btn.IsEnabled = false;
            }
            else
            {
                pretrazi_btn.IsEnabled = true;
                Tab1.Height = 28;
            }

            if (Tab2.IsSelected)
            {
                selectedTab = 1;
                Tab2.Height = 35;
            }
            else
            {
                Tab2.Height = 28;
            }
            if (Tab3.IsSelected)
            {
                selectedTab = 2;
                Tab3.Height = 35;
            }
            else
            {
                Tab3.Height = 28;
            }
            if (Tab4.IsSelected)
            {
                selectedTab = 3;
                Tab4.Height = 35;
            }
            else
            {
                Tab4.Height = 28;
            }
        }

        /* BUTTONS           */
        public static ObservableCollection<Meni_stavke.Lokal> stari_lokali = new ObservableCollection<Meni_stavke.Lokal>();
        public static ObservableCollection<Tip.TipLokala> stari_tipovi = new ObservableCollection<Tip.TipLokala>();
        public static ObservableCollection<Etiketa.Etiketa> stare_etikete = new ObservableCollection<Etiketa.Etiketa>();

        public static bool izmTab = false;       // da bih mogao traziti samo ukoliko sam u tabelarnom rezimu (ne i ako dodajem, menjam)
        private void Pretrazi_btn(object sender, RoutedEventArgs e)        // dugme na koje otvaram novi dijalog
        {
            switch (selectedTab)
            {
                case 0:
                    // nemam dozvolu da pretrazujem ako je selektovana mapa
                    break;
                case 1:
                    {
                        if (stari_lokali.Count == 0) {
                            foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali)
                            {
                                stari_lokali.Add(l);
                            }
                        }

                        if (izmTab == true) {
                            Pretrazi p = new Pretrazi("lokala");
                            p.Show();
                        }
                        else
                        {
                            MessageBox.Show("Pretraživanje možete vršiti samo ukoliko se nalazite \nu tabelarnom režimu prikaza.",
                                "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    break;
                case 2:
                    {
                        if (stari_tipovi.Count == 0)
                        {
                            foreach (Tip.TipLokala l in Tip.DodajTip.ListaTipova)
                            {
                                stari_tipovi.Add(l);
                            }
                        }
                        if (izmTab == true)
                        {
                            Pretrazi p = new Pretrazi("tipa lokala");
                            p.Show();
                        }
                        else
                        {
                            MessageBox.Show("Pretraživanje možete vršiti samo ukoliko se nalazite \nu tabelarnom režimu prikaza.",
                                "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    break;
                case 3:
                    {
                        if (stare_etikete.Count == 0)
                        {
                            foreach (Etiketa.Etiketa l in Etiketa.DodajEtiketu.ListaEtiketa)
                            {
                                stare_etikete.Add(l);
                            }
                        }

                        if (izmTab == true)
                        {
                            Pretrazi p = new Pretrazi("etikete");
                            p.Show();
                        }
                        else
                        {
                            MessageBox.Show("Pretraživanje možete vršiti samo ukoliko se nalazite \nu tabelarnom režimu prikaza.",
                                "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    break;
                default:
                    
                    break;
            }
        }
        /* ---------------- */

        private void DemoMod_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (isDemoMode == false)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private int stanje;
        private static DispatcherTimer timer;
        private static bool isDemoMode = false;
        public static ObservableCollection<Meni_stavke.Lokal> vrati_stare = new ObservableCollection<Meni_stavke.Lokal>();
        private void DemoMod_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (Meni_stavke.Lokal l in Meni_stavke.Dodaj.Lokali)
            {
                vrati_stare.Add(l);
            }
            isDemoMode = true;
            stanje = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private MainWindow s;
        private MainWindow s1;
        private Meni_stavke.Dodaj d;
        private Tip.DodajTip dt;
        private Etiketa.DodajEtiketu de;
        private Etiketa.IzmeniEtiketu ie;
        public static int demoYes = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            switch (stanje)
            {
                case 0:
                    {
                        s1 = s;
                        s = new MainWindow();
                        s.Show();

                        if (s1 != null)         // hide-uj onaj stari ako ga ima
                            s1.Hide();
                        stanje++;
                    }
                    break;
                case 1:
                    {
                        s.Tab2.IsSelected = true;
                        stanje++;
                    }
                    break;
                case 2:
                    {
                        d = new Meni_stavke.Dodaj();            // klik na dugme dodaj
                        s.Tab2.Content = d;
                        stanje++;
                    }
                    break;
                case 3:
                    {
                        d.txt_oznaka.Text = "oznaka lokala";
                        stanje++;
                    }
                    break;
                case 4:
                    {
                        d.txt_naziv.Text = "naziv lokala";
                        stanje++;
                    }
                    break;
                case 5:
                    {
                        d.cb_alkohol.SelectedIndex = 2;
                        stanje++;
                    }
                    break;
                case 6:
                    {
                        d.chb_hendikepirani.IsChecked = true;
                        stanje++;
                    }
                    break;
                case 7:
                    {
                        demoYes = 1;
                        d.btnDodaj.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));          // dodaj taj lokal
                        stanje++;
                    }
                    break;
                case 8:
                    {
                        demoYes = 0;
                        stanje++;
                    }
                    break;
                case 9:
                    {
                        s.Tab3.IsSelected = true;
                        stanje++;
                    }
                    break;
                case 10:
                    {
                        dt = new Tip.DodajTip();
                        s.Tab3.Content = dt;
                        stanje++;
                    }
                    break;
                case 11:
                    {
                        demoYes = 1;
                        dt.btnCancel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));          
                        stanje++;
                    }
                    break;
                case 12:
                    {
                        demoYes = 0;
                        stanje++;
                    }
                    break;
                case 13:
                    {
                        s.Tab4.IsSelected = true;
                        stanje++;
                    }
                    break;
                case 14:
                    {
                        de = new Etiketa.DodajEtiketu();
                        s.Tab4.Content = de;
                        stanje++;
                    }
                    break;
                case 15:
                    {
                        de.txt_oznaka.Text = "oznaka";
                        stanje++;
                    }
                    break;
                case 16:
                    {
                        de.txt_opis.Text = "proizvoljan opis vezan za datu etiketu";
                        stanje++;
                    }
                    break;
                case 17:
                    {
                        de.cpicker.SelectedColor = Color.FromRgb(0,0,0);
                        stanje++;
                    }
                    break;
                case 18:
                    {
                        demoYes = 1;
                        de.btnDodaj.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        stanje++;
                    }
                    break;
                case 19:
                    {
                        demoYes = 0;
                        ie = new Etiketa.IzmeniEtiketu();
                        s.Tab4.Content = ie;
                        stanje++;
                    }
                    break;
                case 20:
                    {
                        ie.txtAnswerOznaka.Text = "1";
                        stanje++;
                    }
                    break;
                case 21:
                    {
                        ie.txtAnswerOznaka.Text = "12";
                        stanje++;
                    }
                    break;
                case 22:
                    {
                        s.Tab1.IsSelected = true;
                        stanje = 0;
                    }
                    break;

                default:
                    {
                        timer.Stop();
                        s.Close();
                    }
                    break;
            }
            
        }

        private void SacuvajEtiketu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Etiketa.DodajEtiketu.ListaEtiketa.Count != 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SacuvajEtiketu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            sacuvajEtiketu("etikete.txt");
            MessageBox.Show("Uspesno ste sacuvali etikete!", "Sacuvaj etikete", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UcitajEtiketu_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UcitajEtiketu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Etiketa.DodajEtiketu.ListaEtiketa = ucitajEtikete("etikete.txt");
            MessageBox.Show("Uspesno ste ucitali etikete!", "Ucitaj etikete", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SacuvajTipove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Tip.DodajTip.ListaTipova.Count != 0)
            {
                e.CanExecute = true;
            }
            else
                e.CanExecute = false;
        }

        private void SacuvajTipove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            sacuvajTip("tipovi.txt");
            MessageBox.Show("Uspesno ste sacuvali tipove lokala!", "Sacuvaj tipove lokala", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UcitajTipove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UcitajTipove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tip.DodajTip.ListaTipova = ucitajTipove("tipovi.txt");
            MessageBox.Show("Uspesno ste ucitali tipove lokala!", "Ucitaj tipove lokala", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SacuvajLokale_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Meni_stavke.Dodaj.Lokali.Count != 0)
            {
                e.CanExecute = true;
            }
            else
                e.CanExecute = false;
        }

        private void SacuvajLokale_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // XML sacuvavanje - jer moram objekat da sacuvam

            sacuvajTip("tipoviLokal.txt");
            sacuvajEtiketu("etiketeLokal.txt");

            string fullPath = System.IO.Path.GetFullPath("lokali.txt");
            using (var writer = new System.IO.StreamWriter(fullPath))
            {
                var serializer = new XmlSerializer(Meni_stavke.Dodaj.Lokali.GetType());
                serializer.Serialize(writer, Meni_stavke.Dodaj.Lokali);
                writer.Flush();
            }

            MessageBox.Show("Uspešno ste sačuvali u fajl sistem!", "Sačuvaj lokale", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UcitajLokale_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UcitajLokale_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ucitajLok();   
            MessageBox.Show("Uspešno ste učitali iz fajl sistema!", "Ucitaj lokale", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ucitajLok()
        {
            Etiketa.DodajEtiketu.ListaEtiketa = ucitajEtikete("etiketeLokal.txt");      // ucitam tipove koje sam dodao za te lokale
            Tip.DodajTip.ListaTipova = ucitajTipove("tipoviLokal.txt");             // ucitam etikete

            // XML ucitavanje

            ObservableCollection<Meni_stavke.Lokal> listaLokala = Meni_stavke.Dodaj.Lokali;
            string fullPath = System.IO.Path.GetFullPath("lokali.txt");
            using (var stream = System.IO.File.OpenRead(fullPath))
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Meni_stavke.Lokal>));
                listaLokala = serializer.Deserialize(stream) as ObservableCollection<Meni_stavke.Lokal>;
            }

            Meni_stavke.Dodaj.Lokali = listaLokala;
        }

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
        

        private void sacuvajTip(string fileName)               // cuvam trenutne tipove za lokal
        {
            string fullPath = System.IO.Path.GetFullPath(fileName);
            System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath);

            foreach (Tip.TipLokala t in Tip.DodajTip.ListaTipova)
            {
                string text = t.Oznaka + "|" + t.NazivTipa + "|" + t.Opis + "|" + t.Slika;
                file.WriteLine(text);
            }
            file.Close();
        }

        private void sacuvajEtiketu(string fileName)           // cuvam trenutne etikete za lokal
        {
            string fullPath = System.IO.Path.GetFullPath(fileName);
            System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath);

            foreach (Etiketa.Etiketa et in Etiketa.DodajEtiketu.ListaEtiketa)
            {
                string text = et.Oznaka + "|" + et.Opis + "|" + et.Boja.ToString();
                file.WriteLine(text);
            }
            file.Close();
        }

        private ObservableCollection<Etiketa.Etiketa> ucitajEtikete(string fileName)
        {
            ObservableCollection<Etiketa.Etiketa> listaEtiketa = Etiketa.DodajEtiketu.ListaEtiketa;
            listaEtiketa.Clear();
            string fullPath = System.IO.Path.GetFullPath(fileName);
            string[] lines = System.IO.File.ReadAllLines(fullPath);

            foreach (string line in lines)
            {
                Etiketa.Etiketa l = new Etiketa.Etiketa(line);
                listaEtiketa.Add(l);
            }

            return listaEtiketa;
        }

        private ObservableCollection<Tip.TipLokala> ucitajTipove(string fileName)
        {
            ObservableCollection<Tip.TipLokala> listaTipova = Tip.DodajTip.ListaTipova;
            listaTipova.Clear();
            string fullPath = System.IO.Path.GetFullPath(fileName);
            string[] lines = System.IO.File.ReadAllLines(fullPath);

            foreach (string line in lines)
            {
                Tip.TipLokala l = new Tip.TipLokala(line);
                listaTipova.Add(l);
            }

            return listaTipova;
        }
        
    }

}
