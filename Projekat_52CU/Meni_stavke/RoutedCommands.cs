using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekat_52CU.Meni_stavke
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand Dodaj = new RoutedUICommand(
            "Dodaj _lokal",
            "DodajLokal",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control),
                new KeyGesture(Key.L, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand Izmeni = new RoutedUICommand(
            "Izmeni lokal",
            "IzmeniLokal",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.I, ModifierKeys.Control),
                new KeyGesture(Key.I, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand DTip = new RoutedUICommand(
            "Dodaj _tip",
            "DodajTip",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.T, ModifierKeys.Control),
                new KeyGesture(Key.T, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand ITip = new RoutedUICommand(
            "Izmeni tip",
            "IzmeniTip",
            typeof(RoutedCommands)
            );


        public static readonly RoutedUICommand DEtiketu = new RoutedUICommand(
            "Dodaj _etiketu",
            "DodajEtiketu",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control),
                new KeyGesture(Key.E, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand IEtiketu = new RoutedUICommand(
            "_Izmeni etiketu",
            "IzmeniEtiketu",
            typeof(RoutedCommands)
            );

        public static readonly RoutedUICommand PrikaziTabela = new RoutedUICommand(
            "Prikazi _tabelu",
            "PrikaziTabela",
            typeof(RoutedCommands)
            );

        public static readonly RoutedUICommand SacuvajEtiketu = new RoutedUICommand(
            "SacuvajEtiketu",
            "SacuvajEtiketu",
            typeof(RoutedCommands)
            
            );

        public static readonly RoutedUICommand UcitajEtiketu = new RoutedUICommand(
            "UcitajEtiketu",
            "UcitajEtiketu",
            typeof(RoutedCommands)
            
            );

        public static readonly RoutedUICommand SacuvajTipove = new RoutedUICommand(
            "SacuvajTipove",
            "SacuvajTipove",
            typeof(RoutedCommands)

            );

        public static readonly RoutedUICommand UcitajTipove = new RoutedUICommand(
            "UcitajTipove",
            "UcitajTipove",
            typeof(RoutedCommands)

            );

        public static readonly RoutedUICommand SacuvajLokale = new RoutedUICommand(
            "SacuvajLokale",
            "SacuvajLokale",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control),
                new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand UcitajLokale = new RoutedUICommand(
            "UcitajLokale",
            "UcitajLokale",
            typeof(RoutedCommands)

            );

        public static readonly RoutedUICommand DemoMod = new RoutedUICommand(
           "Demo mod",
           "DemoMod",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.D, ModifierKeys.Control),
                new KeyGesture(Key.D, ModifierKeys.Alt | ModifierKeys.Control)
           }
           );
    }
}
