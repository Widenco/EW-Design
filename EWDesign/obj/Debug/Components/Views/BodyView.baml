   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      G(clr-namespace:EWDesign.Components.ModelsEWDesign.Components.Models  5      (    "EWDesign.Components.Views.BodyView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EWDesign.Components.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ;models(clr-namespace:EWDesign.Components.Models     5      ��5      �� *BodyBackgroundColor+  � DataContext  5      .   (EWDesign.Components.Models.BodyComponent  5   
    � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowEffect   
BlurRadius   Opacity   ShadowDepth   Color��%c   &	          5   
   $ 10Q�5      $	 0.2Q�5      $ 2Q�5      $ Black��5      5       � ContextMenu 5      �� 5   
   .@�5      m� -   5        $
��Edit��5      6%   m� 5      -   5        $��Remove��5      6'   5      .��5"      �� $
��Auto=�6   .��5#   
   �� $
��1200��5$      $��24,72,24,24q�5%      $��Center=�5&      .+�5)      �� -   5         ��Name$ HeroSectionDropArea��5*      $	��800��5+      $��0,0,0,96q�5,      $��Center=�5-      � 50      -   5         �Name$ FeatureSectionDropArea��51      $��0,0,0,24q�52      $��Center=�53      	 � 
ItemHeight$
	 Auto��54      
 � 	ItemWidth$	
 300��55      5z      5|      5}      5~      