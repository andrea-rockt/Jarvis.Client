using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Jarvis.WCF.Contracts.Service;
using Ninject.Activation;
using Ninject.Modules;
using Ninject;

namespace Jarvis.Client.IoC
{
    public class LocationServiceModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ChannelFactory<ILocationService>>().ToMethod(GetFactoryForLocationService).InSingletonScope();
            Bind<ILocationService>().ToMethod(GetProxyForLocationService);
        }

        public ChannelFactory<ILocationService> GetFactoryForLocationService(IContext context)
        {
            return new ChannelFactory<ILocationService>(
                new NetNamedPipeBinding("NetNamedPipeBinding_ILocationService"), "net.pipe://localhost/Jarvis/Location");
        } 

        public ILocationService GetProxyForLocationService(IContext context)
        {

            return context.Kernel.Get<ChannelFactory<ILocationService>>().CreateChannel();
        }

        #endregion
    }
}
