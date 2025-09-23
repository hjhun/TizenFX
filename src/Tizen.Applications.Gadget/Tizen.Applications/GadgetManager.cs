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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;

using SystemIO = System.IO;

namespace Tizen.Applications
{
    /// <summary>
    /// The GadgetManager provides methods and events related to managing gadgets in the NUI.
    /// </summary>
    /// <since_tizen> 13 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GadgetManager
    {
        private static readonly ConcurrentDictionary<string, GadgetInfo> _gadgetInfos = new ConcurrentDictionary<string, GadgetInfo>(StringComparer.Ordinal);
        private static readonly ConcurrentDictionary<Gadget, byte> _gadgets = new ConcurrentDictionary<Gadget, byte>();

        static GadgetManager()
        {
            var ptr = Interop.Libc.GetEnvironmentVariable("GADGET_PKGIDS");
            if (ptr != IntPtr.Zero)
            {
                var packages = Marshal.PtrToStringAnsi(ptr);
                if (!string.IsNullOrWhiteSpace(packages))
                {
                    foreach (var pkg in packages.Split(':'))
                    {
                        var info = GadgetInfo.CreateGadgetInfo(pkg);
                        if (info != null)
                        {
                            try
                            {
                                _gadgetInfos.TryAdd(info.ResourceType, info);
                            }
                            catch (Exception e) when (e is ArgumentNullException || e is OverflowException)
                            {
                                Log.Error("Exception occurs. " + e.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                Log.Warn("Failed to get environment variable");
            }

            var app = (CoreApplication)CoreApplication.Current;
            app.AppControlReceived += (s, e) => HandleAppControl(e);
            app.LowMemory += (s, e) => HandleEvents(GadgetEventType.LowMemory, e);
            app.LowBattery += (s, e) => HandleEvents(GadgetEventType.LowBattery, e);
            app.LocaleChanged += (s, e) => HandleEvents(GadgetEventType.LocaleChanged, e);
            app.RegionFormatChanged += (s, e) => HandleEvents(GadgetEventType.RegionFormatChanged, e);
            app.DeviceOrientationChanged += (s, e) => HandleEvents(GadgetEventType.DeviceOrientationChanged, e);
        }

        /// <summary>
        /// Occurs when the lifecycle of the Gadget is changed.
        /// </summary>
        /// <remarks>
        /// This event is raised when the state of the Gadget changes.
        /// It provides information about the current state through the GadgetLifecycleChangedEventArgs argument.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public static event EventHandler<GadgetLifecycleChangedEventArgs> GadgetLifecycleChanged;

        private static void OnGadgetLifecycleChanged(object sender, GadgetLifecycleChangedEventArgs args)
        {
            GadgetLifecycleChanged?.Invoke(sender, args);

            if (args.State == GadgetLifecycleState.Destroyed)
            {
                args.Gadget.LifecycleChanged -= OnGadgetLifecycleChanged;
                _gadgets.TryRemove(args.Gadget, out _);
            }
        }

        private static GadgetInfo Find(string resourceType)
        {
            if (!_gadgetInfos.TryGetValue(resourceType, out var info))
            {
                throw new ArgumentException("Failed to find GadgetInfo. resource type: " + resourceType);
            }

            return info;
        }

        /// <summary>
        /// Loads an assembly of the Gadget.
        /// </summary>
        /// <param name="resourceType">The resource type of the Gadget package.</param>
        /// <remarks>
        /// This method loads an assembly of the Gadget based on the specified resource type.
        /// It throws an ArgumentException if the argument is invalid, or an InvalidOperationException if the operation fails due to any reason.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid argument.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void Load(string resourceType) => Load(resourceType, true);

        /// <summary>
        /// Loads an assembly of the Gadget.
        /// </summary>
        /// <param name="resourceType">The resource type of the Gadget package.</param>
        /// <param name="useDefaultContext">Indicates whether to use a default load context or not.</param>
        /// <exception cref="ArgumentException">Thrown when failed due to an invalid argument.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed due to an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void Load(string resourceType, bool useDefaultContext)
        {
            if (string.IsNullOrEmpty(resourceType))
            {
                throw new ArgumentException("Invalid argument");
            }

            GadgetInfo info = Find(resourceType);
            LoadInternal(info, useDefaultContext);
        }

        private static void LoadInternal(GadgetInfo info, bool useDefaultContext)
        {
            if (info == null)
            {
                throw new ArgumentException("Invalid argument", nameof(info));
            }

            try
            {
                lock (info)
                {
                    if (useDefaultContext)
                    {
                        if (info.Assembly == null)
                        {

                            Log.Warn("Gadget.Load(): " + info.ResourcePath + info.ExecutableFile + " ++");
                            info.Assembly = Assembly.Load(SystemIO.Path.GetFileNameWithoutExtension(info.ExecutableFile));
                            Log.Warn("Gadget.Load(): " + info.ResourcePath + info.ExecutableFile + " --");
                        }
                    }
                    else
                    {
                        if (info.GadgetAssembly == null || !info.GadgetAssembly.IsLoaded)
                        {
                            Log.Warn("GadgetAssembly.Load(): " + info.GadgetResourcePath + info.ExecutableFile + " ++");
                            info.GadgetAssembly = new GadgetAssembly(info.GadgetResourcePath + info.ExecutableFile);
                            info.GadgetAssembly.Load();
                            Log.Warn("GadgetAssembly.Load(): " + info.GadgetResourcePath + info.ExecutableFile + " --");
                        }
                    }
                }
            }
            catch (Exception e) when (e is FileLoadException || e is BadImageFormatException)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }

        /// <summary>
        /// Unloads the specified Gadget assembly from memory.
        /// </summary>
        /// <remarks>
        /// To use this method properly, the assembly of the gadget must be loaded using Load() with the custom context.
        /// </remarks>
        /// <param name="resourceType">The resource type of the Gadget package to unload.</param>
        /// <exception cref="ArgumentException">Thrown when the argument passed is not valid.</exception>
        /// <example>
        /// <code>
        /// /// Load an assembly of the Gadget.
        /// GadgetManager.Load("org.tizen.appfw.gadget.NetworkSetting", false);
        /// /// GadgetManager.Add("org.tizen.appfw.gadget.NetworkSetting", "NetworkSettingGadget", false);
        ///
        /// /// Unload the loaded assembly
        /// GadgetManager.Unload("org.tizen.appfw.gadget.NetworkSetting");
        /// </code>
        /// </example>
        /// <since_tizen> 13 </since_tizen>
        public static void Unload(string resourceType)
        {
            if (string.IsNullOrWhiteSpace(resourceType))
            {
                throw new ArgumentException("Invalid argument", nameof(resourceType));
            }

            GadgetInfo info = Find(resourceType);
            if (info == null)
            {
                throw new ArgumentException("Invalid argument", nameof(resourceType));
            }

            lock (info)
            {
                if (info.GadgetAssembly?.IsLoaded == true)
                {
                    info.GadgetAssembly.Unload();
                }
            }
        }

        /// <summary>
        /// Adds a Gadget to the GadgetManager.
        /// </summary>
        /// <param name="resourceType">The resource type of the Gadget package.</param>
        /// <param name="className">The class name of the Gadget.</param>
        /// <returns>The Gadget object.</returns>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid argument.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static Gadget Add(string resourceType, string className) => Add(resourceType, className, true);

        /// <summary>
        /// Adds a Gadget to the GadgetManager.
        /// </summary>
        /// <remarks>
        /// To use Unload() method, the useDefaultContext must be'false'.
        /// </remarks>
        /// <param name="resourceType">The resource type of the Gadget package.</param>
        /// <param name="className">The class name of the Gadget.</param>
        /// <param name="useDefaultContext">The flag it true, use a default context. Otherwise, use a new load context.</param>
        /// <returns>The Gadget object.</returns>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid argument.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static Gadget Add(string resourceType, string className, bool useDefaultContext)
        {
            Log.Info("BEGIN");
            var gadget = CreateInstance(resourceType, className, useDefaultContext);
            if (gadget != null)
            {
                PreCreate(gadget);
                Create(gadget);
            }
            Log.Info("END");
            return gadget;
        }

        /// <summary>
        /// Retrieves the instances of currently running Gadgets.
        /// </summary>
        /// <returns>An enumerable list containing all the active Gadgets.</returns>
        /// <since_tizen> 13 </since_tizen>
        public static IEnumerable<Gadget> GetGadgets() => _gadgets.Keys;

        /// <summary>
        /// Retrieves information about available Gadgets.
        /// </summary>
        /// <remarks>
        /// This method provides details on gadgets that are currently accessible rather than listing all installed gadgets.
        /// A Gadget's resource package may specify which applications have access through the "allowed-packages" setting.
        /// During execution, the platform mounts the resource package in the application's resources directory.
        /// </remarks>
        /// <returns>An enumerable list of GadgetInfo objects.</returns>
        /// <since_tizen> 13 </since_tizen>
        public static IEnumerable<GadgetInfo> GetGadgetInfos() => _gadgetInfos.Values;


        /// <summary>
        /// Creates a new Gadget instance.
        /// </summary>
        /// <remarks>
        /// To use Unload() method, the useDefaultContext must be'false'.
        /// </remarks>
        /// <param name="resourceType">The resource type of the Gadget package.</param>
        /// <param name="className">The class name of the Gadget.</param>
        /// <param name="useDefaultContext">The flag it true, use a default context. Otherwise, use a new load context.</param>
        /// <returns>The Gadget object.</returns>
        /// <exception cref="ArgumentException">Thrown when failed because of a invalid argument.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static Gadget CreateInstance(string resourceType, string className, bool useDefaultContext)
        {
            if (string.IsNullOrWhiteSpace(resourceType) || string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("Invalid argument");
            }

            GadgetInfo info = Find(resourceType);
            LoadInternal(info, useDefaultContext);

            Gadget gadget = null;
            if (GadgetFactory != null)
            {
                Log.Info("Use GadgetFactory");
                gadget = GadgetFactory.CreateInstance(info, className, useDefaultContext);
            }
            else
            {
                Log.Info("Default Creation");
                gadget = useDefaultContext ? info.Assembly.CreateInstance(className, true) as Gadget : info.GadgetAssembly.CreateInstance(className) as Gadget;
            }

            if (gadget == null)
            {
                throw new InvalidOperationException("Failed to create instance. className: " + className);
            }

            gadget.GadgetInfo = info;
            gadget.ClassName = className;
            gadget.GadgetResourceManager = new GadgetResourceManager(info);
            gadget.LifecycleChanged += OnGadgetLifecycleChanged;
            return gadget;
        }

        /// <summary>
        /// Executes the pre-creation process of the Gadget.
        /// </summary>
        /// <param name="gadget">The Gadget object to perform the pre-creation process.</param>
        /// <exception cref="ArgumentNullException">Thrown if the 'gadget' argument is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void PreCreate(Gadget gadget)
        {
            if (gadget == null)
            {
                throw new ArgumentNullException(nameof(gadget));
            }

            Log.Warn("ResourceType: " + gadget.GadgetInfo.ResourceType + ", State: " + gadget.State);
            gadget.PreCreate();
        }

        /// <summary>
        /// Executes the creation process of the Gadget.
        /// </summary>
        /// <param name="gadget">The Gadget object to perform the creation process.</param>
        /// <exception cref="ArgumentNullException">Thrown if the 'gadget' argument is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when failed because of an invalid operation.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void Create(Gadget gadget)
        {
            if (gadget == null)
            {
                throw new ArgumentNullException(nameof(gadget));
            }

            if (_gadgets.ContainsKey(gadget))
            {
                Log.Error("Already exists. ResourceType:" + gadget.GadgetInfo.ResourceType);
                return;
            }

            Log.Warn("ResourceType: " + gadget.GadgetInfo.ResourceType + ", State: " + gadget.State);
            if (!gadget.Create())
            {
                throw new InvalidOperationException("The View MUST be created");
            }
            _gadgets.TryAdd(gadget, 0);
        }

        /// <summary>
        /// Removes the specified Gadget from the GadgetManager.
        /// </summary>
        /// <param name="gadget">The Gadget object that needs to be removed.</param>
        /// <remarks>
        /// This method allows you to remove a specific Gadget from the GadgetManager.
        /// By passing the Gadget object as an argument, you can ensure that only the desired gadget is removed.
        /// It is important to note that once a gadget is removed, any references to it become invalid.
        /// Therefore, it is crucial to handle the removal process carefully to avoid any potential issues.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public static void Remove(Gadget gadget)
        {
            if (gadget == null || !_gadgets.ContainsKey(gadget) || gadget.State == GadgetLifecycleState.Destroyed)
            {
                return;
            }

            _gadgets.TryRemove(gadget, out _);
            CoreApplication.Post(() =>
            {
                Log.Warn("ResourceType: " + gadget.GadgetInfo.ResourceType + ", State: " + gadget.State);
                gadget.Finish();
            });
        }

        /// <summary>
        /// Removes all Gadgets from the GadgetManager.
        /// </summary>
        /// <remarks>
        /// This method is called to remove all Gadgets that are currently registered in the GadgetManager.
        /// It ensures that no more Gadgets exist after calling this method.
        /// </remarks>
        /// <since_tizen> 13 </since_tizen>
        public static void RemoveAll()
        {
            foreach (var gadget in _gadgets.Keys.ToList())
            {
                Remove(gadget);
            }
        }

        /// <summary>
        /// Resumes the execution of the specified Gadget.
        /// </summary>
        /// <remarks>
        /// By calling this method, you can resume the execution of the currently suspended Gadget.
        /// It takes the Gadget object as an argument which represents the target gadget that needs to be resumed.
        /// </remarks>
        /// <param name="gadget">The Gadget object whose execution needs to be resumed.</param>
        /// <exception cref="ArgumentNullException">Thrown if the 'gadget' argument is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void Resume(Gadget gadget)
        {
            if (gadget == null)
            {
                throw new ArgumentNullException(nameof(gadget));
            }

            if (!_gadgets.ContainsKey(gadget))
            {
                return;
            }

            CoreApplication.Post(() =>
            {
                Log.Warn("ResourceType: " + gadget.GadgetInfo.ResourceType + ", State: " + gadget.State);
                gadget.Resume();
            });
        }

        /// <summary>
        /// Pauses the execution of the specified Gadget.
        /// </summary>
        /// <remarks>
        /// Calling this method pauses the currently executing Gadget. It does not affect any other gadgets that may be running simultaneously.
        /// </remarks>
        /// <param name="gadget">The Gadget object whose execution needs to be paused.</param>
        /// <exception cref="ArgumentNullException">Thrown if the argument 'gadget' is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void Pause(Gadget gadget)
        {
            if (gadget == null)
            {
                throw new ArgumentNullException(nameof(gadget));
            }

            if (!_gadgets.ContainsKey(gadget))
            {
                return;
            }

            CoreApplication.Post(() =>
            {
                Log.Warn("ResourceType: " + gadget.GadgetInfo.ResourceType + ", State: " + gadget.State);
                gadget.Pause();
            });
        }

        /// <summary>
        /// Sends the specified application control to the currently running Gadget.
        /// </summary>
        /// <param name="gadget">The Gadget object that will receive the app control.</param>
        /// <param name="appControl">The app control object containing the desired arguments and actions.</param>
        /// <exception cref="ArgumentException">Thrown if any of the arguments are invalid or missing.</exception>
        /// <exception cref="ArgumentNullException">Thrown if either 'gadget' or 'appControl' is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void SendAppControl(Gadget gadget, AppControl appControl)
        {
            if (gadget == null)
            {
                throw new ArgumentNullException(nameof(gadget));
            }

            if (!_gadgets.ContainsKey(gadget))
            {
                throw new ArgumentException("Invalid argument", nameof(gadget));
            }

            if (appControl == null)
            {
                throw new ArgumentNullException(nameof(appControl));
            }

            gadget.HandleAppControlReceivedEvent(new AppControlReceivedEventArgs(new ReceivedAppControl(appControl.SafeAppControlHandle)));
        }

        internal static bool HandleAppControl(AppControlReceivedEventArgs args)
        {
            var extraData = args.ReceivedAppControl?.ExtraData;
            if (extraData == null || !extraData.TryGet("__K_GADGET_RES_TYPE", out string resourceType) ||
                !extraData.TryGet("__K_GADGET_CLASS_NAME", out string className))
            {
                return false;
            }

            foreach (var gadget in _gadgets.Keys)
            {
                if (gadget.GadgetInfo.ResourceType == resourceType && gadget.ClassName == className)
                {
                    gadget.HandleAppControlReceivedEvent(args);
                    return true;
                }
            }

            return false;
        }

        internal static void HandleEvents(GadgetEventType eventType, EventArgs args)
        {
            foreach (Gadget gadget in _gadgets.Keys)
            {
                gadget.HandleEvents(eventType, args);
            }
        }

        /// <summary>
        /// Occurs when the message is received.
        /// </summary>
        /// <since_tizen> 13 </since_tizen>
        public static event EventHandler<GadgetMessageReceivedEventArgs> GadgetMessageReceived;

        /// <summary>
        /// Sends the message to the GadgetManager.
        /// </summary>
        /// <param name="message">The message</param>
        /// <exception cref="ArgumentNullException">Thrown if either 'envelope' is null.</exception>
        /// <since_tizen> 13 </since_tizen>
        public static void SendMessage(Bundle message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            CoreApplication.Post(() =>
            {
                GadgetMessageReceived?.Invoke(null, new GadgetMessageReceivedEventArgs(message));
            });
        }

        /// <summary>
        /// Property to store and retrieve the instance of IGadgetFactory.
        /// </summary>
        /// <details>
        /// When this property is set, The CreateInstance() method uses this property to create a gadget instance.
        /// </details>
        /// <since_tizen> 13 </since_tizen>
        public static IGadgetFactory GadgetFactory { get; set; }
    }
}
