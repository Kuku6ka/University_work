   M S B A M L   `   `   `  ����<  84, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:_4_4  5   	       _4.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:_4     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��0��6   $��0��6   $"��Успешные тесты:��6-   $
��Left=�6G   $��Center=�6b   6   �� 5   
   -   5         ��Name$ passCountLabel��5   -   $��0��6   $��0��6   $��Center=�6E   $��Center=�6b   6   �� 5   
   $��0��6   $��0��6   $$��Неудачные тесты:��6-   $
��Left=�6H   $��Center=�6c   $��	0 50 0 0 q�6~   6�   �� 5   
   -   5        $ failCountLabel��5   -   $��0��6   $��0��6   $��Center=�6E   $��Center=�6b   $��0 50 0 0q�6}   6�   �� 5   
   -   5        $��0��5      $��1��6   $	��150��6.   $��30��6:   $��	Run Tests��6F   6s   �� 5   
   $��1��6   $��0��6    $1��+Неудачные тесты(список)��5   G   .��5      �� -   5         ��Name$ reasonsList��5      6-   5       5!      5"      