using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jarvis.Client.ViewModel;
using Ninject.Modules;

namespace Jarvis.Client.IoC
{
    public class ViewModelsModule:NinjectModule
    {

        private bool _isDesignTime = false;


        public ViewModelsModule(bool isDesignTime=false)
        {
            _isDesignTime = isDesignTime;
        }

        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            if(_isDesignTime)
            {
                
            }else
            {
                Bind<LocateMeViewModel>().ToSelf().InSingletonScope();
                Bind<MainViewModel>().ToSelf().InSingletonScope();
                Bind<CultureMenuViewModel>().ToSelf();
                Bind<CultureMenuViewModel.CultureMenuItem>().ToSelf();
            }
        }

        #endregion
    }
}
