using OpticentroBiometrico.Domain.Models;
using OpticentroBiometrico.Intrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticentroBiometrico.Application.Services
{
    internal class EmployeeService
    {
        private readonly EmployeeRepository _repository;
        public EmployeeService()
        {
            _repository = new EmployeeRepository();
        }
        public List<Employee> GetEmployees()
        {
            return _repository.GetActiveEmployees();
        }
    }
}
