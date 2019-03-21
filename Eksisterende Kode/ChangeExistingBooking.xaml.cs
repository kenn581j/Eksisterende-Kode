using System;
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
    /// Interaction logic for ChangeExistingBooking.xaml
    /// </summary>
    public partial class ChangeExistingBooking : Page
    {
        public ChangeExistingBooking()
        {
            InitializeComponent();
        }

		private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			if(tb.Text == "Identifikationsnøgle")
			{
				tb.Text = string.Empty;
			}
		}

		private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;
			if(tb.Text == string.Empty)
			{
				tb.Text = "Identifikationsnøgle";
			}
		}

		private void btn_Search_Click(object sender, RoutedEventArgs e)
		{
			stackPanel.Visibility = Visibility.Visible;
		}
	}
}
