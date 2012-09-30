using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Jarvis.Client.MvvmLight;
using Jarvis.WCF.Contracts.Data;
using Jarvis.WCF.Contracts.Service;
using Microsoft.Practices.ServiceLocation;
using Ninject.Extensions.Interception.Attributes;

namespace Jarvis.Client.ViewModel
{
    public class ManageLocationsViewModel : AutoNotifyViewModelBase, IManageLocationsViewModel
    {
        private readonly ILocationService _locationService;



        private ObservableCollection<Location> _locations = null;

        [NotifyOfChanges]
        public virtual ObservableCollection<Location> Locations
        {
            get
            {
                if(_locations == null)
                {
                    _locations = new ObservableCollection<Location>(_locationService.GetKnownLocations());
                }
                return _locations;
            }
            set { _locations = value; }
        }

        [NotifyOfChanges("IsLocationSelected")]
        public virtual Location SelectedLocation { get; set; }

        [NotifyOfChanges]
        public virtual bool IsLocationSelected{get { return SelectedLocation != null; }}

        #region Commands

        [NotifyOfChanges]
        public virtual RelayCommand AddLocation { get; set; }


        public void AddLocationAction()
        {
            var location = new Location() {Name = "New Location - " + Guid.NewGuid()};

            
                _locationService.StoreCurrentLocation(location);
            


            RefreshLocations();
        }

        [NotifyOfChanges]
        public virtual RelayCommand UpdateLocation { get; set; }

        public void UpdateLocationAction()
        {
            _locationService.UpdateLocation(SelectedLocation);
        }

        public bool UpdateLocationCanExecute()
        {
            return SelectedLocation != null;
        }

        [NotifyOfChanges]
        public virtual RelayCommand RemoveLocation { get; set; }

        public void RemoveLocationAction()
        {
            _locationService.RemoveLocation(SelectedLocation);
            RefreshLocations();
        }

        public bool RemoveLocationCanExecute()
        {
            return SelectedLocation != null;
        }
        #endregion


        public ManageLocationsViewModel()
        {
            _locationService = ServiceLocator.Current.GetInstance<ILocationService>();
            AddLocation = new RelayCommand(AddLocationAction);
            UpdateLocation = new RelayCommand(UpdateLocationAction, UpdateLocationCanExecute);
            RemoveLocation = new RelayCommand(RemoveLocationAction,RemoveLocationCanExecute);
        }


        private void RefreshLocations()
        {
            //Locations.Clear();
            //foreach (var location in _locationService.GetKnownLocations())
            //{
            //    Locations.Add(location);
            //}

            Locations = new ObservableCollection<Location>(_locationService.GetKnownLocations());

        }
    }
}
