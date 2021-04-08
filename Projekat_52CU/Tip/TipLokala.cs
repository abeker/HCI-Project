using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_52CU.Tip
{
    public class TipLokala : INotifyPropertyChanged
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

        private string _oznaka;
        private string _opis;
        private string _nazivTipa;
        private string _slika;

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

        public string NazivTipa
        {
            get
            {
                return _nazivTipa;
            }
            set
            {
                if (value != _nazivTipa)
                {
                    _nazivTipa = value;
                    OnPropertyChanged("NazivTipa");
                }
            }
        }

        public string Slika
        {
            get
            {
                return _slika;
            }
            set
            {
                if (value != _slika)
                {
                    _slika = value;
                    OnPropertyChanged("Slika");
                }
            }
        }

        public TipLokala() { }

        public TipLokala(string line)
        {
            char[] delimiterChars = { '|' };
            string[] words = line.Split(delimiterChars);
            this.Oznaka = words[0];
            this.NazivTipa = words[1];
            this.Opis = words[2];
            this.Slika = words[3];
        }
    }
}
