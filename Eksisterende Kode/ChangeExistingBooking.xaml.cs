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
		private string originalText = string.Empty;
		//private string OriginalText { get {return this.originalText; } set {if(this.originalText == string.Empty) this.originalText = value ; } } 

        public ChangeExistingBooking()
        {
            InitializeComponent();
        }

		private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;

		if(originalText == string.Empty)
			{
				originalText = tb.Text;
			}
			


			if(tb.Text == originalText)
			{
				tb.Text = string.Empty;
			}
		}

		private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox tb = sender as TextBox;

			if(tb.Text == string.Empty)
			{
				tb.Text = originalText;
			}

			//originalText = string.Empty;
		}

		private void btn_Search_Click(object sender, RoutedEventArgs e)
		{
			stackPanel.Visibility = Visibility.Visible;
		}
	}
}
