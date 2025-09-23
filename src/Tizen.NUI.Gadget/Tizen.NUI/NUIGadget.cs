/*
 * Copyright (c) 2023 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Runtime.CompilerServices;
using Tizen.Applications;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    /// <summary>
    /// Represents a NUIGadget controlled lifecycle.
    /// </summary>
    /// <remarks>
    /// This class provides functionality related to managing the lifecycle of a NUIGadget.
    /// It enables developers to handle events such as initialization, activation, deactivation, and destruction of the gadget.
    /// By implementing this class, developers can define their own behavior for these events and customize the lifecycle of their gadgets accordingly.
    /// </remarks>
    /// <since_tizen> 10 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class NUIGadget : Gadget
    {
        /// <summary>
        /// Initializes the gadget.
        /// </summary>
        /// <param name="type">The type of the NUIGadget.</param>
        /// <remarks>
        /// This constructor initializes a new instance of the NUIGadget class based on the specified type.
        /// It is important to provide the correct type argument in order to ensure proper functionality and compatibility with other components.
        /// </remarks>
        /// <since_tizen> 10 </since_tizen>
        public NUIGadget(NUIGadgetType type) : base((GadgetType)type)
        {
        }

        /// <summary>
        /// Initializes the gadget with OneShotService factory.
        /// </summary>
        /// <param name="type">The type of the NUIGadget.</param>
        /// <param name="serviceFactory">The factory that can create OneShotService object</param>
        /// <param name="autoClose">Whether to automatically close the service after execution</param>
        /// <exception cref="ArgumentNullException">Thrown if either 'serviceFactory' is null.</exception>
        /// <remarks>
        /// This constructor initializes a new instance of the NUIGadget class based on the specified type with OneShotService.
        /// It is important to provide the correct type argument in order to ensure proper functionality and compatibility with other components.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public NUIGadget(NUIGadgetType type, IServiceFactory serviceFactory, bool autoClose = true) : base((GadgetType)type, serviceFactory, autoClose)
        {
        }

        /// <summary>
        /// Gets the class representing information of the current gadget.
        /// </summary>
        /// <remarks>
        /// This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides details about the current gadget such as its ID, name, version, and other relevant information.
        /// By accessing this property, developers can retrieve the necessary information about the gadget they are working on.
        /// </remarks>
        /// <since_tizen> 10 </since_tizen>
        public NUIGadgetInfo NUIGadgetInfo
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the type of the NUI gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public new NUIGadgetType Type
        {
            get
            {
                return (NUIGadgetType)base.Type;
            }
        }

        /// <summary>
        /// Gets the class name.
        /// </summary>
        /// <remarks>
        /// This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides access to the name of the class that was used to create the current instance.
        /// </remarks>
        /// <since_tizen> 10 </since_tizen>
        public new string ClassName
        {
            get
            {
                return (string)base.ClassName;
            }
        }

        /// <summary>
        /// Gets the main view of the NUI gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public new View MainView
        {
            get
            {
                return (View)base.MainView;
            }
        }

        /// <summary>
        /// Gets the current lifecycle state of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public new NUIGadgetLifecycleState State
        {
            get
            {
                return (NUIGadgetLifecycleState)base.State;
            }
        }

        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        /// <remarks> This property is set before the OnCreate() is called, after the instance has been created.
        /// It provides access to various resources such as images, sounds, and fonts that can be used in your application.
        /// By utilizing the resource manager, you can easily manage and retrieve these resources without having to manually handle their loading and unloading.
        /// Additionally, the resource manager ensures efficient memory management by automatically handling the caching and recycling of resources.
        /// </remarks>
        /// <since_tizen> 10 </since_tizen>
        public NUIGadgetResourceManager NUIGadgetResourceManager
        {
            get; internal set;
        }

        /// <summary>
        /// Override this method to define the behavior when the gadget is pre-created.
        /// Calling 'base.OnPreCreate()' is necessary in order to emit the 'NUIGadgetLifecycleChanged' event with the 'NUIGadgetLifecycleState.PreCreated' state.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        protected override void OnPreCreate()
        {
            base.OnPreCreate();
        }

        /// <summary>
        /// Override this method to define the behavior when the gadget is created.
        /// Calling 'base.OnCreate()' is necessary in order to emit the 'NUIGadgetLifecycleChanged' event with the 'NUIGadgetLifecycleState.Created' state.
        /// </summary>
        /// <returns>The main view object.</returns>
        /// <since_tizen> 10 </since_tizen>
        protected override Tizen.NUI.BaseComponents.View OnCreate()
        {
            base.OnCreate();
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
        /// <since_tizen> 10 </since_tizen>
        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
        }

        /// <summary>
        /// Override this method to handle the behavior when the gadget is destroyed.
        /// If 'base.OnDestroy()' is not called, the 'NUIGadgetLifecycleChanged' event with the 'NUIGadgetLifecycleState.Destroyed' state will not be emitted.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the gadget is paused.
        /// If 'base.OnPause()' is not called. the event 'NUIGadgetLifecycleChanged' with the 'NUIGadgetLifecycleState.Paused' state will not be emitted.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the gadget is resumed.
        /// If 'base.OnResume()' is not called. the event 'NUIGadgetLifecycleChanged' with the 'NUIGadgetLifecycleState.Resumed' state will not be emitted.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system language is changed.
        /// </summary>
        /// <param name="e">The locale changed event argument.</param>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system battery is low.
        /// </summary>
        /// <param name="e">The low batter event argument.</param>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the system memory is low.
        /// </summary>
        /// <param name="e">The low memory event argument.</param>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the region format is changed.
        /// </summary>
        /// <param name="e">The region format changed event argument.</param>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the device orientation is changed.
        /// </summary>
        /// <param name="e">The device orientation changed event argument.</param>
        /// <since_tizen> 10 </since_tizen>
        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the message is received.
        /// </summary>
        /// <param name="e">The message received event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected virtual void OnMessageReceived(NUIGadgetMessageReceivedEventArgs e)
        {            
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the message is received.
        /// </summary>
        /// <param name="e">The message received event argument.</param>
        /// <since_tizen> 13 </since_tizen>
        protected override void OnMessageReceived(GadgetMessageReceivedEventArgs e)
        {
            base.OnMessageReceived(e);
            if (e != null)
            {
                OnMessageReceived(new NUIGadgetMessageReceivedEventArgs(e.Message));
            }
        }

        /// <summary>
        /// Sends the message to the gadget.
        /// The message will be delived to the OnMessageReceived() method.
        /// </summary>
        /// <param name="message">The message</param>
        /// <exception cref="ArgumentNullException">Thrown if either 'envelope' is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public new void SendMessage(Bundle message)
        {
            base.SendMessage(message);
        }

        /// <summary>
        /// Finishes the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public new void Finish()
        {
            base.Finish();
        }
    }
}
