using System;
using System.Globalization;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Jarvis.Client.Messages;

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
            Messenger.Default.Register<AddCategoryDialogMessage>(this,ShowAddCategoryDialog);
        }

        private void JarvisMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var culture=System.Threading.Thread.CurrentThread.CurrentUICulture;
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture =
                CultureInfo.CreateSpecificCulture("it-it");
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = culture;

        }

        void ShowAddCategoryDialog(AddCategoryDialogMessage message)
        {
            AddCategoryDialog addCategoryDialog = new AddCategoryDialog();
            addCategoryDialog.DataContext = message.Content;
            addCategoryDialog.Owner = this;
            addCategoryDialog.ShowDialog();
        }

    }
}
