using System.Globalization;
using System.Windows;

namespace Jarvis.Client.View
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
