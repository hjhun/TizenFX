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

using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Tizen.Applications;

using SystemIO = System.IO;

namespace Tizen.NUI
{
    /// <summary>
    /// This class provides properties to retrieve information the gadget.
    /// </summary>
    /// <since_tizen> 10 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class NUIGadgetInfo
    {
        internal GadgetInfo GadgetInfo { get; set; }

        internal NUIGadgetInfo(GadgetInfo info)
        {
            GadgetInfo = info;
            NUIGadgetAssembly = new NUIGadgetAssembly(info.GadgetAssembly);
        }

        /// <summary>
        /// Gets the package ID of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public string PackageId => GadgetInfo.PackageId;

        /// <summary>
        /// Gets the resource type of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public string ResourceType => GadgetInfo.ResourceType;

        /// <summary>
        /// Gets the resource version of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public string ResourceVersion => GadgetInfo.ResourceVersion;

        /// <summary>
        /// Gets the resource path of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public string ResourcePath => GadgetInfo.ResourcePath;

        /// <summary>
        /// Gets the gadget resource path of the gadget.
        /// </summary>
        /// <since_tizen> 12 </since_tizen>
        public string GadgetResourcePath => GadgetInfo.GadgetResourcePath;

        /// <summary>
        /// Gets the executable file of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public string ExecutableFile => GadgetInfo.ExecutableFile;

        /// <summary>
        /// Gets the metadata of the gadget.
        /// </summary>
        /// <since_tizen> 10 </since_tizen>
        public IDictionary<string, string> Metadata => GadgetInfo.Metadata;
                
        /// <summary>
        /// Gets the assembly of the gadget.
        /// </summary>
        /// <since_tizen> 12 </since_tizen>
        public Assembly Assembly => GadgetInfo.Assembly;

        /// <summary>
        /// Gets the assembly of the gadget.
        /// </summary>
        /// <since_tizen> 12 </since_tizen>
        public NUIGadgetAssembly NUIGadgetAssembly { get; private set; }
    }
}
