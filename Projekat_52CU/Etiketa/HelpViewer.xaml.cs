using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        public HelpViewer(string key)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();            // .../bin/Debug
            string path = String.Format("{0}/Help/{1}.htm", curDir, key);

            if (!File.Exists(path))
            {
                key = "error";
            }

            Uri u = new Uri(String.Format("file:///{0}/Help/{1}.htm", curDir, key));
            wbHelp.Navigate(u);
        }
    }
}
