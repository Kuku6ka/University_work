   M S B A M L   `   `   `  ����<  81, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:_1_1  5   	       _1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:_1     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� -   5         ��Name$ unitsComboBox��5      $
��Left=�6*   $	��Top=�6E   $	��200��6]   $��25��6i   .@�5      �� $��Metric��6   �� 5      $��Imperial��6   5      �� 5   
   -   5        $��1��5      $��0��6!   $)��#Create MeasureLengthDevice Instance��6.   $	��Top=�6\   $	��200��6t   $��30��6�   6�   �� 5   
   -   5        $��1��5      $��0��6!   $��Start Collecting��6.   $��Center=�6J   $	��200��6e   $��30��6q   6}   �� 5   
   -   5        $��1��5      $��0��6!   $��Stop Collecting��6.   $��Bottom=�6H   $	��200��6c   $��30��6o   6{   �� 5   
   -   5        $��1��5      $��1��6!   $��Get Raw Data��6.   $��Center=�6E   $	��200��6`   $��30��6l   6x   �� 5   
   -   5        $��1��5      $��1��6!   $��Get Metric Value��6.   $��Bottom=�6I   $	��200��6d   $��30��6p   6|   �� 5   
   -   5        $��2��5      $��1��6!   $��Get Imperial Value��6.   $��Bottom=�6K   $	��200��6f   $��30��6r   6~   �� 5!   
   -   5         ��Name$ metricValueTextBlock��5!   1   $��0��6   $��2��6$   $
��Left=�6O   $	��Top=�6j   �� 5"   
   -	   5        $ imperialValueTextBlock��5"   1   $��2��6   $��1��6!   $
��Left=�6Q   $	��Top=�6l   �� 5$   
   -
   5         ��Name$ rawDataListBox��5$   /   $��1��6   $��0��6   $
��Left=�6G   $	��Top=�6b   $	��200��6z   $	��100��6�   5&      5'      