using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Jarvis.WCF.Contracts.Data;
using Ninject.Extensions.Interception.Attributes;

namespace Jarvis.Client.ViewModel
{
    public interface IManageLocationsViewModel
    {
        [NotifyOfChanges]
        ObservableCollection<Location> Locations { get; }

        [NotifyOfChanges]
        Location SelectedLocation { get; set; }

        [NotifyOfChanges]
        RelayCommand AddLocation { get; set; }

        [NotifyOfChanges]
        RelayCommand UpdateLocation { get; set; }
    }
}