using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Helpers;
using Ninject.Extensions.Interception;

namespace Jarvis.Client.MvvmLight
{
    /// <summary>
    /// A base class for the ViewModel classes in the MVVM pattern.
    /// <para>The IDisposable implementation of this class is obsolete, and
    /// should not be used anymore. It will be removed in a future version.
    /// </para>
    /// </summary>
    public abstract class AutoNotifyViewModelBase : IAutoNotifyPropertyChanged, ICleanup
    {
        private static bool? _isInDesignMode;

        /// <summary>
        /// Initializes a new instance of the ViewModelBase class.
        /// </summary>
        protected AutoNotifyViewModelBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ViewModelBase class.
        /// </summary>
        /// <param name="messenger">An instance of a <see cref="Messenger" />
        /// used to broadcast messages to other objects. If null, this class
        /// will attempt to broadcast using the Messenger's default
        /// instance.</param>
        protected AutoNotifyViewModelBase(IMessenger messenger)
        {
            MessengerInstance = messenger;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {   
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets a value indicating whether the control is in design mode
        /// (running in Blend or Visual Studio).
        /// </summary>
        [SuppressMessage(
            "Microsoft.Security",
            "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands",
            Justification = "The security risk here is neglectible.")]
        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
#if SILVERLIGHT
                    _isInDesignMode = DesignerProperties.IsInDesignTool;
#else
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;

                    // Just to be sure
                    if (!_isInDesignMode.Value
                        && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
                    {
                        _isInDesignMode = true;
                    }
#endif
                }

                return _isInDesignMode.Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the control is in design mode
        /// (running under Blend or Visual Studio).
        /// </summary>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "Non static member needed for data binding")]
        public bool IsInDesignMode
        {
            get
            {
                return IsInDesignModeStatic;
            }
        }

        /// <summary>
        /// Gets or sets an instance of a <see cref="IMessenger" /> used to
        /// broadcast messages to other objects. If null, this class will
        /// attempt to broadcast using the Messenger's default instance.
        /// </summary>
        protected IMessenger MessengerInstance
        {
            get;
            set;
        }

        /// <summary>
        /// Broadcasts a PropertyChangedMessage using either the instance of
        /// the Messenger that was passed to this class (if available) 
        /// or the Messenger's default instance.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="oldValue">The value of the property before it
        /// changed.</param>
        /// <param name="newValue">The value of the property after it
        /// changed.</param>
        /// <param name="propertyName">The name of the property that
        /// changed.</param>
        protected virtual void Broadcast<T>(T oldValue, T newValue, string propertyName)
        {
            var message = new PropertyChangedMessage<T>(this, oldValue, newValue, propertyName);

            if (MessengerInstance != null)
            {
                MessengerInstance.Send(message);
            }
            else
            {
                Messenger.Default.Send(message);
            }
        }


        /// <summary>
        /// Unregisters this instance from the Messenger class.
        /// <para>To cleanup additional resources, override this method, clean
        /// up and then call base.Cleanup().</para>
        /// </summary>
        public virtual void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

    }
}