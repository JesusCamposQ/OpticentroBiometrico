using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Domain.Models
{
    internal class Employee
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Username { get; set; }
        public string Ci { get; set; }
        public bool IsActive { get; set; }

        public string FullName
        {
            get { return $"{Nombre} {ApPaterno} {ApMaterno}"; }
        }
    }
}
