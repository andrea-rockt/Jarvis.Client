using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Jarvis.Client.MvvmLight;
using Jarvis.WCF.Contracts.Service;
using Microsoft.Practices.ServiceLocation;
using Ninject.Extensions.Interception.Attributes;
using WPFLocalizeExtension.Providers;

namespace Jarvis.Client.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : AutoNotifyViewModelBase
    {

        

        [NotifyOfChanges]
        public virtual CultureInfo CurrentCulture { get; set; }

        [NotifyOfChanges]
        public virtual ObservableCollection<CultureInfo> Cultures { get; set; }

        public CultureMenuViewModel CultureMenuViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CurrentCulture = CultureInfo.CurrentUICulture;
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = CurrentCulture;
            Cultures=ResxLocalizationProvider.Instance.AvailableCultures;
            CultureMenuViewModel = ServiceLocator.Current.GetInstance<CultureMenuViewModel>();

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

     
    }
}