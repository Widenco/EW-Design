   M S B A M L   `   `   `  ����C  ?EWDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:EWDesign.Components.ViewsEWDesign.Components.Views  5      G(clr-namespace:EWDesign.Components.ModelsEWDesign.Components.Models  5      (    "EWDesign.Components.Views.BodyView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:EWDesign.Components.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ;models(clr-namespace:EWDesign.Components.Models     5      ��5      �� *BodyBackgroundColor+  � DataContext  5      .   (EWDesign.Components.Models.BodyComponent  5   
    � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowEffect   
BlurRadius   Opacity   ShadowDepth   Color��%c   &	          5   
   $ 10Q�5      $	 0.2Q�5      $ 2Q�5      $ Black��5      5      .��5      �� $
��Auto=�6   .��5   
   �� $
��1200��5      $��24,72,24,24q�5      $��Center=�5       .+�5#      �� $	��800��5$      $��0,0,0,96q�5%      $��Center=�5&      .+�5'      �� $��0,0,0,24q�5(      $��48(�5)      $
��Bold"�5*      
������� �� TextAlignment$ Center=�5-      $
��Wrap=�5.      ��5,      �� *HeroSectionText[0]+5.      �� 50      $��0,0,0,40q�51      $��20(�52      ���#DADADA$ Center=�55      $
��Wrap=�56      ��54      �� *HeroSectionText[1]+56      �� 58      $	��200��59      $
��14,8q�5:      $��Center=�5;      ���#6C63FF � Cursor$
 Hand��5?      $��18(�5@      $��SemiBold"�5A      
���������5=      Z� ��5>      �� *HeroSectionText[2]+��5C      �� 5D       �� 
TargetType" ��6    .�5E      �� 	 �� Property	 � ��
 �� Value
 �#6C63FF6;   �� 5F      	 � ��

 �����6;   �� 5G      	 � ��
 5H   "   �� 5I   &    �� 
TargetType" ��66   .R�5J   *   ��  �� Padding# z��5K   -   #��z��5L   -    �� CornerRadius$ 8��5M   -   ._�5N   .   �� $��Center=�6?   $��Center=�6\   5O   +   5P   '   5R       �� Triggers 5S      P� 5T   "    P� Property �@J�IsMouseOver P� Value . . �5U   &   �� 	 � ��
 �#837EFF6C   5V   #   5X      5Z      5[      � 5^      $��0,0,0,24q�5_      $��Center=�5`       � 
ItemHeight$
 Auto��5a       � 	ItemWidth$	 300��5b      .+�5d      �� $	��300��5e      $��12q�5f      $ 32q�5g      $ 16��5i       J� Effect# �   5j      ��5h      �� *FeatureBackgroundColor+._�5k      �� .+�5l      �� $��0,0,0,12q�5m      $��22(�5n      $��SemiBold"�5o      
���������5q      �� *FeatureSectionText[0]+�� 5r      $��16(�5s      ���#DADADA$
��Wrap=�5v      ��5u      �� *FeatureSectionText[1]+5v      5w      5x      �� 5{      $	��300��5|      $��12q�5}      $ 32q�5~      $ 16��5�      # �   5�      ��5      �� *FeatureBackgroundColor+._�5�      �� .+�5�      �� $��0,0,0,12q�5�      $��22(�5�      $��SemiBold"�5�      
���������5�      �� *FeatureSectionText[2]+�� 5�      $��16(�5�      ���#DADADA$
��Wrap=�5�      ��5�      �� *FeatureSectionText[3]+5�      5�      5�      �� 5�      $	��300��5�      $��12q�5�      $ 32q�5�      $ 16��5�      # �   5�      ��5�      �� *FeatureBackgroundColor+._�5�      �� .+�5�      �� $��0,0,0,12q�5�      $��22(�5�      $��SemiBold"�5�      
���������5�      �� *FeatureSectionText[4]+�� 5�      $��16(�5�      ���#DADADA$
��Wrap=�5�      ��5�      �� *FeatureSectionText[5]+5�      5�      5�      5�      5�      5�      5�      