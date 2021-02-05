/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Threading;

namespace Tizen.Applications.ComponentBased
{
    /// <summary>
    /// Provides a task class for the Tizen component-based application model.
    /// </summary>
    /// <since_tizen> 9 </since_tizen>
    public class ComponentTask
    {
        private Thread _thread = null;
        private readonly ComponentPort _port = null;

        /// <summary>
        /// Initializes the instance of the ComponentTask class.
        /// </summary>
        /// <param name="port">The component port object</param>
        /// <since_tizen> 9 </since_tizen>
        public ComponentTask(ComponentPort port)
        {
            _port = port;
            _thread = new Thread(new ThreadStart(OnThread));
        }

        /// <summary>
        /// Finalizes the instance of the ComponentTask class.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        ~ComponentTask()
        {
            Stop();
        }

        private void OnThread()
        {
            IsRunning = true;
            _port.WaitForEvent();
            IsRunning = false;
        }

        /// <summary>
        /// Starts the task
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public void Start()
        {
            if (_thread == null)
            {
                _thread = new Thread(new ThreadStart(OnThread));
                _thread.Start();
            }
        }

        /// <summary>
        /// Stops the task
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public void Stop()
        {
            if (_thread != null)
            {
                _port.Cancel();
                _thread.Join();
                _thread = null;
            }
        }

        /// <summary>
        /// Checks whether the component port is running or not.
        /// </summary>
        /// <return>true, if the component port is running</return>
        /// <since_tizen> 9 </since_tizen>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// Gets the component port
        /// </summary>
        /// <return>The instance of the component port</return>
        /// <since_tizen> 9 </since_tizen>
        public ComponentPort Port
        {
            get
            {
                return _port;
            }
        }
    }
}
