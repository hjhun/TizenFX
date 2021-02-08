using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.Applications.ComponentBased
{
    /// <summary>
    /// Arguments for the event raised when the port is vanished.
    /// </summary>
    /// <since_tizen> 9 </since_tizen>
    public class PortVanishedEventArgs
    {
        internal PortVanishedEventArgs(string endpoint)
        {
            Endpoint = endpoint;
        }

        /// <summary>
        /// The name of the endpoint
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public string Endpoint
        {
            get; private set;
        }
    }
}
