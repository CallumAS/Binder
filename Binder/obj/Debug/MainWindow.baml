   M S B A M L   `   `   `  ����A  =Binder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BinderBinder  5   	       Binder.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Z VDevExpress.Mvvm.UI, Version=20.2.0.0, Culture=neutral, PublicKeyToken=382ad28f6ae31f30F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Binder     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Binder��5   	   $	��450��6   $	��800��6%   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $
��100* �6   .+�5   
   �� ��5      �� 5      $��CONTROLS��6   $
��Bold"�6,   
�������$��Center=�6Q   $��
Montserrat)�6n   .��5      �� $��Center=�6   .+�5      �� $��CREATE RULE��6   $��5q�6/   $��
Montserrat)�6:   ��6R   �� *	Creator+�� 5      $��IMPORT RULE��6   $��5q�6/   $��
Montserrat)�6:   ��6R   �� *
ImportRule+�� 5      $��
SHOW RULES��6   $��5q�6.   $��
Montserrat)�69   ��6Q   �� *
ShowFolder+�� 5      $��RELOAD RULES��6   $��5q�60   $��
Montserrat)�6;   ��6S   �� *Reload+�� 5      $��ACTIVATE ALL RULES��6   $��5q�66   $��
Montserrat)�6A   ��6Y   �� *	SelectAll+�� 5       $��DEACTIVATE ALL RULES��6   $��5q�68   $��
Montserrat)�6C   ��6[   �� *DeselectAll+�� 5!      $��START��6   $��5q�6)   $��
Montserrat)�64   ��6L   �� *Start+�� 5"      $
��Stop��6   $��5q�6(   $��
Montserrat)�63   ��6K   �� *Stop+{�6d   �� *	IsRunning+5#      5$      �� 5%   
   $��1��6   ��5&      �� 5'      $��RULES��6   $
��Bold"�6)   
�������$��Center=�6N   $��
Montserrat)�6k   .��5)      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     CanUserAddRows .     SelectionMode$ Single=�6�      SelectionUnit$ FullRow=�6�   ��6   �� *Rules+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
   Columns
 5*      4  .System.Windows.Controls.DataGridCheckBoxColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ ACTIVE��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 25* 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Active+6X   0  *System.Windows.Controls.DataGridTextColumn  5,      $
 NAME��6)      
IsReadOnly . $	 75* 6b    67   �� *Name+6b   5.      5/      )  #MaterialDesignThemes.Wpf.DialogHost  50   
   $��2��6C      CloseOnClickAway .    IsOpen 6$   �� *ShowCreator+   DialogContent 51      � 52      $
��Auto=�6   .+�53      �� ��6%   �� *CreatorPage+$	 PropertyChanged=�54      56      57      58      