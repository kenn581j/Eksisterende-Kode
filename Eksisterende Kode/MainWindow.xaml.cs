﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eksisterende_Kode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void btn_OpretBooking_Click(object sender, RoutedEventArgs e)
		{
			ContentFrame.NavigationService.Navigate(new OpretBooking());
		}

		private void btn_RedigerBooking_Click(object sender, RoutedEventArgs e)
		{
			ContentFrame.NavigationService.Navigate(new ChangeExistingBooking());
		}

		private void btn_SletBooking_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_TilføjRustvogn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_RedigerRustvogn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_AfslutProgram_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
