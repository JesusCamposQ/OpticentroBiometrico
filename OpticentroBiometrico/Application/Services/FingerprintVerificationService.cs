using libzkfpcsharp;
using OpticentroBiometrico.Common;
using System;
using System.Collections.Generic;

namespace OpticentroBiometrico.Application.Services
{
    internal class FingerprintVerificationService : IDisposable
    {
        private IntPtr _dbHandle;
        private readonly Dictionary<int, string> _fidToEmployeeId = new Dictionary<int, string>();
        private readonly object _identifyLock = new object();
        private bool _disposed = false;

        public bool IsReady => _dbHandle != IntPtr.Zero && _fidToEmployeeId.Count > 0;

        public FingerprintVerificationService()
        {
            _dbHandle = zkfp2.DBInit();

            if (_dbHandle == IntPtr.Zero)
                Logger.Log("FingerprintVerificationService: DBInit falló al crear la base de datos de identificación.");
        }

        public int LoadTemplates(List<(string employeeId, byte[] template)> templates)
        {
            if (_dbHandle == IntPtr.Zero)
                return 0;

            zkfp2.DBClear(_dbHandle);
            _fidToEmployeeId.Clear();

            int fid = 1;
            int loaded = 0;

            foreach (var (employeeId, template) in templates)
            {
                int ret = zkfp2.DBAdd(_dbHandle, fid, template);

                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    _fidToEmployeeId[fid] = employeeId;
                    fid++;
                    loaded++;
                }
                else
                {
                    Logger.Log($"DBAdd falló para empleado {employeeId} (fid={fid}), código: {ret}");
                }
            }

            Logger.Log($"Templates cargados: {loaded}/{templates.Count}");
            return loaded;
        }

        // capturedTemplate debe ser un array de exactamente tpSize bytes (no el buffer completo de 2048).
        public string IdentifyFingerprint(byte[] capturedTemplate)
        {
            if (_dbHandle == IntPtr.Zero || capturedTemplate == null || _fidToEmployeeId.Count == 0)
                return null;

            lock (_identifyLock)
            {
                int fid = 0;
                int score = 0;

                int ret = zkfp2.DBIdentify(_dbHandle, capturedTemplate, ref fid, ref score);

                if (ret != zkfp.ZKFP_ERR_OK)
                {
                    Logger.Log($"DBIdentify: sin coincidencia (código: {ret})");
                    return null;
                }

                Logger.Log($"DBIdentify: match fid={fid}, score={score}");

                return _fidToEmployeeId.TryGetValue(fid, out string employeeId) ? employeeId : null;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_dbHandle != IntPtr.Zero)
                {
                    zkfp2.DBFree(_dbHandle);
                    _dbHandle = IntPtr.Zero;
                }

                _disposed = true;
            }
        }
    }
}
