   M S B A M L   `   `   `  ����H  Dparkiransmart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:parkiransmart.Viewparkiransmart.View  5   	        parkiransmart.View.Monitor   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:parkiransmart.View     5   	     � Title$  Monitor��5   	   $	��800��6   $
��1380��6&   .��5	      � .+�5
   
   �� $
��Left=�6   $	��769��6,   $	��Top=�69   $
��1372��6Q   ���	#FF297875.+�5      �� $	��681��6   
 �� Top$ 33��6"   $
��1372��62   
�������.+�5      �� $��1��6   $ 98��6$   $
��1372��64   ���	#FF2978756A   �� 5      -   5         ��Name$ btnBayar��5      $��Bayar��6+    �� Left$	 666��6;   $ 28��6M   $	��176��6]   $��47��6i   ���	#FF297875
�������
�������6�   �� 5      -   5        $ 
btnControl��5      $��Control Menu��6-   $	 899��6D   $ 28��6V   $	��176��6f   $��47��6r   ���	#FF297875
�������
�������6�   �� 5      -   5        $ btnMonitoring��5      $��Monitoring Parkiran��60   $
 1131��6N   $ 28��6a   $	��176��6q   $��47��6}   ���	#FF33D3CD
�������
�������6�   �� 5      -   5        $ btnInput��5      $��Input��6+   $	 437��6;   $ 28��6M   $	��176��6]   $��47��6i   ���	#FF297875
�������
�������6�   �� 5      -   5         ��Name$ label1��5      $7��12017 @ Copyright VViVu Corp. All Rights Reserved.��6(   $ 19��6d   $	 691��6u   
�������
������ 6�   �� 5      -   5        $ label2��5      $*��$Designed and Developed by VViVu Corp��6(   $
 1131��6W   $	 691��6j   
�������6{   &   System.Windows.Controls.DataGrid  5      -   5          Name$ 	dgMonitor��5      $	��439��6.   $	 160��6;   $	 150��6M   $
��1100��6^   �� 5      $��Query SQL :  ��6   $	 160��60   $	 594��6B   �� 5      -   5        $ buttonExecute��5      $��Execute��60   $	 396��6B   $	 651��6T   $	��638��6e   6q   �� 5      $��SELECT��6   $	 244��6)   $	 594��6;   �� 5      -	   5         ��Name$ richtextboxQuery��5      $��52��68   $	 297��6D   $	 594��6V   $	��831��6g    �� 
LineHeight$ 5��6s   6�   5      �� 5      -
   5        $ label��5      
�������$.��(Smart Parkiran - Hartono Mall Yogyakarta��66   $ 10��6i   $ 3��6z   �� 5      -   5         ��Name$ image��5      $��Center=�6#   $	��Top=�6@   $	��316��6X   $��/Img/logo.png��6d   $��26,56,1030,0q�6{   $��85��6�   	 J� RenderTransformOrigin$	 0.5,0.5&�6�   
 J� OpacityMask
 5      �� 5       �� ImageSource$ /Img/logo.png��6!   5       �� 5!      -   5        $ btnCobakeluar��5!      $��Logout��6,   $
 1287��6=   $��75��6P   $ 5��6[   
���  ��
�������6�   5#      �� 5$   
   -   5        $��lblTime��5$      $��18(�6   
�������$��Center=�6@   $	��Top=�6]   5%      5&      