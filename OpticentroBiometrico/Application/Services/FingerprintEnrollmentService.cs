using libzkfpcsharp;
using OpticentroBiometrico.Common;
using System;
using System.Threading;

namespace OpticentroBiometrico.Application.Services
{
    internal class FingerprintEnrollmentService
    {
        private readonly IntPtr _deviceHandle;
        private const int ImgBufferSize = 200000;
        private const int TemplateBufferSize = 2048;
        private const int MinQualityScore = 60;

        public FingerprintEnrollmentService(IntPtr deviceHandle)
        {
            _deviceHandle = deviceHandle;
        }

        private const int MaxConsecutiveErrors = 5;

        public string CaptureTemplate(IProgress<string> progress = null, IProgress<int> progressStep = null)
        {
            if (_deviceHandle == IntPtr.Zero)
                return null;

            byte[][] templates = new byte[3][];
            int[] sizes = new int[3];

            for (int captureIndex = 0; captureIndex < 3; captureIndex++)
            {
                progress?.Report($"Captura {captureIndex + 1} de 3 — Coloque el dedo en el sensor...");

                var imgBuffer = new byte[ImgBufferSize];
                var tpBuffer = new byte[TemplateBufferSize];
                bool captured = false;
                int consecutiveErrors = 0;

                for (int attempt = 0; attempt < 200; attempt++)
                {
                    int tpSize = tpBuffer.Length;

                    int ret = zkfp2.AcquireFingerprint(_deviceHandle, imgBuffer, tpBuffer, ref tpSize);

                    if (ret == zkfp.ZKFP_ERR_OK)
                    {
                        consecutiveErrors = 0;

                        int quality = GetLastCaptureQuality();

                        if (quality >= 0 && quality < MinQualityScore)
                        {
                            Logger.Log($"Captura {captureIndex + 1}: calidad insuficiente ({quality}/{MinQualityScore}), reintentando...");
                            progress?.Report($"Calidad baja ({quality}%) — Retire y vuelva a colocar el dedo...");
                            Thread.Sleep(1500);
                            continue;
                        }

                        templates[captureIndex] = new byte[tpSize];
                        Array.Copy(tpBuffer, templates[captureIndex], tpSize);
                        sizes[captureIndex] = tpSize;
                        captured = true;

                        string qualityInfo = quality >= 0 ? $" (calidad: {quality}%)" : "";
                        progress?.Report($"Captura {captureIndex + 1} aceptada{qualityInfo}.");
                        progressStep?.Report(captureIndex + 1);
                        break;
                    }

                    if (ret == -8)
                    {
                        consecutiveErrors = 0;
                    }
                    else
                    {
                        consecutiveErrors++;
                        Logger.Log($"AcquireFingerprint intento {attempt + 1}, código: {ret}");

                        if (consecutiveErrors >= MaxConsecutiveErrors)
                        {
                            Logger.Log($"Dispositivo inaccesible tras {consecutiveErrors} errores consecutivos (último código: {ret}).");
                            progress?.Report("Error: el dispositivo parece haberse desconectado. Reconecte y vuelva a intentar.");
                            return null;
                        }
                    }

                    Thread.Sleep(50);
                }

                if (!captured)
                {
                    Logger.Log($"Timeout en captura {captureIndex + 1}: no se detectó huella en el tiempo esperado.");
                    progress?.Report($"Captura {captureIndex + 1} fallida. Tiempo agotado.");
                    return null;
                }

                if (captureIndex < 2)
                {
                    progress?.Report($"Captura {captureIndex + 1} OK — Retire el dedo...");
                    Thread.Sleep(1500);
                }
            }

            IntPtr dbHandle = zkfp2.DBInit();
            if (dbHandle == IntPtr.Zero)
            {
                Logger.Log("DBInit falló al crear el motor de comparación para merge.");
                return null;
            }

            try
            {
                byte[] finalTemplate = new byte[TemplateBufferSize];
                int finalSize = TemplateBufferSize;

                int mergeResult = zkfp2.DBMerge(
                    dbHandle,
                    templates[0],
                    templates[1],
                    templates[2],
                    finalTemplate,
                    ref finalSize);

                if (mergeResult != zkfp.ZKFP_ERR_OK)
                {
                    Logger.Log($"DBMerge falló con código: {mergeResult}");
                    return null;
                }

                progress?.Report("Plantilla generada correctamente.");
                return zkfp2.BlobToBase64(finalTemplate, finalSize);
            }
            finally
            {
                zkfp2.DBFree(dbHandle);
            }
        }

        private int GetLastCaptureQuality()
        {
            var paramBuf = new byte[4];
            int size = 4;
            int ret = zkfp2.GetParameters(_deviceHandle, 1, paramBuf, ref size);
            if (ret != zkfp.ZKFP_ERR_OK)
                return -1;
            int quality = 0;
            zkfp2.ByteArray2Int(paramBuf, ref quality);
            return quality;
        }
    }
}
