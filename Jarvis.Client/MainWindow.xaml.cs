using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLocalizeExtension.Providers;

namespace Jarvis.Client
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void JarvisMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var culture=System.Threading.Thread.CurrentThread.CurrentUICulture;
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture =
                CultureInfo.CreateSpecificCulture("it-it");
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = culture;

        }
    }
}
