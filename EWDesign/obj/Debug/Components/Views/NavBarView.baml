   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      -clr-namespace:EWDesign.CoreEWDesign.Core  5      E'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      G(clr-namespace:EWDesign.Components.ModelsEWDesign.Components.Models  5
      *    $EWDesign.Components.Views.NavBarView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      >
components'clr-namespace:EWDesign.Components.Views     5      2
convertersclr-namespace:EWDesign.Core     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EWDesign.Components.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      :model(clr-namespace:EWDesign.Components.Models     5
        � DataContext  5      0   *EWDesign.Components.Models.NavBarComponent  5   
   .��5      ��  �� Padding$ 10q�6   ��6   �� *BackgroundColor+._�5   
   �  � ColumnDefinitions 5      �� 5      $	��450 �6#   �� 5      .+�5      �� -   5        $
��24,0q�5      $��Center=�5       � Cursor$
 Hand��5      $��24(�5      $
��Bold"�5      ���#2a2e35��5!      �� *Title+y�5"      �� *	IsEditing+ $   InverseBoolToVisibilityConverter �� 	Converter# [  �� 5$      -   5        $
��24,0q�5%      $��Center=�5&      $
 Hand��5'      $��24(�5(      $
��Bold"�5)      ���#2a2e35��5-      �� *Title+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�y�5.      �� *	IsEditing+  BoolToVisibilityConverter# [ �� 51      $��1��6   ��6+   �� *NavbarElements+��52      �� 53      .R�54      �� $��0,0,24,0q�55      $��Right=�56       �� Orientation$ 
Horizontal=�57      58      ��5;      �� 5<      .R�5=      �� $
��16,0q�5>      $��Center=�5?      $
 Hand��5@      $��16(�5A      ���#3A3F47��5C      �� 5D      5F      5G      5H      5I      