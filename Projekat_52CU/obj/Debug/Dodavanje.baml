   M S B A M L   `   `   `  ����H  DProjekat_52CU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Projekat_52CUProjekat_52CU  5   	   E'clr-namespace:Projekat_52CU.Meni_stavkeProjekat_52CU.Meni_stavke  5   	       Projekat_52CU.Dodavanje   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Projekat_52CU     5   	   7cmd'clr-namespace:Projekat_52CU.Meni_stavke     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Icon$ grb_jpg_GXD_icon.ico��5
   	    � Title$ 	Dodavanje��5   	   $	��150��6   $	��400��6(    � 	Resources 5         fancyButtonStyle �� 
TargetType �� Property �� Value 
� GradientOrigin 
� Center	 
� RadiusX
 
� RadiusY � Offset � Color��%�  &	        �� 5   
   " ��6)   .�5      ��  � ��$ 4q�6'   �� 5       � ��$ 25q�6(   �� 5       � ��$ 25(�6)   �� 5       � ��$ 0q�60   �� 5       � ��$
 Bold"�6+   �� 5       �@J�OpacityMask 5      
� 5      $ 0.5, 0.5&�6*   $ 0.5,0.5&�6D   $		 0.5Q�6U   $	
 0.5Q�6c   ��5      � $	 0.2Q�6+   $ Black��68   6H   � 5      $
 0.75Q�6+   $ Transparent��69   6O   5      5      5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5    
   -   5           Command$ cmd:RoutedCommands.Dodaj��5       6v     5!   
   -   5        $ cmd:RoutedCommands.DTip��5!      6s     5"   
   -   5        $! cmd:RoutedCommands.DEtiketu��5"      6   .��5$      ]� .+�5%   
   `� $	��Top=�6   $��Center=�6+   $��Center=�6H   $	��400��6c   }�. .�5&      c�  `� IsLocked . .@�5'      ��  �� Orientation$ 
Horizontal=�6    c� OverflowMode$ Never=�66   .+�5(      ��  � ToolTip$ Dodaj lokal��6   #��[  63   $��cmd:RoutedCommands.Dodaj��6i   $��Dodaj
 lokal��6�   6�   �� 5)      $ 	Dodaj tip��6   #��[  61   $��cmd:RoutedCommands.DTip��6g   $��Dodaj
   tip��6�   6�   �� 5*      $ Dodaj etiketu��6   #��[  65   $!��cmd:RoutedCommands.DEtiketu��6k   $�� Dodaj
etiketu��6�   6�   5+      5,      5-      5.      5/      