   M S B A M L   `   `   `  ����  His.HabitacionesUI*    $His.HabitacionesUI.frmCuentaPaciente   5      R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    6B   s oInfragisticsWPF3.DataPresenter.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb0igDP%http://infragistics.com/DataPresenter  6�   m iInfragisticsWPF3.Editors.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb/	igEditorshttp://infragistics.com/Editors  6�     � Title$  frmCuentaPaciente��5      $	��522��6   $	��712��6,   .��5      �    backGroundVentana1#���   6   .+�5      �� $��24,22,20.624,0q�6   ���	#FF9F9F9F �� RadiusX$ 15��69    �� RadiusY$ 15��6F   $	��175��6S   $	��Top=�6`   $��3��6x   ��5      �� 5	       �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6+   .��5
      �  � Color$ 	#FFEFEFEF��6    � Offset$ 0.953Q�6&   � 5      $ 	#FFD0E3FB��6   $ 1Q�6&   5      5      9  3Infragistics.Windows.DataPresenter.XamDataPresenter  5      -   5          Name$ xamDataPresenterProductos��5   6   $��24,217,20.624,24.5q�6   :  4Infragistics.Windows.DataPresenter.DataPresenterBase   ThemeJ  DInfragistics.Windows.Themes.Internal.DataPresenterThemeTypeConverter$ Office2k7Blue 6Y   
�������	   FieldLayoutSettings	 5      <  6Infragistics.Windows.DataPresenter.FieldLayoutSettings  5      
   AllowDelete$
 FalseY�6      FieldLayouts 5      4  .Infragistics.Windows.DataPresenter.FieldLayout  5      
   Key$ ProductosLayout��6      Fields. 5      .  (Infragistics.Windows.DataPresenter.Field     Name$ 
PRO_CODIGO��6      Label$ 	 Código ��6%      Width$ 50X�66     5      $ PRO_NOMBRE_GENERICO��6   $  Nombre genérico��6.   $	 100X�6G     5      $ PRO_NOMBRE_COMERCIAL��6   $  Nombre comercial ��6/   $	 100X�6J     5      $ PRO_CANTIDAD��6   $ 
 Cantidad ��6'   $ 40X�6:     5      $ PRO_DESCRIPCION��6   $  Descripción ��6*   $	 150X�6@     5      $ PRO_CONC��6   $  Concentración ��6#   $ 70X�6;     5      $ PRO_FF��6   $  Forma farmacéutica ��6!   $	 100X�6>     5      $ PRO_PRESENTACION��6   $  Presentación ��6+   $	 100X�6B     5      $ PRO_VIA_ADMINISTRACION��6   $  Vía administración ��61   $ 70X�6N     5      $ PRO_REFERENCIA��6   $  Referencia ��6)   $ 70X�6>     5       $ PRO_ACCION_TERAPEUTICA��6   $  Acción terapéutica ��61   $ 70X�6N     5!      $ PRO_OBSERVACION��6   $  Observación ��6*   $	 120X�6@     5"      $ 
PRO_PRECIO��6   $  Precio ��6%   $ 60X�66     5#      $ PRO_IVA��6   $  Iva ��6"   $ 60X�60     5$      $ PRO_CODIGO_BARRAS��6      
Visibility$ 	Collapsed=�6+     5%      $ 
PRO_ESTADO��6   $ 	Collapsed=�6$   5&         FieldSettings 5(      6  0Infragistics.Windows.DataPresenter.FieldSettings  5)         	AllowEdite aInfragisticsWPF3.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb�  �Infragistics.Windows.Helpers.NullableConverter`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ False 6      AllowRecordFiltering$
 True 6+      CellClickAction$ 
SelectCell=�6G   !   FilterOperatorDefaultValue�	  �Infragistics.Windows.Helpers.NullableConverter`1[[Infragistics.Windows.Controls.ComparisonOperator, InfragisticsWPF3.v11.2, Version=11.2.20112.1012, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb]]$ Contains	 6d   5+      �� 5,      $��20��6
   $��274.086,41,290,0q�6     SimpleLabel#���  60   $	��Top=�6V   $��12(�6n   $��Datos del Paciente��6|   $
��Bold"�6�   ���	#FF151352 J� RenderTransformOrigin$ 0.498,-0.15&�6�   �� 5-      $��20��6
   $��101.586,69,0,0q�6   #���  6.   $	��Top=�6T   $��12(�6l   $��Nombres:��6z   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $
��Left=�6�   $��119.414��6�   �� 5.      $��20��6
   $��101.758,169,0,0q�6   #���  6/   $	��Top=�6U   $��12(�6m   $��Datos de Paciente��6{   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $
��Left=�6�   $��119.414��6�   �� 5/      $��20��6
   $��101.758,143,0,0q�6   #���  6/   $	��Top=�6U   $��12(�6m   $��	Telefono:��6{   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $
��Left=�6�   $��119.414��6�   �� 50      $��20��6
   $��0,115,146,0q�6   #���  6+   $	��Top=�6Q   $��12(�6i   $��Edad:��6w   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $��Right=�6�   $��82��6�   �� 51      $��20��6
   $��101.586,117,0,0q�6   #���  6/   $	��Top=�6U   $��12(�6m   $��Sexo:��6{   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $
��Left=�6�   $��119.414��6�   �� 52      $��20��6
   $��101.586,93,0,0q�6   #���  6.   $	��Top=�6T   $��12(�6l   $��Dirección:��6z   $
��Bold"�6�   ���	#FF151352$ 0.498,-0.15&�6�   $
��Left=�6�   $��119.414��6�   0
  *Infragistics.Windows.Editors.XamTextEditor
  54      -   5         
 Name$ 	txtBuscar��54   =   $��16��6   $��221,69,65,0q�6(   $	��Top=�6P   .  (Infragistics.Windows.Editors.ValueEditor   ThemeD  >Infragistics.Windows.Themes.Internal.EditorsThemeTypeConverter$ Office2k7Silver 6h   
�������6�   
  55      -   5        $ txtBuscar_Copy��55   =   $��16��6   $��221,93,65,0q�6(   $	��Top=�6U   $ Office2k7Silver 6m   
�������6�   
  56      -   5        $ txtBuscar_Copy1��56   =   $��16��6   $��221,117,0,0q�6(   $	��Top=�6V   $ Office2k7Silver 6n   
�������$
��Left=�6�   $	��120��6�   
  57      -   5        $ txtBuscar_Copy2��57   <   $��16��6   $��
0,115,65,0q�6(   $	��Top=�6U   $ Office2k7Silver 6m   
�������$��Right=�6�   $	��120��6�   
  58      -   5        $ txtBuscar_Copy3��58   C   $��16��6   $��221,139,329.166,0q�6(   $	��Top=�6\   $ Office2k7Silver 6t   
�������6�   59      5:      