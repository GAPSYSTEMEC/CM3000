   M S B A M L   `   `   `  ����   AccesoUI    AccesoUI.login   R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml    >d2http://schemas.microsoft.com/expression/blend/2008    Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    -     � Title$  login��$��135.456��$��277.68�� � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=� J� SnapsToDevicePixels .  � WindowStyle$
 None=� � 	Resources    MyButtonStyle �� 
TargetType �� Property �� Value	 �� EndPoint
 �� 
StartPoint � Color � Offset  OnMouseEnter1��%�   &	        &	 �     �� " ��.���  � �� �� $	 0.5,1&�$
 0.5,0&�.��� $ 	#FF565656��$ 0Q�� $ White��$ 1Q���  � ��# � ���� ���� $	 1.034,0.483&�$
 -0.084,0.482&�.��� $ 	#FFE2E2E2��$ 1Q�� $ 	Gainsboro��$ 0.091Q�� $ 	#FEBAB8B8��$ 0.185Q�� $ 	#FEDCDCDC��$ 0.263Q�� $ 	#FEBAB8B8��$ 0.354Q�� $ 	#FED6D6D6��$ 0.451Q�� $ 	#FEBAB8B8��$ 0.543Q�� $ 	#FEDADADA��$ 0.638Q�� $ 	#FEBAB8B8��$ 0.731Q�� $ 	#FED6D6D6��$ 0.823Q�� $ 	#FEBAB8B8��$ 0.918Q�� $ 	#FFBEBEBE��.��� -   $��marco������ $	 0.5,1&�$
 0.5,0&�.��� $ 	#FF111647��$ 0.026Q�� $ 	#FF14B2FF��$ 1Q� � ColumnDefinitions �� $��7* ��� $��9* ��� $
��273* �.+��� -    ��Name$ 
txtUsuario��$!��96.118,43.277,52.255,67.073q�$��13.333(�$��1��$��2���� -    ��Name$ 
btnAceptar��$��22��$��51.271,0,143.271,4.52q�$��Bottom=�$��13.333(�$��3��$��Aceptar�� J� RenderTransformOrigin$ 0.521,1.182&�$��2������ $	 0.5,1&�$
 0.5,0&�.��� $ 	#FF939393��$ 0Q�� $ White��$ 1Q��� -   $ btnSalir��$��22��$��148.044,0,0,4.52q�$��Bottom=�$��13.333(�$��Salir��$��2��$
��Left=�$��77.875������ $	 0.5,1&�$
 0.5,0&�.��� $ 	#FF939393��$ 0Q�� $ White��$ 1Q��� -    ��Name$ lblUser��$��34.544,43.4,0,0q�$��13.333(�$��Usuario��$��24��$	��Top=�$
��Left=�$��59.739��$��2��$ 0.5,0.5&�$��Impact)����	#FFB2B2B4z�V� .~��� �� ��  �� Angle$ 0.939Q�T� �� -   $ lblPwd��$��33.479,69.956,0,0q�$��13.333(�$��Clave��$��26��$	��Top=�$
��Left=�$��62.64��$ -2.049,0.429&�$��2��$��Impact)����	#FFB2B2B4G� -    G�Name$ txtClave��$��96.119,72.573,52.249,0q�$��2��$��23.383��$	��Top=�$��2���� $��35.401��$��76.197,2.344,75.109,0q�$��His 3000.png��$
��Fill=�$	��Top=� J� Opacity$	 0.5Q�$��2��$ 0.5,0.5&� J� OpacityMask 
� .��� $ 	#FF52684C��$ 0Q�� $ 	#FF185507��$ 1Q�z�V� .~��� �� �� $ 0.243Q�T�  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  �� -	    ��Name$ 
lblEmpresa��$"��-31.476,56.025,214.411,48.41q�$
��Wrap=�$��EMPRESA��$��94.745��$ 0.471,-0.337&�$��13.333(�$
��Bold"� �� TextAlignment$ Center=�$��Center=�$��Center=�$��3��$��39.903������ $	 0.5,1&� � MappingMode$ RelativeToBoundingBox=�$
 0.5,0&�.��� $ 	#FFBABABA��$ 0Q�� $ 	#FFD0D0D0��$ 1Q�z�V� .~��� ��  �� AngleX$
 1.02Q��� $ -90.589Q�T�  T� X$ -26.894Q� T� Y$ 32.128Q��� -
    ��Name$ IconoPruebas��$��2��$��Right=�$��32��$��0,39,15.25,0q�$��imagenes/Tools.png��$
��Fill=�$	��Top=�$��32���� -   $ cambioClave��$��21��$	��Top=�$��imagenes/Security.png��$ 1.365,0.227&�$��221.539,74.956,19.768,0q�$��2��