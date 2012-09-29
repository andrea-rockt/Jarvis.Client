using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Windows.Media.Imaging;
using Bing.Maps.GeocodeService;
using Bing.Maps.ImageryService;
using Jarvis.Client.MvvmLight;
using Jarvis.Client.Properties;
using Microsoft.Practices.ServiceLocation;
using Ninject.Extensions.Interception.Attributes;


namespace Jarvis.Client.ViewModel
{
    public class LocateMeViewModel :AutoNotifyViewModelBase
    {
        [NotifyOfChanges]
        public virtual BitmapImage Map { get; set; }

       

        private string _address;
        
        [NotifyOfChanges("Map")]
        public virtual string Address
        {
            get { return _address; }
            set { 
                _address = value;
                Map = GetMap(_address);
            }
        }

        public LocateMeViewModel()
        {
            Address = "Via Fidia 23, Torino";
        }

        private BitmapImage GetMap(string address)
        {
            var geocodeClient = ServiceLocator.Current.GetInstance<IGeocodeService>();

            var geocodeRequest = new GeocodeRequest()
            {
                Credentials = new Bing.Maps.GeocodeService.Credentials()
                {
                    ApplicationId = Settings.Default.BingMapsKey
                },
                Query = address
            };


            var geocodeResponse = geocodeClient.Geocode(geocodeRequest);


            var imageryClient = ServiceLocator.Current.GetInstance<IImageryService>();

            var imageryRequest = new MapUriRequest()
            {
                Credentials = new Bing.Maps.ImageryService.Credentials()
                {
                    ApplicationId = Settings.Default.BingMapsKey
                },
                Center = new Bing.Maps.ImageryService.Location()
                {
                    Latitude =
                        geocodeResponse.Results[0].Locations[0].Latitude,
                    Longitude =
                        geocodeResponse.Results[0].Locations[0].Longitude
                },
                Options = new MapUriOptions()
                {
                    Style = MapStyle.AerialWithLabels,
                    ZoomLevel = 17,
                    ImageSize = new Bing.Maps.ImageryService.SizeOfint()
                    {
                        Height = 512,
                        Width = 512
                    }
                }
            };

            var mapUriResponse = imageryClient.GetMapUri(imageryRequest);

            return new BitmapImage(new Uri(mapUriResponse.Uri), new RequestCachePolicy(RequestCacheLevel.BypassCache));
        }

   
    }
}
