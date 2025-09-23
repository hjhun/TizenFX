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
using System.Globalization;
using Tizen.Applications;

namespace Tizen.NUI
{
    /// <summary>
    /// Manages resources related to NUI gadgets.
    /// </summary>
    /// <since_tizen> 10 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NUIGadgetResourceManager
    {
        private GadgetResourceManager GadgetResourceManager { get; set; }

        /// <summary>
        /// Initializes the resource manager of the gadget.
        /// </summary>
        /// <param name="info">The information of the gadget.</param>
        /// <exception cref="ArgumentNullException">Thrown when the argument is not valid.</exception>
        /// <since_tizen> 10 </since_tizen>
        public NUIGadgetResourceManager(NUIGadgetInfo info)
        {
            GadgetResourceManager = new GadgetResourceManager(info.GadgetInfo);
        }

        /// <summary>
        /// Initializes the resource manager of the gadget.
        /// </summary>
        /// <param name="resourcePath">The path where the resources are located.</param>
        /// <param name="resourceDll">The name of the DLL containing the resources.</param>
        /// <param name="resourceClassName">The name of the class that represents the resources.</param>
        /// <since_tizen> 10 </since_tizen>
        public NUIGadgetResourceManager(string resourcePath, string resourceDll, string resourceClassName)
        {
            GadgetResourceManager = new GadgetResourceManager(resourcePath, resourceDll, resourceClassName);
        }

        /// <summary>
        /// Retrieves the value of the specified string resource.
        /// </summary>
        /// <param name="name">The unique identifier for the string resource to retrieve.</param>
        /// <returns>The value of the requested string resource, or null if no matching resource could be found.</returns>
        /// <remarks>
        /// This function allows you to access localized string resources by providing their names.
        /// It returns the actual value of the requested resource, which can then be displayed to users or used elsewhere in your application logic.
        /// If the specified resource does not exist or cannot be found, the function will return null instead.
        /// </remarks>
        /// <example>
        /// Here's an example demonstrating how to retrieve a string resource named "greeting" from the current context:
        ///
        /// <code>
        /// // Retrieve the greeting message
        /// string greetingMessage = GetString("greeting");
        ///
        /// // Display the greeting message to the user
        /// Console.WriteLine(greetingMessage);
        /// </code>
        /// </example>
        /// <since_tizen> 10 </since_tizen>
        public string GetString(string name)
        {
            return GetString(name, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Retrieves the localized string resource for the specified culture.
        /// </summary>
        /// <remarks>
        /// This method enables you to obtain a localized version of a specific string resource based on the provided culture.
        /// It returns the desired resource value or null if the requested resource cannot be found in the resource set.
        /// </remarks>
        /// <param name="name">The name of the resource to fetch.</param>
        /// <param name="cultureInfo">An object representing the culture for which the resource needs to be localized.</param>
        /// <returns>The localized string resource for the specified culture, or null if the resource cannot be found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when an invalid argument causes failure.</exception>
        /// <since_tizen> 10 </since_tizen>
        public string GetString(string name, CultureInfo cultureInfo)
        {
            return GadgetResourceManager.GetString(name, cultureInfo);
        }
    }
}
