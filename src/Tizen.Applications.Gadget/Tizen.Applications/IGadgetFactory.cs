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

using System.ComponentModel;

namespace Tizen.Applications
{
    /// <summary>
    /// An interface that make the Gadget object.
    /// </summary>
    /// <since_tizen> 13 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IGadgetFactory
    {
        /// <summary>
        /// Creates a new Gadget instance.
        /// </summary>
        /// <param name="info">The gadget information.</param>
        /// <param name="className">The class name of the Gadget.</param>
        /// <param name="useDefaultContext">The flag it true, use a default context. Otherwise, use a new load context.</param>
        /// <returns>A new Gadget instance</returns>
        /// <since_tizen> 13 </since_tizen>
        Gadget CreateInstance(GadgetInfo info, string className, bool useDefaultContext);
    }
}
