using libzkfpcsharp;
using System;

namespace OpticentroBiometrico.Intrastructure.Devices
{
    internal class ZK4500DeviceService
    {
        private zkfp _zkfp;
        private IntPtr _deviceHandle = IntPtr.Zero;
        private IntPtr _dbHandle = IntPtr.Zero;
        private bool _initialized = false;

        public IntPtr DeviceHandle => _deviceHandle;

        public bool ConnectDevice()
        {
            _zkfp = new zkfp();

            int result = _zkfp.Initialize();

            if (result != zkfp.ZKFP_ERR_OK)
                return false;

            _initialized = true;
            _deviceHandle = zkfp2.OpenDevice(0);

            if (_deviceHandle == IntPtr.Zero)
                return false;

            _dbHandle = zkfp2.DBInit();

            return _dbHandle != IntPtr.Zero;
        }

        public void DisconnectDevice()
        {
            if (_dbHandle != IntPtr.Zero)
            {
                zkfp2.DBFree(_dbHandle);
                _dbHandle = IntPtr.Zero;
            }

            if (_deviceHandle != IntPtr.Zero)
            {
                zkfp2.CloseDevice(_deviceHandle);
                _deviceHandle = IntPtr.Zero;
            }

            if (_initialized)
            {
                zkfp2.Terminate();
                _initialized = false;
            }
        }
    }
}
