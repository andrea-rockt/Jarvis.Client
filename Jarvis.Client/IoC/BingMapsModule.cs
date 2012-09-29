using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bing.Maps.GeocodeService;
using Bing.Maps.ImageryService;
using Ninject.Modules;

namespace Jarvis.Client.IoC
{
    public class BingMapsModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.Bind<IGeocodeService>().To<GeocodeServiceClient>().WithConstructorArgument("endpointConfigurationName","BasicHttpBinding_IGeocodeService");
            Kernel.Bind<IImageryService>().To<ImageryServiceClient>().WithConstructorArgument("endpointConfigurationName","BasicHttpBinding_IImageryService");;
        }

    }
}
