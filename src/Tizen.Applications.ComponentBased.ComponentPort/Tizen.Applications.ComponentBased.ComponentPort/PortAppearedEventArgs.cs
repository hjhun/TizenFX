using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.Applications.ComponentBased
{
    /// <summary>
    /// Arguments for the event raised when the endpoint is appeared.
    /// </summary>
    /// <since_tizen> 9 </since_tizen>
    public class PortAppearedEventArgs
    {
        internal PortAppearedEventArgs(string endpoint, int owner)
        {
            Endpoint = endpoint;
            Owner = owner;
        }

        /// <summary>
        /// The name of the endpoint
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public string Endpoint
        {
            get; private set;
        }

        /// <summary>
        /// The process ID of the owner of the endpoint.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public int Owner
        {
            get; private set;
        }
    }
}
