   M S B A M L   `   `   `  ����E  AIntellibox, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullK4clr-namespace:FeserWard.Controls;assembly=IntelliboxFeserWard.Controls  5       His.HabitacionesUI,   &His.HabitacionesUI.frmDevolucionPedido   5   	   R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Cauto4clr-namespace:FeserWard.Controls;assembly=Intellibox    5      -   5        $��frmDevolucionPedido1��5   @     � Title$  Devolucion Pedido��6   $��724.372��6   $��929.907��60    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5         MyFocusVisual �� Property �� Value �� 
TargetType ��Name �� StrokeDashArray ��Name	 � ActualWidth
 � ActualHeight �� CornerRadius  ShadowStyle  InformButton �� 
TargetType �� 
StartPoint �� EndPoint � Color � Offset �� Padding ��Name T� X T� Y �� Triggers P� Property P� Value �� 
TargetName  backgroundBoton1��%�	  &	        &	 (    &	 h    7   7  &	 �    �� 5   
   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      � $	��3 2q�6   .+�5      �� $ r1��6(   $��1��64   
���   �$	 2 2R�6W   �� 5      $ border��6%   #��z	 65   #��z
 6[   $ 2��6�   $��1q�6�   5      5      5      5      �� 5   
   .�5      ��  � ��
 �����63   5      �� 5   
   " ��6%   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ 2q�6'   �� 5       � ��$ Verdana)�6+   �� 5       � ��$
 11px(�6)   �� 5       � ��$
 Bold"�6+   �� 5        �@�FocusVisualStyle8   61   �� 5!       � �� 5"      �� 5#      $	 0,0&�6*   $	 0,1&�6;   .��5$      � $ 	#FFFFD190��6'   $	 0.2Q�69   � 5%      $ Orange��6'   $
 0.85Q�66   � 5&      $ 	#FFFFD190��6'   $ 1Q�69   5'      5)      �� 5*       � �� 5+      �� 5,      " ��6&   .R�5-      �� $ border��6!   $��1q�5.      $	 4,2q�5/      
�������$ 3��51      #��z��52      ._�53      � .+�54   "   �� $ contentShadow��55   (   $��Center=�54   3   $��Center=�55      8�� z�57   &   T� 58   *   $	 1.0Q�6=   $	 1.0Q�6E   5:   #   �� 5;   "   $ content��5<   (   $��Center=�5;   3   $��Center=�5<      6(   5=      5>       5?      P� 5@       �@J�IsMouseOver . . �5A   "   �� $ border��6)    � �� �	#FF4788c86T   �� 5B   "    � �� �	#FF4788c86?   5C      P� 5D       � �� . . �5E   "   ��  � �� 5F   &   �� 5G   *   $	 0,0&�6>   $	 0,1&�6O   .��5H   .   � $ 	#FFFFD190��6;   $
 0.35Q�6M   � 5I   .   $ Orange��6;   $
 0.95Q�6J   � 5J   .   $ 	#FFFFD190��6;   $ 1Q�6M   5K   +   5M   #   �� 5N   "   $ content��6)    � z� 5O   &   T� 5P   *   $	 1.0Q�6=   5R   #   5S      P� 5T       �@��IsDefaulted . . �5U   "   �� $ border��6)    � �� �	#FF2828286T   5V      P� 5W       �@J�	IsFocused . . �5X   "   �� $ border��6)    � �� �	#FF2828286T   5Y      P� 5Z       � {� .  . �5[   "   �� $ border��6)    �@J�Opacity$	 0.7Q�6P   �� 5\   "    � ��
 �����6?   5]      5`      5b      5c      �� 5d   
   $ 0.5,1&�67   $ 0.5,0&�6H   .��5e      � $ 	#FFBEC4C4��6   $ 0.543Q�6-   � 5f      $ 	#FFDFE7E7��6   $ 1Q�6-   � 5g      $ 	#FFB2B4B4��6   5h      5j      .��5l      � .+�5m   
   �� ���	#FFF3F3F3$��45��6%   $��
10,10,12,0q�61    �� RadiusX$ 15��6E    �� RadiusY$ 15��6R   ���	#FF9F9F9F$��3��6r   $	��Top=�6�   �� 5n   
   $
��Bold"�6   ���	#FF151352$��23��69   $
��Left=�6E   $��	20,14,0,0q�6`     SimpleLabel#���  6s   $	��Top=�6�   $��Center=�6�   $��66��6�   .��6�   
Paciente :6�   �� 5o   
   $
��Bold"�6   $��23��6"   $
��Left=�6.   $��	20,32,0,0q�6I   #���  6\   $	��Top=�6�   $��Center=�6�   $��66��6�   .��6�   
Historia :6�   �� 5p   
   -   5        $��lblPaciente��5p   \   $
��Bold"�6   $��23��6"   $
��Left=�6.   $��	81,14,0,0q�6I   #���  6o   $	��Top=�6�   $��Center=�6�   $	��199��6�   .��6�   
Paciente :6�   �� 5q   
   -   5        $��lblHistoria��5q   \   $
��Bold"�6   $��23��6"   $
��Left=�6.   $��	81,32,0,0q�6I   #���  6o   $	��Top=�6�   $��Center=�6�   $	��199��6�   .��6�   
Paciente :6�   �� 5r   
   $
��Bold"�6   $��23��6"   $��Right=�6.   $��
0,32,217,0q�6J   #���  6^   $	��Top=�6�   $��Center=�6�   $��66��6�   .��6�   	Atencion:6�   �� 5s   
   $
��Bold"�6   $��23��6"   $��Right=�6.   $��
0,14,232,0q�6J   #���  6^   $	��Top=�6�   $��Center=�6�   $��51��6�   .��6�   
Médico:6�   �� 5t   
   -   5        $��	lblMedico��5t   ]   $
��Bold"�6   $��23��6"   $��Right=�6.   $��	0,14,60,0q�6J   #���  6n   $	��Top=�6�   $��Center=�6�   $	��166��6�   .��6�   
Paciente :6�   �� 5u   
   -   5        $��lblAtencion��5u   ]   $
��Bold"�6   $��23��6"   $��Right=�6.   $��	0,32,50,0q�6J   #���  6p   $	��Top=�6�   $��Center=�6�   $	��176��6�   .��6�   
Paciente :6�   �� 5w   
   ���	#FFF3F3F3$��12,61,10,315q�6%   $ 15��6;   $ 15��6H   ���	#FF9F9F9F$��3��6h   s oInfragisticsWPF3.DataPresenter.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb9  3Infragistics.Windows.DataPresenter.XamDataPresenter  5x   
   0igDP%http://infragistics.com/DataPresenter  6�   -   5        $*��$xamDataPresenterProductosSolicitados��5x   L   
�������$��31,0,31,370.478q�63   :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ Office2k7Blue 6x   $��221.42��6�   $��Bottom=�6�      FieldLayouts 5y      4  .Infragistics.Windows.DataPresenter.FieldLayout  5z      
   Key$ ProductosLayout��6#      Fields. 5{      .  (Infragistics.Windows.DataPresenter.Field     Label$  CODIGO ��6!       Name$  
PDD_CODIGO��62   !   
Visibility$! Hidden=�6D   "   Width$" 0X�6X     5|      $  CODIGO P. ��6!   $  
PRO_CODIGO��65   $" 70X�6H     5}      $  DESCRIPCION��6!   $  PRO_DESCRIPCION��66   $	" 250X�6M     5~      $ 
 CANTIDAD ��6!   $  PDD_CANTIDAD��64   $" 75X�6H     5      $  CANTIDAD DEV ��6!   $  CantidadDevuelta��68   $" 75X�6P     5�      $  VALOR ��6!   $  	PDD_VALOR��61   $" 75X�6B     5�      $  IVA ��6!   $  PDD_IVA��6/   $" 75X�6>     5�      $  TOTAL ��6!   $  	PDD_TOTAL��61   $" 75X�6B     5�      $  TOTAL ��6!   $  
PED_CODIGO��61   $! Hidden=�6C   $" 0X�6W     5�      $  TOTAL ��6!   $  
PDD_ESTADO��61   $! Hidden=�6C   $" 0X�6W     5�      $  TOTAL ��6!   $  	PDD_COSTO��61   $! Hidden=�6B   $" 0X�6V     5�      $  TOTAL ��6!   $  PDD_FACTURA��61   $! Hidden=�6D   $" 0X�6X     5�      $  TOTAL ��6!   $  PDD_ESTADO_FACTURA��61   $! Hidden=�6K   $" 0X�6_     5�      $  TOTAL ��6!   $  PDD_FECHA_FACTURA��61   $! Hidden=�6J   $" 70X�6^     5�      $  TOTAL ��6!   $  PDD_RESULTADO��61   $! Hidden=�6F   $" 0X�6Z     5�      $  TOTAL ��6!   $  PRO_CODIGO_BARRAS��61   $! Hidden=�6J   $" 0X�6^   5�      #   FieldSettings# 5�      6  0Infragistics.Windows.DataPresenter.FieldSettings  5�      $   	AllowEdite aInfragisticsWPF3.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$$ False 6%   %   CellClickAction$% 
SelectCell=�67   !&   FilterOperatorDefaultValue�  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.Controls.ComparisonOperator, InfragisticsWPF3.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$& Contains 6T   5�      �� 5�   
   ���	#FFF3F3F3$��
12,0,10,10q�6%   $ 15��69   $ 15��6F   ���	#FF9F9F9F$��3��6f   $��296.857��6z   $��Bottom=�6�     5�   
   0igDP%http://infragistics.com/DataPresenter  6�   -   5        $��xamDataPresenter1��5�   T   
�������$��32.003,0,29.997,116.328q�63   $ Office2k7Blue 6m   $��138.091��6�   $��Bottom=�6�    5�        5�      $ ProductosLayout��6#   . 5�        $  CODIGO ��6!   $  	DevCodigo��62   $! Hidden=�6C   $" 0X�6W     5�      $  CODIGO P. ��6!   $  
PRO_CODIGO��65   $" 70X�6H     5�      $  DESCRIPCION��6!   $  PRO_DESCRIPCION��66   $	" 250X�6M     5�      $ 
 CANTIDAD ��6!   $  DevDetCantidad��64   $" 75X�6J     5�      $  VALOR ��6!   $  DevDetValor��61   $" 75X�6D     5�      $  IVA ��6!   $  	DevDetIva��6/   $" 75X�6@     5�      $  TOTAL ��6!   $  DevDetIvaTotal��61   $" 75X�6G     5�      $ 
 NDETALLE ��6!   $  
PDD_CODIGO��64   $! Hidden=�6F   $" 0X�6Z   5�      # 5�        5�      $$ False 6%   $% 
SelectCell=�67   $& Contains 6T   5�      �� 5�   
   -   5        $��lblCantidadAdd��5�   Q   $��43��6   $��Right=�6   $��0,0,147.763,315q�68   ' J� RenderTransformOrigin$' 0.36,-0.719&�6g   #���  6�   $��Bottom=�6�   $��Center=�6�   $��62.216��6�   .��6�   
Cantidad :6	  m	 iInfragisticsWPF3.Editors.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0	 	 *Infragistics.Windows.Editors.XamTextEditor	  5�   
   /	igEditorshttp://infragistics.com/Editors 	 6�   -	   5        $��txtCantidad��5�   �   .
 	 (Infragistics.Windows.Editors.ValueEditor( 
  Format$	( 000��6"   $��19.998��6/   $��Right=�6?   $��Right=�6[   $��0,0,115,329.191q�6~   $��4��6�   ) 
  ThemeD 	 >Infragistics.Windows.Themes.Internal.EditorsThemeTypeConverter$) Office2k7Silver 6�   $��Bottom=�6�   $��36.663��6�   * 
  Value.* 6G  16J  �� 5�   
   -
   5        $��btnAdd��5�   �   #���  6   
�������$��Right=�6Y   $��0,0,29.997,329.191q�6u   + J� OpacityMask
+ �� ��$' 0.492,1.115&�6�   #��[ 6�   $��7��6�   $��78.881��6  $��20.052��6  $��Bottom=�6+  , � BindingGroup, 5�      &   System.Windows.Data.BindingGroup  5�      -   NotifyOnValidationError- .  .   Name. 6   Z� 6/   .��5�   #   	Agregar5�      �� 5�   
   -   5        $��	lblPedido��5�   �   $
��Bold"�6   ���	#FF151352$��30��69   $��293.304,66.66,288.86,0q�6E   #���  6e   $	��Top=�6�   $��Center=�6�   $��Center=�6�   $��13(�6�   .��6  
PEDIDO No.6  �� 5�   
   -   5        $��label1��5�   h   $
��Bold"�6   ���	#FF151352$��23��69   $��281.083,0,301.081,261.085q�6E   #���  6v   $��Bottom=�6�   $��Center=�6�   $��Center=�6�   $��13(�6�   .��6  
DEVOLUCION6  �� 5�   
   -   5        $��
btnGuardar��5�   �   #���  6   
�������$��20.052��6Y   $��Right=�6i   $��0,0,114.433,27.775q�6�   
+ �� ��$' 0.492,1.115&�6�   #��[ 6�   $��7��6  $��Bottom=�6   $��78.881��6;  , 5�        5�      - .  . 6   Z� 6/   .��5�   #   	Guardar5�      �� 5�   
   -   5        $��btnCancelar��5�   �   #���  6   
�������$��21.163��6Y   $��Right=�6i   $��0,0,29.997,26.664q�6�   
+ �� ��$' 0.492,1.115&�6�   #��[ 6�   $��7��6  $��Bottom=�6   $��78.881��6;  , 5�        5�      - .  . 6   Z� 6/   .��5�   #   
Cancelar5�      �� 5�   
   -   5        / ��Name$/ txtobservacion��5�      $
��Left=�6*   $��47.545��6E   $��115.37,593,0,0q�6U   0 �� TextWrapping$
0 Wrap=�6m   $	��Top=�6�   $��775.537��6�   $��1q�6�   �� 5�   
   -   5        1 ��Name$1 label��5�      $��Observación:��6   $
��Left=�66   $��32.003,593,0,0q�6Q   $	��Top=�6i   $' -0.395,0&�6�   5�      5�      