using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Business.Interfaces;
using AttendanceSystem.Services.Interfaces;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        IEmployeeBusiness _business;

        public EmployeeServices(IEmployeeBusiness business)
        {
            _business = business;
        }
        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _business.GetAllEmployeesAsync();
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return await _business.GetEmployeeByIdAsync(id);
        }
        public async Task<Boolean> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            employeeDto.Id = 0;
            await _business.AddEmployeeAsync(employeeDto);
            return true;
        }
        public async Task<Boolean> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            await _business.UpdateEmployeeAsync(employeeDto);
            return true;
        }
        public async Task<Boolean> DeleteEmployeeByIdAsync(int id)
        {
            await _business.DeleteEmployeeByIdAsync(id);
            return true;

        }
    }
}
