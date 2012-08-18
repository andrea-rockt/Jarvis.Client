using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Jarvis.TrayClient
{
    /// <summary>
    /// Logica di interazione per TrayPopupUserControl.xaml
    /// </summary>
    public partial class TrayPopupUserControl : UserControl, INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private ServiceController _serviceController;


        private Boolean _serviceIsRunning;
        public Boolean ServiceIsRunning
        {
            get { return _serviceIsRunning; }
            set
            {
                if (_serviceIsRunning != value)
                {
                    _serviceIsRunning = value;
                    OnPropertyChanged((x) => x.ServiceIsRunning);
                }
            }
        }

        private SolidColorBrush _labelBrush = new SolidColorBrush();
        public SolidColorBrush LabelBrush
        {
            get { return _labelBrush; }
            set
            {
                if (_labelBrush != value)
                {
                    var ca = new ColorAnimation(value.Color, new Duration(TimeSpan.FromMilliseconds(200)));
                    _labelBrush.BeginAnimation(SolidColorBrush.ColorProperty, ca);
                    OnPropertyChanged((x) => x.LabelBrush);
                }
            }
        }

        private SolidColorBrush _labelBorderBrush = new SolidColorBrush();
        public SolidColorBrush LabelBorderBrush
        {
            get { return _labelBorderBrush; }
            set
            {
                var ca = new ColorAnimation(value.Color, new Duration(TimeSpan.FromMilliseconds(200)));
                _labelBorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, ca);
                OnPropertyChanged((x) => x.LabelBorderBrush);
            }
        }

        public TrayPopupUserControl()
        {
            InitializeComponent();
            _serviceController = new ServiceController();
            _timer = new DispatcherTimer
                         {
                             Interval = TimeSpan.FromSeconds(1),
                             IsEnabled = true
                         };
            _timer.Tick += UpdateServiceStatus;
            _timer.Start();
            LabelBrush = new SolidColorBrush(Colors.Red);
            LabelBorderBrush = new SolidColorBrush(Colors.Red);
            ServiceLabel.Content = "Jarvis Service is not installed";
            _serviceController = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName.Contains("JarvisService"));
        }

        void UpdateServiceStatus(object sender, EventArgs args)
        {
            _serviceController.Refresh();
            if (_serviceController == null)
            {
                ServiceLabel.Content = "Jarvis Service is not installed";
            }
            else
            {

                if (_serviceController.Status == ServiceControllerStatus.Running)
                {
                    ServiceLabel.Content = "Service Started";
                    LabelBrush = new SolidColorBrush(Colors.Green);
                    LabelBorderBrush = new SolidColorBrush(Colors.DarkGreen);
                    ServiceIsRunning = true;
                }
                else
                {
                    ServiceLabel.Content = "Service Stopped";
                    LabelBrush = new SolidColorBrush(Colors.Red);
                    LabelBorderBrush = new SolidColorBrush(Colors.DarkRed);
                    ServiceIsRunning = false;
                }
            }
        }



        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            if (_serviceController != null)
            {
                try
                {
                    _serviceController.Start();
                }
                catch (InvalidOperationException ex)
                {
                    ServiceLabel.Content = ex.Message;
                    LabelBrush = new SolidColorBrush(Colors.Red);
                    LabelBorderBrush = new SolidColorBrush(Colors.DarkRed);
                }
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {

            CleanShutdown();
        }

        private void CleanShutdown()
        {
            if (_serviceController != null)
            {
                _serviceController.Close();
            }

            _timer.Stop();

            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }
        }

        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            if (_serviceController != null)
            {
                try
                {
                    _serviceController.Stop();
                }
                catch (InvalidOperationException ex)
                {
                    ServiceLabel.Content = ex.Message;
                    LabelBrush = new SolidColorBrush(Colors.Red);
                    LabelBorderBrush = new SolidColorBrush(Colors.DarkRed);
                }
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(Expression<Func<TrayPopupUserControl, object>> property)
        {
            string propertyName = null;
            MemberExpression memberExp = property.Body as MemberExpression;
            if (memberExp != null)
                propertyName = memberExp.Member.Name;

            // for DateTime
            UnaryExpression unaryExp = property.Body as UnaryExpression;
            if (unaryExp != null)
            {
                memberExp = unaryExp.Operand as MemberExpression;
                if (memberExp != null)
                    propertyName = memberExp.Member.Name;
            }

            var eventArgs = new PropertyChangedEventArgs(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, eventArgs);
        }

        #endregion
    }
}
