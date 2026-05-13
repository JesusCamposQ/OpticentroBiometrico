using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Common
{
    internal class Logger
    {
        public static void Log(String message) {
            string path = "log.txt";
            string fullMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";
            File.AppendAllText(path, fullMessage);
        }
    }
}
