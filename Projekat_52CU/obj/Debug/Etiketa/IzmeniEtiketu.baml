   M S B A M L   `   `   `  ����H  DProjekat_52CU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Projekat_52CU.EtiketaProjekat_52CU.Etiketa  5   	   =#clr-namespace:Projekat_52CU.EtiketaProjekat_52CU.Etiketa  5	   	   )    #Projekat_52CU.Etiketa.IzmeniEtiketu   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :xctk)http://schemas/xceed.com/wpf/xaml/toolkit     5   	   5local#clr-namespace:Projekat_52CU.Etiketa     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f45xctk1)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -val#clr-namespace:Projekat_52CU.Etiketa  5	   	     � 	Resources  5         CenterGridHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  txtStyle  	tboxStyle  	opisStyle  btnStyle �� 
TargetType �Name ��Name �� CornerRadius 
� GradientOrigin	 �� RelativeTransform
 �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%`	  &	        &	 L     &	     &	     &	 ~    �� 5   
   "  6.   .�5      ��  � ��$ Center=�6;   5      �� 5   
   " ��6!   .�5      ��  � ��$ 13(�6)   �� 5       � ��$ Center=�62   �� 5       � ��$ Right=�64   �� 5       � ��$ 3,0,3,0q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ 13(�6)   �� 5       � ��$ 25��6'   �� 5       � ��$	 230��6&   �� 5       � ��$ 2,2,2,2q�6(   �� 5       � ��$
 Left=�64   �� 5       � ��$ 20,0,0,0q�6'   5      �� 5   
   " ��6"   .�5      ��  � ��$ 13(�6)   �� 5       � ��$
 Auto��6'   �� 5        � ��$ 2,3,2,3q�6(   �� 5!       � ��$ 
20,0,100,0q�6'   �� 5"       �@��AcceptsReturn . 6.   �� 5#       �@��TextWrapping$
 Wrap=�6-   �� 5$      # �@��VerticalScrollBarVisibility$
 Auto=�6<   5%      �� 5&   
   " ��6!   .�5'      ��  � ��$ 	10,20,5,5q�6'   �� 5(       � ��$ 80��6&   �� 5)       � ��$ 40��6'   �� 5*       � ��$ 5q�6(   �� 5+       � ��$ 13(�6)   �� 5,       � �� 5-      �� 5.      " ��6&   .R�5/      � $
 grid��6   .+�50      �� $ border��6%   $ 8��65   
���   �$	��0.5q�6Z   ��51   "   
� 52   &   $ 0.496,1.052&�6:   	 53   *   V� 54   .   .~�55   2   �� $	
 0.5Q�6A   $	 0.5Q�6O   $	 1.5Q�56   9   $	 1.5Q�6F   T� 57   2   $
 0.02Q�6E   $	 0.3Q�6N   58   /   .��5:   *   � $ 1Q�67   $ LightSkyBlue��6B   � 5;   *   $	 0.3Q�67   $ Transparent��6D   5<   '   ._�5>   "   �� $��Center=�63   $��Center=�5?   +   5@   #   5A      5C       5D      P� 5E       � �� . . �5F   "   �� $ border��6?    � �� 5G   &   
� 5H   *   $ 0.496,1.052&�6>   	 5I   .   V� 5J   2   .~�5K   6   �� $	
 0.5Q�6E   $	 0.5Q�6S   $	 1.5Q�6a   $	 1.5Q�6n   T� 5L   6   $
 0.02Q�6I   $	 0.3Q�6R   5M   3   .��5O   .   � $ 	LightBlue��6;   $ 1Q�6M   � 5P   .   $ 
MediumBlue��6;   $	 0.3Q�6N   5Q   +   5S   #   5T      P� 5U       �@J�IsMouseOver . . �5V   "   �� $ border��6@    � �� �	#FF33962B6T   5W      P� 5X       � {� .  . �5Y   "   �� $
 grid��6<    �@J�Opacity$	 0.3Q�6N   5Z      5]      5_      5`      5a       J� CommandBindings 5c      )  #System.Windows.Input.CommandBinding  5d   
   -   5           Command$ ApplicationCommands.Help��5d      6<   .��5g      �� $
��Auto=�6   $
��Auto=�66   .��5h      �  � RowDefinitions 5i   
   �� 5j       � ColumnDefinitions 5l   
   �� 5m      .+�5o   
   �� $��0��6   $��0��6&   ��66   ��  �� Path$ 	MyContent�� 5p   
   -   5        $��control��5p   ,   $��0��6   $��0��6    5q      �� 5r      $��60 �6    �� 5s      $	��150 �6    �� 5t       5v      �� 5w      .+�5z      �� $��0��6   .+�5{      �� $��50��6   $��5,5,5,5q�6%   
����ح�$	��0.6q�6N   ._�5|      ��  �� Orientation$ 
Horizontal=�6!   .+�5}      ��  � ToolTip$F @Filtriranje sadržaja tabela etiketa na osnovu unetih parametara��6    $��15(�5~   %   $��Courier New)�63   
����  �$��Filtriranje��6b   6z   �� 5      $��13(�6$   $��120,12,0,12q�62   $��Oznaka:��6G   6X   �� 5�      -   5        $��txtAnswerOznaka��5�      (   "Projekat_52CU.Etiketa.HelpProvider   HelpKey$ OznakaEtikete��5�   "   $��2q�5�   4   $��13(�6@   $J DUneti jedinstvenu oznaku etikete po kojoj će se vršiti filtriranje��6N   $	��140��5�   *   $��15,12q�66   6G   �� 5�      -   5        $��Poništi��5�   !    �� IsCancel . $- 'Vrati tabelu u prvobitni kontekst (Esc)��6C   #��[ 5�   )   $��70��6W   $��31��6b   $��	80,9,12,9q�5�   )   $��0q�6<   6^   5�      5�      5�      &   System.Windows.Controls.DataGrid  5�      -   5          Name$ dgrMain��5�      $��1��5�      $��0,5,0,10q�5�      ���#ccffff$��Center=�6>       
IsReadOnly  . !   AutoGenerateColumns! .  $" �� IsSynchronizedWithCurrentItem$
" TrueY�5�      #   CanUserResizeColumns# . $   CanUserResizeRows$ .  %   CanUserSortColumns% . & �� SelectedIndex& 5�   (   �� $ PromeniSelekcija���5�      �� $ Etikete�' �� UpdateSourceTrigger$' PropertyChanged=�(   Columns( 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn)   Header$ ) Jedinstvena oznaka etikete��6-   *   HeaderStyle#* [  6Q   +   Width5  /System.Windows.Controls.DataGridLengthConverter$	+ 600 6�   ,   CellTemplate, 5�      �� 5�      .R�5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��Center=�6H   $��13(�6e   $��20��6s   ��60   �� *Oznaka+6s   5�   #   5�      5�      5�      � 5�      $��0��6   $��2��6#   - � DataContext- 60   �� . �� ElementName$. dgrMain��$ SelectedItem�.+�5�      ]� .+�5�      ]� $��Bottom=�6    .+�5�      �� -   5        $��	btnIzmeni��5�   E   $( "Izmena selektovane etikete (Alt+I)��5�   !   $��Right=�5�      / �� 	IsDefault/ . #��[ 6k   .��5�      �� .��6)   	_Izmeni62   5�      �� 5�      -   5        $��	btnObrisi��5�   4   $) #Obriši selektovanu etiketu (Alt+B)��5�   !   $��Right=�5�      #��[ 6Z   .��5�      �� .��6)   	O_brisi62   5�      �� 5�      -   5        $$ Dodavanje nove etikete (Alt+D)��5�   !   $
��Left=�5�      #��[ 6G   .��5�      �� .��6)   _Dodaj61   5�      �� 5�      $��Bottom=�6$   $��
15,15,15,0q�6=   6S   5�      ]� 5�      $	��Top=�6    $��Stretch=�65   .+�5�      �  5�      �� 5�   "    5�      �� 5�   "   5�      � 5�       5�      �� 5�   "   $��60 �60   �� 5�   "   �� 5�   "   $��60 �60    5�      �� 5�   "   $	��170 �63   �� 5�   "   .+�5�      �� $��0��6(   $��0��68   #��[ 6E   Jedinstvena oznaka:6�   �� 5�      $��0��6(   $��1��68   #��[ 6E   Opis:6z   �� 5�      $��0��6(   $��2��68   #��[ 6E   Boja etikete:6�   �� 5�      -	   5        $��
txt_oznaka��5�   &   {�.  $��1��6t   $��0��6�   #��[ 6�   ��6J   �� $ Oznaka�0 �� Mode$0 OneWay=�6�   �� 5�      -
   5        $��txt_opis��5�   &   {�.  $��1��6H   $��1��6X   #��[ 6�   ��6e   �� $
 Opis�$0 OneWay=�6�   #  Xceed.Wpf.Toolkit.ColorPicker  5�      -   5        $��cpicker��5�   0   {�.  $��1��6Q   $��3��6a   1   DisplayColorAndName1 . $
��Left=�6�   $��60,0,0,0q�6�   $��30��6�   $	��150��6�   2   SelectedColor2 6n   �� *Boja+$0 OneWay=�6�   5�      5�      5�      5�      5�      5�      5�      5�      