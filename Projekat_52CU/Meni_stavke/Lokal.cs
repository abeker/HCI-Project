using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Projekat_52CU.Meni_stavke
{
    public class Lokal : INotifyPropertyChanged
    {
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

        #region NotifyProperties
        private string _oznaka;
        private string _ime;
        private string _opis;
        private string _datum_otvaranja;
        private string _ikonica;

        private int _alkohol;
        private int _cene;
        private int _kapacitet;
        private int _tip;
        private string _tip_str;
        private ObservableCollection<Point> _pozicija = new ObservableCollection<Point>();

        private bool _hendikepirani;
        private bool _rezervacije;
        private bool _dozvoljeno_pusenje;
        
        private ObservableCollection<Projekat_52CU.Etiketa.Etiketa> _etikete;
        private ObservableCollection<string> spec_alkohol;     // iz ove liste ubacujem u combo box
        private ObservableCollection<string> spec_cene;    // pravim je ovde, jer se cela tabela
        private ObservableCollection<string> spec_kapacitet;   // bind-uje na Lokal
        private ObservableCollection<string> spec_tip;
        private ObservableCollection<string> spec_etiketa;
        private ObservableCollection<string> list;

        #region Observable liste - inicijalizacija
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

        public ObservableCollection<string> Spec_etiketa
        {
            get
            {
                spec_etiketa = new ObservableCollection<string>();
                foreach (Projekat_52CU.Etiketa.Etiketa t in Projekat_52CU.Etiketa.DodajEtiketu.ListaEtiketa)
                {
                    spec_etiketa.Add(t.Oznaka);
                }

                return spec_etiketa;
            }
            set { }
        }
        #endregion

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
                if (Tip < Projekat_52CU.Tip.DodajTip.ListaTipova.Count) {
                    _tip_str = Projekat_52CU.Tip.DodajTip.ListaTipova[Tip].NazivTipa;
                }
                else 
                {
                    _tip_str = "";   
                }


                return _tip_str;
            }
            set
            {
                
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

        public ObservableCollection<string> Oznaka_etikete      // nebitno, nesto sam pokusavao
        {
            get
            {
                list = new ObservableCollection<string>();
                foreach (Projekat_52CU.Etiketa.Etiketa e in Etikete)
                {
                    if(!list.Contains(e.Oznaka))
                        list.Add(e.Oznaka);
                }
                
                return list;
            }
            set
            {
                
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

        public ObservableCollection<Point> Pozicija               // gde se na mapi nalazi
        {
            get
            {
                return _pozicija;
            }
            set
            {
                if (value != _pozicija)
                {
                    _pozicija = value;
                    OnPropertyChanged("Pozicija");
                }
            }
        }
        #endregion

        public Lokal() { }

    }
}
