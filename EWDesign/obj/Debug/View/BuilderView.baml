   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:EWDesign.ViewEWDesign.View  5      7 clr-namespace:EWDesign.ViewModelEWDesign.ViewModel  5          EWDesign.View.BuilderView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:EWDesign.View     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6	viewModel clr-namespace:EWDesign.ViewModel     5        � Title$  BuilderView��5	      $	��920��5
      $	��600��5      
������  � WindowStartupLocation$ CenterScreen=�5       � WindowState$ 	Maximized=�5       � DataContext 5      )   #EWDesign.ViewModel.BuilderViewModel  5   
   .��5      �� ���#121A21 �� CornerRadius$ 10��6"   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5      .+�5      �� $��20,0,0,0q�5      $
��Left=�5       $��Center=�5!      $��22(�5"      
�������$��
Components��5$      �� 5&      $��1��5'      $��Center=�5(      $��Center=�5)      $��30(�5*      
�������$!��Start Building Your WebPage��5,      �� 5.      $��1��6   $��10q�6&   .+�5/      � .+�50      �� -   5        $��SearchTextBox��51      $��30��52      $��5q�53      
�������$��16(�55      
���   ���57      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 59      $��8,0,0,0q�5:      $��Center=�5;      $��16(�5<      
������� J� IsHitTestVisible .  $��Search Component...��5?      y�5@      �� *Text+	 �� ElementName$	 SearchTextBox�� *  &StringNullOrEmptyToVisibilityConverter
 �� 	Converter#
 [  5A      �� 5D      $
��Auto=�6   .��5E      �� ��6#   �� *GroupedComponents+��5F      �� 5G      .R�5H   "   9� 
������� 9� 
IsExpanded . ��5J   %   �� *Key+.��5L   &   �� ��63   �� ��5M   *   �� 5N   .   .R�5O   2   �� -   5        $��5q�5P   5    �� Padding$ 8q�5Q   5   ���#1E2A36$ 8��5S   5    � Cursor$
 Hand��5T   5   ._�5V   6   �� 
���������6S   �� *DisplayName+5W   3   5X   /   5Z   '   5[   #   5\      5^      5_      5`      �� 5b      $��1��5c      $��1��5d      $��10q�5e      ���#243647$��1q�5g      ._�5i      �� $
��Auto=�6   .��5j      �� -   5        $��DropArea��5k       J� 	AllowDrop . 
������ 5n      5o      5q      5t      5v      5w      