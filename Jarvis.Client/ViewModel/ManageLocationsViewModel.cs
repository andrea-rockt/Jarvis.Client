using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Jarvis.Client.Messages;
using Jarvis.Client.Model;
using Jarvis.Client.MvvmLight;
using Jarvis.WCF.Contracts.Service;
using Microsoft.Practices.ServiceLocation;
using Ninject.Extensions.Interception.Attributes;
using Action = Jarvis.Client.Model.Action;

namespace Jarvis.Client.ViewModel
{
    public class ManageLocationsViewModel : AutoNotifyViewModelBase, IManageLocationsViewModel
    {
        private readonly ILocationService _locationService;
        private readonly IMappingEngine _mappingEngine;


        private ObservableCollection<Location> _locations = null;

        [NotifyOfChanges]
        public virtual ObservableCollection<Location> Locations
        {
            get
            {
                if (_locations == null)
                {
                    var list = _mappingEngine.Map<IList<Jarvis.Client.Model.Location>>(_locationService.GetKnownLocations());
                    _locations = new ObservableCollection<Location>(list);
                }
                return _locations;
            }
            set { _locations = value; }
        }

        [NotifyOfChanges("IsLocationSelected")]
        public virtual Location SelectedLocation { get; set; }

        [NotifyOfChanges]
        public virtual bool IsLocationSelected { get { return SelectedLocation != null; } }



        [NotifyOfChanges("IsActionSelected")]
        public virtual Action SelectedAction { get; set; }

        [NotifyOfChanges]
        public virtual bool IsActionSelected { get { return SelectedAction != null; } }

        [NotifyOfChanges("IsCategorySelected")]
        public virtual LocationCategory SelectedCategory { get; set; }

        [NotifyOfChanges]
        public virtual bool IsCategorySelected { get { return SelectedCategory != null; } }
        #region Commands

        [NotifyOfChanges]
        public virtual RelayCommand AddLocation { get; set; }


        public void AddLocationAction()
        {
            var location = new Location() { Name = "New Location - " + Guid.NewGuid(), Id = Guid.NewGuid()};


            _locationService.StoreCurrentLocation(_mappingEngine.Map<Jarvis.WCF.Contracts.Data.Location>(location));



            RefreshLocations();
        }

        [NotifyOfChanges]
        public virtual RelayCommand UpdateLocation { get; set; }

        public void UpdateLocationAction()
        {
            _locationService.UpdateLocation(_mappingEngine.Map<Jarvis.WCF.Contracts.Data.Location>(SelectedLocation));
        }

        public bool UpdateLocationCanExecute()
        {
            return SelectedLocation != null;
        }

        [NotifyOfChanges]
        public virtual RelayCommand RemoveLocation { get; set; }

        public void RemoveLocationAction()
        {
            _locationService.RemoveLocation(_mappingEngine.Map<Jarvis.WCF.Contracts.Data.Location>(SelectedLocation));
            RefreshLocations();
        }

        public bool RemoveLocationCanExecute()
        {
            return SelectedLocation != null;
        }



        [NotifyOfChanges]
        public virtual RelayCommand AddAction { get; set; }

        public void AddActionAction()
        {
            throw new NotImplementedException("not imp");
        }

        public bool AddActionCanExecute()
        {
            return true;
        }

        [NotifyOfChanges]
        public virtual RelayCommand RemoveAction { get; set; }

        public void RemoveActionAction()
        {
            throw new NotImplementedException("not imp");
        }

        public bool RemoveActionCanExecute()
        {
            return IsActionSelected;
        }

        [NotifyOfChanges]
        public virtual RelayCommand AddCategory { get; set; }

        public void AddCategoryAction()
        {
            var category = new LocationCategory() { Name = "Category" };
            
            SelectedLocation.Categories.Add(category);
            
            Messenger.Default.Send(new AddCategoryDialogMessage(category));
        }

        public bool AddCategoryCanExecute()
        {
            return true;
        }

        [NotifyOfChanges]
        public virtual RelayCommand RemoveCategory { get; set; }

        public void RemoveCategoryAction()
        {
            SelectedLocation.Categories.Remove(SelectedCategory);            
        }

        public bool RemoveCategoryCanExecute()
        {
            return IsCategorySelected;
        }



        #endregion




        public ManageLocationsViewModel()
        {
            _locationService = ServiceLocator.Current.GetInstance<ILocationService>();
            _mappingEngine = ServiceLocator.Current.GetInstance<IMappingEngine>();
            AddLocation = new RelayCommand(AddLocationAction);
            UpdateLocation = new RelayCommand(UpdateLocationAction, UpdateLocationCanExecute);
            RemoveLocation = new RelayCommand(RemoveLocationAction, RemoveLocationCanExecute);
            AddAction = new RelayCommand(AddActionAction, AddActionCanExecute);
            RemoveAction = new RelayCommand(RemoveActionAction, RemoveActionCanExecute);
            AddCategory = new RelayCommand(AddCategoryAction, AddCategoryCanExecute);
            RemoveCategory = new RelayCommand(RemoveCategoryAction, RemoveCategoryCanExecute);

        }


        private void RefreshLocations()
        {
            var list = _mappingEngine.Map<IList<Jarvis.Client.Model.Location>>(_locationService.GetKnownLocations());
            Locations = new ObservableCollection<Location>(list);
        }
    }
}
