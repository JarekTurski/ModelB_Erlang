   M S B A M L   `   `   `  ����P  LProjektAplikacjaTRwST, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ModelB_ErlangModelB_Erlang  5   	       ModelB_Erlang.HelpWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ModelB_Erlang     5   	     � Title$  
HelpWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   9� $0��*Do czego służy program ModelErlanga.exe?��6   $��1��6E   $��10,0,0,0q�6R   .��5      �� $
��Wrap=�6   ��Program służy do wyliczania wartości jednego z parametrów - prawdopodobieństwa blokady(P), średniej wartości ruchu(A) lub liczby serwerów(N) - na podstawie modelu Erlanga. Umożliwia również rysowanie wykresów przedstawiających zależności:�� 5   r   nl1) liczby serwerów od średniej wartości ruchu, dla wybranego prawdopodobieństwa blokady -> N(P=const, A)�� 5      mk2) prawdopodobieństwa blokady od liczby serwerów, dla wybranej średniej wartości ruchu -> P(A=const, N)5      5      9� 5   
   $-��'Jak używać programu ModelErlanga.exe?��6   $��2��6B   $��10,0,0,0q�6O   .��5      �� $
��Wrap=�6   ��Aby obliczyć wartość jednego z trzech parametrów należy wpisać wartości pozostałych dwóch i nacisnąć przycisk "Licz". Aby narysować wykres należy zaznaczyć pole "Zakres" przy wybranym parametrze i wpisać wartość początkową w pierwszym polu, a końcową w drugim oraz wpisać wartość drugiego parametru niezbędnego do narysowania wykresu i nacisnąć przycisk "Wykres".5       5!      9� 5"   
   $&�� W jakim formacie wpisywać dane?��6   $��3��6<   $��10,0,0,0q�6I   .��5#      �� $
��Wrap=�6   RPWszystkie wartości zmiennoprzecinkowe należy wpisywać używając przecinków.5%      5&      5)      5*      