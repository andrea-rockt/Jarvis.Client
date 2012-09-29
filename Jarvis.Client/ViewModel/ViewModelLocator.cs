/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Jarvis.Client"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Collections.Generic;
using System.Globalization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Jarvis.Client.IoC;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;
using NinjectAdapter;

namespace Jarvis.Client.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IKernel _container;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var modules = new INinjectModule[]
                              {
                                  new LocationServiceModule(),
                                  new ViewModelsModule(ViewModelBase.IsInDesignModeStatic),
                                  new BingMapsModule(), 
                              };

            
            _container = new StandardKernel(modules);
           
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(_container));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LocateMeViewModel LocateMe
        {
            get { return ServiceLocator.Current.GetInstance<LocateMeViewModel>(); }
        }
        
        public static void Cleanup()
        {
            ServiceLocator.Current.GetInstance<MainViewModel>();
        }
    }
}