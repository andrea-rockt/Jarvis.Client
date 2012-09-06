using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using Jarvis.Client.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using Ninject.Extensions.Interception.Attributes;
using WPFLocalizeExtension.Providers;

namespace Jarvis.Client.ViewModel
{
    public class CultureMenuViewModel : AutoNotifyViewModelBase
    {
        public class CultureMenuItem : AutoNotifyViewModelBase
        {
            [NotifyOfChanges("Header")]
            public virtual CultureInfo Culture { get; set; }

            public virtual String Header
            {

                get { return Culture.TextInfo.ToTitleCase(Culture.NativeName); }
            }

            [NotifyOfChanges]
            public virtual Boolean IsChecked { get; set; }


        }

        [NotifyOfChanges]
        public virtual ObservableCollection<CultureMenuItem> CultureMenuItems { get; set; }

        public virtual CultureMenuItem SelectedCultureMenuItem{get
        {
            return (from cmi in CultureMenuItems
                    where cmi.IsChecked
                    select cmi).FirstOrDefault();
        }}

        public CultureMenuViewModel()
        {
           
            CultureMenuItems = new ObservableCollection<CultureMenuItem>();
            SelectCulture = new RelayCommand<CultureMenuItem>(SelectCultureCommand);

            foreach (var availableCulture in ResxLocalizationProvider.Instance.AvailableCultures)
            {
                if (availableCulture.Equals(CultureInfo.InvariantCulture))
                {
                    continue;
                }
                var newMenuItem = ServiceLocator.Current.GetInstance<CultureMenuItem>();
                newMenuItem.Culture = availableCulture;
                newMenuItem.IsChecked = availableCulture.Equals(WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture);
                CultureMenuItems.Add(newMenuItem);
            }

            


        }

        public RelayCommand<CultureMenuItem> SelectCulture { get; set; }

        private void SelectCultureCommand(CultureMenuItem cultureMenuItem)
        {
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = cultureMenuItem.Culture;

            foreach (var menuItem in CultureMenuItems)
            {
                menuItem.IsChecked = false;
            }

            cultureMenuItem.IsChecked = true;
        }
    }
}
