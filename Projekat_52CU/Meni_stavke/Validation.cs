using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat_52CU.Meni_stavke
{
    public class Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (string.IsNullOrEmpty(s))
                    return new ValidationResult(false, "Unesite oznaku lokala!");

                foreach (Lokal l in Dodaj.Lokali)
                {
                    if (l.Oznaka.Equals(s))
                    {
                        return new ValidationResult(false, "Uneta oznaka vec postoji!");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Greska nepoznatog porekla");
            }
        }
    }

    public class ValidateNaziv : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (string.IsNullOrEmpty(s))
                    return new ValidationResult(false, "Unesite naziv lokala!");
                /*
                foreach (Lokal l in Dodaj.Lokali)
                {
                    if (l.Ime.Equals(s))
                    {
                        return new ValidationResult(false, "Uneto ime lokala vec postoji!");
                    }
                }*/

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Greska nepoznatog porekla.");
            }
        }
    }

    public class MinMaxValidationRule : ValidationRule
    {
        public double Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is string)
            {
                string str = value.ToString();
                int d = str.Length;
                if (d > Max) return new ValidationResult(false, "Mozete uneti max "+ Max +" karaktera");
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Greska nepoznatog porekla.");
            }
        }
    }
}
