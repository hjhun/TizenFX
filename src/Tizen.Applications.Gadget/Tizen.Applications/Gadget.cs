/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;

namespace Tizen.Applications
{
    /// <summary>
    /// Represents a Gadget controlled lifecycle.
    /// </summary>
    /// <remarks>
    /// This class provides functionality related to managing the lifecycle of a Gadget.
    /// It enables developers to handle events such as initialization, activation, deactivation, and destruction of the gadget.
    /// By implementing this class, developers can define their own behavior for these events and customize the lifecycle of their gadgets accordingly.
    /// </remarks>
    /// <since_tizen> 13 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Gadget
    {
        private static int s_ServiceNameSequence = 0;

        /// <summary>
        /// Initializes the gadget.
        /// </summary>
        /// <param name="type">The type of the Gadget.</param>
        /// <remarks>
        /// This constructor initializes a new instance of the Gadget class based on the specified type.
        /// It is important to provide the correct type argument in order to ensure proper functionality and compatibility with other components.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public Gadget(GadgetType type)
        {
            Type = type;
            State = GadgetLifecycleState.Initialized;
            Log.Info("Type=" + Type + ", State=" + State);
        }

        /// <summary>
        /// Initializes the gadget with OneShotService factory.
        /// </summary>
        /// <param name="type">The type of the Gadget.</param>
        /// <param name="serviceFactory">The factory that can create OneShotService object</param>
        /// <param name="autoClose">Whether to automatically close the service after execution</param>
        /// <exception cref="ArgumentNullException">Thrown if either 'serviceFactory' is null.</exception>
        /// <remarks>
        /// This constructor initializes a new instance of the Gadget class based on the specified type with OneShotService.
        /// It is important to provide the correct type argument in order to ensure proper functionality and compatibility with other components.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public Gadget(GadgetType type, IServiceFactory serviceFactory, bool autoClose = true) : this(type)
        {
            if (serviceFactory == null)
            {
                throw new ArgumentNullException(nameof(serviceFactory));
            }

            AutoClose = autoClose;
            ServiceFactory = serviceFactory;
            Service = ServiceFactory.CreateService(GenerateOneShotServiceName(), AutoClose);
            Service.LifecycleStateChanged += OnOneShotServiceLifecycleChanged;
        }

        internal event EventHandler<GadgetLifecycleChangedEventArgs> LifecycleChanged;

        /// <summary>
        /// Occurs when the lifecycle of the OneShotService is changed.
        /// </summary>
        /// <remarks>
        /// This event is raised when the state of OneShotService changes.
        /// It provides information about the current state through the 
        /// OneShotServiceLifecycleChangedEventArgs argument.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public event EventHandler<OneShotServiceLifecycleChangedEventArgs> OneShotServiceLifecycleChanged;

        /// <summary>
        /// Gets the class representing information of the current gadget.
        /// </summary>
        /// <remarks>
        /// This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides details about the current gadget such as its ID, name, version, and other relevant information.
        /// By accessing this property, developers can retrieve the necessary information about the gadget they are working on.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public GadgetInfo GadgetInfo
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets the type of the NUI gadget.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public GadgetType Type
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets the class name.
        /// </summary>
        /// <remarks>
        /// This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides access to the name of the class that was used to create the current instance.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public string ClassName
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets the main view of the gadget.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public object MainView
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets the current lifecycle state of the gadget.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public GadgetLifecycleState State
        {
            internal set;
            get;
        }

        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        /// <remarks> This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides access to various resources such as images, sounds, and fonts that can be used in your application.
        /// By utilizing the resource manager, you can easily manage and retrieve these resources without having to manually handle their loading and unloading.
        /// Additionally, the resource manager ensures efficient memory management by automatically handling the caching and recycling of resources.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public GadgetResourceManager GadgetResourceManager
        {
            internal set;
            get;
        }

        /// <summary>
        /// The OneShotService.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public OneShotService Service
        {
            internal set;
            get;
        }

        private IServiceFactory ServiceFactory
        {
            set; get;
        }

        private bool AutoClose
        {
            set; get;
        }

        internal void PreCreate()
        {
            if (State == GadgetLifecycleState.Initialized)
            {
                OnPreCreate();
                if (Service != null)
                {
                    Log.Info($"PreCreate(), Service.Name = {Service.Name}");
                    Service.Run();
                }
            }
        }

        internal bool Create()
        {
            if (State == GadgetLifecycleState.PreCreated)
            {
                this.MainView = OnCreate();
                if (this.MainView == null)
                {
                    return false;
                }
            }

            return true;
        }

        internal void Resume()
        {
            if (State == GadgetLifecycleState.Created || State == GadgetLifecycleState.Paused)
            {
                OnResume();
            }
        }

        internal void Pause()
        {
            if (State == GadgetLifecycleState.Resumed)
            {
                OnPause();
            }
        }

        internal void Destroy()
        {
            if (State == GadgetLifecycleState.PreCreated || State == GadgetLifecycleState.Created || State == GadgetLifecycleState.Paused)
            {
                OnDestroy();
            }
        }

        internal void HandleAppControlReceivedEvent(AppControlReceivedEventArgs args)
        {
            OnAppControlReceived(args);
        }

        internal void HandleEvents(GadgetEventType eventType, EventArgs args)
        {
            switch (eventType)
            {
                case GadgetEventType.LocaleChanged:
                    OnLocaleChanged((LocaleChangedEventArgs)args);
                    break;
                case GadgetEventType.LowMemory:
                    OnLowMemory((LowMemoryEventArgs)args);
                    break;
                case GadgetEventType.LowBattery:
                    OnLowBattery((LowBatteryEventArgs)args);
                    break;
                case GadgetEventType.RegionFormatChanged:
                    OnRegionFormatChanged((RegionFormatChangedEventArgs)args);
                    break;
                case GadgetEventType.DeviceOrientationChanged:
                    OnDeviceOrientationChanged((DeviceOrientationEventArgs)args);
                    break;
                default:
                    Log.Warn("Unknown Event Type: " + eventType);
                    break;
            }
        }

        private void OnOneShotServiceLifecycleChanged(object sender, OneShotServiceLifecycleChangedEventArgs args)
        {
            OneShotServiceLifecycleChanged?.Invoke(sender, args);

            if (args.State == OneShotServiceLifecycleState.Destroyed)
            {
                args.OneShotService.LifecycleStateChanged -= OnOneShotServiceLifecycleChanged;
            }
        }

        private void NotifyLifecycleChanged()
        {
            var args = new GadgetLifecycleChangedEventArgs();
            args.State = State;
            args.Gadget = this;
            LifecycleChanged?.Invoke(null, args);
        }

        private static string GenerateOneShotServiceName()
        {
            return $"oneshot{s_ServiceNameSequence++}";
        }

        /// <summary>
        /// Override this method to define the behavior when the gadget is pre-created.
        /// Calling 'base.OnPreCreate()' is necessary in order to emit the 'GadgetLifecycleChanged' event with the 'GadgetLifecycleState.PreCreated' state.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnPreCreate()
        {
            State = GadgetLifecycleState.PreCreated;
            Log.Debug("ClassName=" + ClassName);
            NotifyLifecycleChanged();
        }

        /// <summary>
        /// Override this method to define the behavior when the gadget is created.
        /// Calling 'base.OnCreate()' is necessary in order to emit the 'GadgetLifecycleChanged' event with the 'GadgetLifecycleState.Created' state.
        /// </summary>
        /// <returns>The main view object.</returns>
        /// <since_tizen> 13 </since_tizen>
        protected virtual object OnCreate()
        {
            State = GadgetLifecycleState.Created;
            Log.Debug("ClassName=" + ClassName);
            NotifyLifecycleChanged();
            return null;
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the gadget receives the appcontrol message.
        /// </summary>
        /// <remarks>
        /// This method provides a way to customize the response when the gadget receives an appcontrol message.
        /// By overriding this method in your derived class, you can define specific actions based on the incoming arguments.
        /// </remarks>
        /// <param name="e">The appcontrol received event argument containing details about the received message.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            Log.Debug("ClassName=" + ClassName);
        }

        /// <summary>
        /// Override this method to handle the behavior when the gadget is destroyed.
        /// If 'base.OnDestroy()' is not called, the 'GadgetLifecycleChanged' event with the 'GadgetLifecycleState.Destroyed' state will not be emitted.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnDestroy()
        {
            State = GadgetLifecycleState.Destroyed;
            Log.Debug("ClassName=" + ClassName);
            NotifyLifecycleChanged();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the gadget is paused.
        /// If 'base.OnPause()' is not called. the event 'GadgetLifecycleChanged' with the 'GadgetLifecycleState.Paused' state will not be emitted.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnPause()
        {
            State = GadgetLifecycleState.Paused;
            Log.Debug("ClassName=" + ClassName);
            NotifyLifecycleChanged();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the gadget is resumed.
        /// If 'base.OnResume()' is not called. the event 'GadgetLifecycleChanged' with the 'GadgetLifecycleState.Resumed' state will not be emitted.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnResume()
        {
            State = GadgetLifecycleState.Resumed;
            Log.Debug("ClassName=" + ClassName);
            NotifyLifecycleChanged();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system language is changed.
        /// </summary>
        /// <param name="e">The locale changed event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnLocaleChanged(LocaleChangedEventArgs e)
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system battery is low.
        /// </summary>
        /// <param name="e">The low batter event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnLowBattery(LowBatteryEventArgs e)
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system memory is low.
        /// </summary>
        /// <param name="e">The low memory event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnLowMemory(LowMemoryEventArgs e)
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the region format is changed.
        /// </summary>
        /// <param name="e">The region format changed event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the device orientation is changed.
        /// </summary>
        /// <param name="e">The device orientation changed event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the message is received.
        /// </summary>
        /// <param name="e">The message received event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnMessageReceived(GadgetMessageReceivedEventArgs e)
        {
        }

        /// <summary>
        /// Sends the message to the gadget.
        /// The message will be delived to the OnMessageReceived() method.
        /// </summary>
        /// <param name="message">The message</param>
        /// <exception cref="ArgumentNullException">Thrown if either 'message' is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public void SendMessage(Bundle message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            CoreApplication.Post(() =>
            {
                OnMessageReceived(new GadgetMessageReceivedEventArgs(message));
            });
        }

        /// <summary>
        /// Finishes the gadget.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public void Finish()
        {
            Pause();
            Destroy();
        }
    }
}
