   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:EWDesignEWDesign  5      7 clr-namespace:EWDesign.ViewModelEWDesign.ViewModel  5          EWDesign.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:EWDesign     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6	viewModel clr-namespace:EWDesign.ViewModel     5        � Title$  
MainWindow��5	      $	��920��5
      $	��600��5      
������  � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5       � DataContext 5      &   EWDesign.ViewModel.MainViewModel  5   
   .��5      �� ���#121A21._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5      .+�5       �� $��20,0,0,0q�5!      $
��Left=�5"      $��Center=�5#      $��22(�5$      
�������$��	EW Design��5&      �� 5(      $��1��6   .+�5)      	� $��50��5*      $	��New��5,      $��14(�5-      
������� d� 	IsChecked$
 TrueY�5/         MenuButtonTheme#��[  50      ��5+      �� *NewRC+50      	� 51      $��50��52      $��Recent��54      $��14(�55      
�������#��[  57      ��53      �� *
RecentRC+57      	� 58      $��50��59      $
��Open��5:      $��14(�5;      
�������#��[  5=      	� 5>      $��50��5?      $��Settings��5@      $��14(�5A      
�������#��[  5C      	� 5D      $��50��5E      $��About��5F      $��14(�5G      
�������#��[  5I      5J      �� 5L      $��1��5M      $��1��5N      $��10q�5O      ��5P      �� *CurrentView+5Q      5R      5S      