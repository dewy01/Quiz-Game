   M S B A M L   `   `   `  ����H  DQuiz-Gui-Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Quiz_Gui_GameQuiz_Gui_Game  5           Quiz_Gui_Game.SelectedQuiz   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Quiz_Gui_Game     5        N� Title$  SelectedQuiz��5	      .��5      � ��5   
   �� 5       �� ImageSource$2 ,pack://application:,,,/Assets/background.png��6    � RowDefinitions 5   
   �� 5      $
��80px �6   �� 5      $��* �6   �� 5      $
��80px �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��	0,20,0,20q�6X   .+�5      �� -   5         ��Name$ QuizName��5      $��0��6&   $��Quiz: ��63      SubtitleStyle#��[  6D   5      �� 5   
   $��1��6   $��Center=�6"   $��Center=�6?   .+�5      �� -   5        $ 
TimerLabel��5      $��Center=�6(   $��Center=�6E   �� 5      -   5        $ QuestionLabel��5      $��Center=�6+   $��Center=�6H   #��[  6c   �� 5      -   5         ��Name$ OptionsStackPanel��5      $��Center=�64   $ Vertical=�6Q   $��10q�6h   5      �� 5   
   $��2��6   $��100px��6"   .+�5      �� -   5        $��
Zatwierdź��5        QuizButtonStyle#��[ 6D   �� 5       -   5         ��Name$ 
ExitButton��5    %   $��Wyjdź��6   #��[ 6R    J� Opacity$ 0Q�6{   {�.  6�   5!      5#      5$      