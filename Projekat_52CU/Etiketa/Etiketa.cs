using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Projekat_52CU.Etiketa
{
    public class Etiketa : INotifyPropertyChanged
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
        private Color _boja;
        private bool _check;
        
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

        public Color Boja
        {
            get
            {
                return _boja;
            }
            set
            {
                if (value != _boja)
                {
                    _boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }

        public bool Check
        {
            get
            {
                return _check;
            }
            set
            {
                if (value != _check)
                {
                    _check = value;
                    OnPropertyChanged("Check");
                }
            }
        }

        public Etiketa() { }

        public Etiketa(string line)
        {
            char[] delimiterChars = { '|' };
            string[] words = line.Split(delimiterChars);
            this.Oznaka = words[0];
            this.Opis = words[1];
            this.Boja = (Color)ColorConverter.ConvertFromString(words[2]); 
        }
    }
}


