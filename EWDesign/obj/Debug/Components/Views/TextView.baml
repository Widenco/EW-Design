   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      G(clr-namespace:EWDesign.Components.ModelsEWDesign.Components.Models  5      (    "EWDesign.Components.Views.TextView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EWDesign.Components.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :model(clr-namespace:EWDesign.Components.Models     5        � DataContext  5      .   (EWDesign.Components.Models.TextComponent  5   
   .��5      � .+�5   
   �� -   5        $
��24,0q�5      $��Center=�5       � Cursor$
 Hand��5      $��24(�5      $
��Bold"�5      ��5      �� *
ForeGround+��5      �� *Text+y�5      �� *	IsEditing+ $   InverseBoolToVisibilityConverter �� 	Converter# [  �� 5   
   -   5        $
��24,0q�5      $��Center=�5      $
 Hand��5      $��24(�5       $
��Bold"�5!      ��5"      �� *
ForeGround+��5%      �� *Text+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�y�5&      �� *	IsEditing+  BoolToVisibilityConverter# [ 5(      5)      