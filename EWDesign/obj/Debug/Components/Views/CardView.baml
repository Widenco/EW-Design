   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      G(clr-namespace:EWDesign.Components.ModelsEWDesign.Components.Models  5      (    "EWDesign.Components.Views.CardView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EWDesign.Components.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :model(clr-namespace:EWDesign.Components.Models     5      -   5          � DataContext  5      .   (EWDesign.Components.Models.CardComponent  5   
    � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowEffect   
BlurRadius   Opacity   ShadowDepth   Color��%c   &	          5   
   $ 10Q�5      $	 0.2Q�5      $ 2Q�5      $ Black��5      5       � ContextMenu 5      �� 5   
   .@�5      m� -   5        $
��Edit��5   *   m� 5      -   5        $��Remove��5   ,   5      .��5!      �� $��12q�5$   	    �� Padding$ 32q�5%   	    �� CornerRadius$ 16��5'   	   	 J� Effect#	 �   5(   	     SelectedComponent#��[ 5)   	   ��5"   	   �� *Width+��5#   	   �� *Height+��5&   	   �� *
Background+._�5*   
   �� -   5        
 ��Name$

 Card��5*      5+      5,      