   M S B A M L   `   `   `  ����H  DProjekat_52CU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Projekat_52CU.EtiketaProjekat_52CU.Etiketa  5   	   =#clr-namespace:Projekat_52CU.EtiketaProjekat_52CU.Etiketa  5   	   '    !Projekat_52CU.Etiketa.IzmenaPrava   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Projekat_52CU.Etiketa     5   	   3val#clr-namespace:Projekat_52CU.Etiketa     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xctk1)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � 	Resources  5
         txtStyle �� 
TargetType �� Property �� Value  	tboxStyle  	opisStyle  btnStyle �� 
TargetType �Name ��Name �� CornerRadius 
� GradientOrigin	 �� RelativeTransform
 �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%	  &	        &	 �     &	 �    &	 4    �� 5   
   " ��6!   .�5      ��  � ��$ Center=�62   �� 5       � ��$ Right=�64   �� 5       � ��$ 13(�6)   �� 5       � ��$ 3,0,0,0q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ 30��6'   �� 5       � ��$ 13(�6)   �� 5       � ��$ 2,2,2,2q�6(   �� 5       � ��$	 230��6&   �� 5       � ��$
 Left=�64   �� 5       � ��$ 20,0,0,0q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$
 Auto��6'   �� 5       � ��$ 13(�6)   �� 5       � ��$ 2,3,2,3q�6(   �� 5       � ��$ 20,10,150,10q�6'   �� 5       �@��AcceptsReturn . 6.   �� 5       �@��TextWrapping$
 Wrap=�6-   �� 5       # �@��VerticalScrollBarVisibility$
 Auto=�6<   5!      �� 5"   
   " ��6!   .�5#      ��  � ��$ 	10,20,5,5q�6'   �� 5$       � ��$ 80��6&   �� 5%       � ��$ 40��6'   �� 5&       � ��$ 5q�6(   �� 5'       � ��$ 13(�6)   �� 5(       � �� 5)      �� 5*      " ��6&   .R�5+      � $
 grid��6   .+�5,      �� $ border��6%   $ 8��65   
���   �$	��0.5q�6Z   ��5-   "   
� 5.   &   $ 0.496,1.052&�6:   	 5/   *   V� 50   .   .~�51   2   �� $	
 0.5Q�6A   $	 0.5Q�6O   $	 1.5Q�52   9   $	 1.5Q�6F   T� 53   2   $
 0.02Q�6E   $	 0.3Q�6N   54   /   .��56   *   � $ 1Q�67   $ LightSkyBlue��6B   � 57   *   $	 0.3Q�67   $ Transparent��6D   58   '   ._�5:   "   �� $��Center=�63   $��Center=�5;   +   5<   #   5=      5?       5@      P� 5A       � �� . . �5B   "   �� $ border��6?    � �� 5C   &   
� 5D   *   $ 0.496,1.052&�6>   	 5E   .   V� 5F   2   .~�5G   6   �� $	
 0.5Q�6E   $	 0.5Q�6S   $	 1.5Q�6a   $	 1.5Q�6n   T� 5H   6   $
 0.02Q�6I   $	 0.3Q�6R   5I   3   .��5K   .   � $ 	LightBlue��6;   $ 1Q�6M   � 5L   .   $ 
MediumBlue��6;   $	 0.3Q�6N   5M   +   5O   #   5P      P� 5Q       �@J�IsMouseOver . . �5R   "   �� $ border��6@    � �� �	#FF33962B6T   5S      P� 5T       � {� .  . �5U   "   �� $
 grid��6<    �@J�Opacity$	 0.3Q�6N   5V      5Y      5[      5\      5]       J� CommandBindings 5_      )  #System.Windows.Input.CommandBinding  5`   
   -   5           Command$ ApplicationCommands.Help��5`      6<   .��5c      �� $
��Auto=�6   $
��Auto=�66   .��5d   
   �  � RowDefinitions 5e      �� 5f       � ColumnDefinitions 5h      �� 5i      .+�5k      �� $��0��6   $��0��6*   ��6:   ��  �� Path$ 	MyContent�� 5l      -   5        $��control��5l   0   $��0��6   $��0��6#   .+�5m      ]� .+�5n      ]� $��Bottom=�6    .+�5o      �� -   5         � ToolTip$1 +Vrati se u tabelarni prikaz etiketa (Alt+O)��5o   !   $��Right=�5p       �� IsCancel . #��[ 6U   .��5q      �� .��6)   	_Odustani64   5r      �� 5s      -   5        $L FIzmeni etiketu unetim parametrima i vrati
se na tabelu etiketa (Alt+I)��5s   !   $��Right=�5t       �� 	IsDefault . #��[ 6V   .��5u      �� .��6)   	_Izmeni62   5v      �� 5w      $��Bottom=�6$   $��
15,15,15,0q�6<   6R   5x      ]� 5y      $	��Top=�6    $��Stretch=�65   .+�5z      �  5{      �� 5|   "    5~      �� 5   "   5�      � 5�       5�      �� 5�   "   $	��120 �60   �� 5�   "   $��60 �60   �� 5�   "   $	��150 �60   �� 5�   "   $��60 �60    5�      �� 5�   "   $	��210 �63   �� 5�   "   .+�5�      �� $��0��6(   $��0��6[   $��3��6h   $
��Bold"�6|   $��24(�6�   
����  �$��Center=�6�   $��Courier New)�6�   #��[  6�   ��68   �� $ TxtBlockText�6  �� 5�      $��0��6(   $��1��68   #��[  6E   Jedinstvena oznaka:6�   �� 5�      $��0��6(   $��2��68   #��[  6E   Opis:6z   �� 5�      $��0��6(   $��3��68   #��[  6E   Odaberite boju:6�   �� 5�      -   5        $��
txt_oznaka��5�      (   "Projekat_52CU.Etiketa.HelpProvider   HelpKey$ OznakaEtikete��5�   &   $� �Izmeni jedinstvenu oznaku etikete. Voditi računa o ograničenjima:
  1) oznaka mora imati jedinstvenu vrednost
  2) može sadržati najviše 12 karaktera��5�   !   $��1��5�   /   $��1��6?   #��[ 6L   .��6{   ��5�   "   �� 5�   &   $ Oznaka�6.    �� UpdateSourceTrigger$ PropertyChanged=�6<     �� ValidationRules  5�   *   &    Projekat_52CU.Etiketa.Validation  5�   .   ,  &System.Windows.Controls.ValidationRule!   ValidationStep$! RawProposedValue=�6=   0   *Projekat_52CU.Etiketa.MinMaxValidationRule  5�   .   $! ConvertedProposedValue=�6G   
"   Max$" 12Q�6o   5�   '   .��5�   0   # =� ErrorTemplate# 5�   "   �� 5�   &   .R�5�   *   �  5�   .   �� 5�   2    5�   .   �� 5�   2   �� 5�   2   $
��Auto �6C   .+�5�   .   �� $ ��Name$$ textBox��6e   $��0��6H   $��0��6X   6e   �� 5�   .   $��1��68   $��0��6H   
���  ����6U   �� *[0].ErrorContent+6w   5�   +   5�   '   .��5�   <   5�      �� 5�      -   5        $��txt_opis��5�      $ Izmeni opis etikete��5�   &   $��1��5�   H   $��2��6X   #��[ 6e   ��6-   �� $
 Opis�6�   #  Xceed.Wpf.Toolkit.ColorPicker  5�      -   5        $��cpicker��5�   A   $+ %Izmeni boju zakačenu za datu etiketu��5�   0   $��1��5�   P   $��3��6`   %   DisplayColorAndName% . $
��Left=�6�   $��60,0,0,0q�6�   $��30��6�   $	��170��6�   &   SelectedColor& 6   �� $
 Boja�6�   5�      5�      5�      5�      5�      5�      5�      