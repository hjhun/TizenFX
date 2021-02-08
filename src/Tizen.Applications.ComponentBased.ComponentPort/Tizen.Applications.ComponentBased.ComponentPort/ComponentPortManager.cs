using System;

namespace Tizen.Applications.ComponentBased
{
    /// <summary>
    /// This class has the methods and events of the ComponentPortManager.
    /// </summary>
    /// <since_tizen> 9 </since_tizen>
    public class ComponentPortManager : IDisposable
    {
        private EventHandler<PortAppearedEventArgs> _appearedHandler;
        private EventHandler<PortVanishedEventArgs> _vanishedHandler;
        private Interop.ComponentPort.ComponentPortAppearedCallback _appearedCallback;
        private Interop.ComponentPort.ComponentPortVanishedCallback _vanishedCallback;
        private readonly object _lock = new object();
        private uint _watcherId = 0;

        /// <summary>
        /// Constructor for this class.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the argument is invalid.</exception>
        /// <param name="endpoint">The name of the port.</param>
        /// <since_tizen> 9 </since_tizen>
        public ComponentPortManager(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentException("Invalid argument");
            }

            Endpoint = endpoint;
            _appearedCallback = new Interop.ComponentPort.ComponentPortAppearedCallback(OnPortAppeared);
            _vanishedCallback = new Interop.ComponentPort.ComponentPortVanishedCallback(OnPortVanished);
        }

        /// <summary>
        /// Gets the endpoint.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public string Endpoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks whether the port is running or not.
        /// </summary>
        /// <exception cref="global::System.IO.IOException">Thrown when because of I/O error.</exception>
        /// /// <param name="endpoint">The name of the port.</param>
        /// <since_tizen> 9 </since_tizen>
        public static bool IsRunning(string endpoint)
        {
            Interop.ComponentPort.ErrorCode err = Interop.ComponentPort.IsRunning(endpoint, out bool IsRunning);
            if (err != Interop.ComponentPort.ErrorCode.None)
            {
                throw new global::System.IO.IOException("Failed to check whether the port is running or not.");
            }
            return IsRunning;
        }

        /// <summary>
        /// Occurs whenever the port is appeared.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public event EventHandler<PortAppearedEventArgs> PortAppeared
        {
            add
            {
                lock (_lock)
                {
                    _appearedHandler += value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _appearedHandler -= value;
                }
            }
        }

        /// <summary>
        /// Occurs whenever the port is vanished.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public event EventHandler<PortVanishedEventArgs> PortVanished
        {
            add
            {
                lock (_lock)
                {
                    _vanishedHandler += value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _vanishedHandler -= value;
                }
            }
        }

        /// <summary>
        /// Starts watching the endpoint.
        /// </summary>
        /// <exception cref="global::System.IO.IOException">Thrown when because of I/O error.</exception>
        /// <since_tizen> 9 </since_tizen>
        public void Watch()
        {
            if (_watcherId == 0)
            {
                Interop.ComponentPort.ErrorCode err = Interop.ComponentPort.Watch(Endpoint, _appearedCallback, _vanishedCallback, IntPtr.Zero, out _watcherId);
                if (err != Interop.ComponentPort.ErrorCode.None)
                {
                    throw ComponentPort.ComponentPortErrorFactory.GetException(err, "Failed to watch " + Endpoint);
                }
            }
        }

        /// <summary>
        /// Stops watching the endpoint.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        public void Unwatch()
        {
            if (_watcherId != 0)
            {
                Interop.ComponentPort.Unwatch(_watcherId);
                _watcherId = 0;
            }
        }

        private void OnPortAppeared(string endpoint, int owner, IntPtr userData)
        {
            lock (_lock)
            {
                _appearedHandler?.Invoke(null, new PortAppearedEventArgs(endpoint, owner));
            }
        }

        private void OnPortVanished(string endpoint, IntPtr userData)
        {
            lock (_lock)
            {
                _vanishedHandler?.Invoke(null, new PortVanishedEventArgs(endpoint));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Releases any unmanaged resources used by this object. Can also dispose any other disposable objects.
        /// </summary>
        /// <param name="disposing">If true, disposes any disposable objects, or false not to dispose disposable objects.</param>
        /// <since_tizen> 9 </since_tizen>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_watcherId != 0)
                        Unwatch();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Finalizer of the class ComponentPortManager.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>
        ~ComponentPortManager()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Releases all the resources used by the class ComponentPortManager.
        /// </summary>
        /// <since_tizen> 9 </since_tizen>>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
