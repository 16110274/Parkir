   M S B A M L   `   `   `  ���� H  Dparkiransmart, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:parkiransmart.Viewparkiransmart.View      parkiransmart.View.Exit   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:parkiransmart.View       � Title$
  Exit��$	��800��$
��1380��.��� .+��� $
��Left=�$	��769��$	��Top=�$
��1372�����	#FF297875.+��� $	��681��
 �� Top$ 34��$
��1372��
�������.+��� $��1��$ 98��$
��1372�����	#FF297875�� -    ��Name$ button1��$��Bayar�� �� Left$	 666��$ 28��$	��176��$��47�����	#FF66DEDA
�������
��������� -   $ button2��$��Control Menu��$	 899��$ 28��$	��176��$��47�����	#FF297875
�������
��������� -   $ btnMonitoring��$��Monitoring Parkiran��$
 1131��$ 28��$	��176��$��47�����	#FF297875
�������
��������� -   $ button4��$��Input��$	 437��$ 28��$	��176��$��47�����	#FF297875
�������
��������� -    ��Name$ label1��$7��12017 @ Copyright VViVu Corp. All Rights Reserved.��$ 19��$	 691��
�������
������ �� -   $ label2��$*��$Designed and Developed by VViVu Corp��$
 1131��$	 691��
��������� -   $ lblPlat��$��No Plat          :��$	 194��$ 19��$��24(��� -   $ 	lblLokasi��$��Lokasi           :��$	 254��$ 19��$��24(��� -	   $ lblJamM��$��Masuk           :��$	 314��$ 19��$��24(��� -
   $ lblJamK��$��Keluar           :��$	 374��$ 19��$��24(��� -   $ lblTotal��$��Total             :��$	 432��$ 23�� J� RenderTransformOrigin$ 
0.63,0.577&�$��24(��� -   $ lblNmPet��$��Petugas        :��$	 492��$ 23��$��24(��� -   $ 	btnProses��$��Proses��$	��176��$
 1131��$	 614��$��47�����	#FFDDDDDD
���   �
���   �$��20(��� -    ��Name$ txtPlatNoIsi��$��35��$	 225�� �� TextWrapping$
 Wrap=�$�� ��$	 194��$	��316��$��24(��� -   $ 	txtLokasi��$��35��$	 225��$
 Wrap=�$�� ��$	 254��$	��316��$��24(��� -   $ txtTotalIsi��$��42��$	 225��$
 Wrap=�$�� ��$	 432��$	��316��$��24(��� -   $ 	txtPetIsi��$��42��$	 225��$
 Wrap=�$�� ��$	 492��$	��316��$��24(��� -   $ lblKIsi��$�� ��$	 225��$	 369��$��24(�$	��136��$��47���� -   $ lblMIsi��$�� ��$	 225��$	 313��$	��136��$��24(�$��43���� -   $ btnCek��$��Check��$ 23��$	 614��$	��176��$��47�����	#FFDDDDDD
���   �
���   �$��20(��� -   $ 	lblNoStat��$��No Status      :��$ 19��$	 133��$��24(��� -   $ 	txtNoStat��$��35��$	 225��$
 Wrap=�$�� ��$	 140��$	��316��$��24(�&   System.Windows.Controls.DataGrid  -     Name$ dgvExit��$��0��$��0��$
 1207��$	 485���� -   $ lblKIsi1��$�� ��$	 361��$	 370��$��24(�$	��180��$��46���� -   $ lblMIsi1��$�� ��$	 361��$	 312��$	��171��$��24(�$��44���� -   $ lblIdKar��$��ID                  :��$	 543��$ 21��$��24(��� -   $ txtID��$��42��$	 225��$
 Wrap=�$�� ��$	 543��$	��316��$��24(��� -   $ label��
�������$.��(Smart Parkiran - Hartono Mall Yogyakarta��$ 10��$ 3���� -   	 ��Name$	 image��$��Center=�$	��Top=�$	��316��$��/Img/logo.png��$��26,56,1030,0q�$��85��$ 0.5,0.5&�
 J� OpacityMask
 ��  �� ImageSource$ /Img/logo.png���� -   $ btnCobakeluar��$��Logout��$
 1287��$��75��$ 5��
���  ��
��������� -   $��lblTime��$��18(�
�������$��Center=�$	��Top=�