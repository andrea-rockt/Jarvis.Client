using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Jarvis.Client.Messages;

namespace Jarvis.Client
{
	/// <summary>
	/// Logica di interazione per AddCategoryDialog.xaml
	/// </summary>
	public partial class AddCategoryDialog : Window
	{
		public AddCategoryDialog()
		{
			this.InitializeComponent();
			
			// Inserire il codice richiesto per la creazione dell'oggetto al di sotto di questo punto.
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
	}
}