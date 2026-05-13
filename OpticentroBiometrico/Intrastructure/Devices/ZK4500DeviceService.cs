using libzkfpcsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Intrastructure.Devices
{
    internal class ZK4500DeviceService
    {
        private zkfp _zkfp;
        private IntPtr _deviceHandle = IntPtr.Zero;
        private IntPtr _dbHandle = IntPtr.Zero;

        public bool ConnectDevice()
        {
            _zkfp = new zkfp();

            int result = _zkfp.Initialize();

            if (result != zkfp.ZKFP_ERR_OK)
                return false;

            _deviceHandle = zkfp2.OpenDevice(0);

            if (_deviceHandle == IntPtr.Zero)
                return false;

            _dbHandle = zkfp2.DBInit();

            return _dbHandle != IntPtr.Zero;
        }

        public void DisconnectDevice()
        {
            if (_deviceHandle != IntPtr.Zero)
                zkfp2.CloseDevice(_deviceHandle);

            zkfp2.Terminate();
        }
    }
}
