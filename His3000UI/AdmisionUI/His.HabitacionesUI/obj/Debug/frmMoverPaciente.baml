   M S B A M L   `   `   `  ����  His.HabitacionesUI)    #His.HabitacionesUI.frmMoverPaciente   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      m iInfragisticsWPF3.Editors.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  5      :d2http://schemas.microsoft.com/expression/blend/2008  64   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6q   -   5          �Name$  Window��5       � Title$ Cambio de habitación��5      $	��347��5      $	��197��6      MiltonFondo1#���   6    � WindowStartupLocation$ CenterScreen=�6^    � WindowStyle$ 
ToolWindow=�6�    � 	Resources 5	       �� EndPoint � MappingMode �� 
StartPoint � Color	 � Offset��%�   &	        �� 5
      $ 0.5,1&�6-   $ RelativeToBoundingBox=�6>   $ 0.5,0&�6b   .��5      � $ 	#FFD6D6D6��6   $	 0.151Q�6$   � 5      $ 	#FFE9E9E9��6   $	 1Q�6$   � 5      $ 	#FF918D8D��6   $	 0.458Q�6$   5      5      .��5      � -   5        
 �Name$
 
LayoutRoot��5      .+�5   
   1  +Infragistics.Windows.Editors.XamComboEditor  -   5          Name$ 
xamCboPiso��5   9   $��100,20,124,0q�6#   $��21��6M   $	��Top=�6Y   .  (Infragistics.Windows.Editors.ValueEditor   ThemeD  >Infragistics.Windows.Themes.Internal.EditorsThemeTypeConverter$
 Onyx 6q   6     5   
   -   5        $ xamCboHabitaciones��5   9   $��100,47,124,0q�6#   $��20.558��6U   $	��Top=�6e   �� 5   
   -   5         ��Name$ blbPiso��5   J   $��23��6   $
��Left=�6   $��	18,20,0,0q�67   $	��Top=�6[   $��82��6s   $��Piso (Nivel)��6~   �� 5   
   -   5        $ lblNumHabitacion��5   >   $
��Left=�6   $��	18,45,0,0q�6+   $��82��6X   $��N° Habitación��6c   $��27��6{   $	��Top=�6�   �� 5   
   -   5        $ lblObservaciones��5   #   $��	18,77,0,0q�6   $��Observaciones��6=   $
��Left=�6U   $��82��6p   $��22��6{   $	��Top=�6�   �� 5   
   -   5        $��
btnAceptar��5   [   $��Aceptar��6   $��22��6#   $��
67,142,0,0q�6/   $	��Top=�6C   $
��Left=�6m   $��90��6�   ��5      �� 5      $ 0.5,1&�6    $ 0.5,0&�61   .��5      � $ White��6   $	 0Q�6(   � 5      $ 	#FF4E79BE��6   $		 0.9Q�6,   5      5      �� 5   
   -	   5        $��btnCancelar��5   �   $��Cancelar��6   $
��Left=�6$   $��22��6?   $��183,142,0,0q�6K   $	��Top=�6`   $��92��6x   ��5       �� 5!      $ 0.5,1&�6    $ 0.5,0&�61   .��5"      � $ White��6   $	 0Q�6(   � 5#      $ 	#FF4E79BE��6   $		 0.9Q�6,   5$      5&      �� 5'   
   -
   5         ��Name$ txtObservacion��5'      $��100,77,25,45q�6*    �� TextWrapping$
 Wrap=�6@   5(      5)      