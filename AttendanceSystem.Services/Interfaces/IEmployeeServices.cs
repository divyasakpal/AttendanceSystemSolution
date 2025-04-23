using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IEmployeeServices
    {
        /// <summary>
        /// Method to get all employees 
        /// </summary>
        /// <returns>A <see cref="Task<List<EmployeeDto>>"/> All Employees </returns>
        public Task<List<EmployeeDto>> GetAllEmployeesAsync();

        /// <summary>
        /// Method to get employee by ID
        /// </summary>
        /// <param name="id">EmployeeId to identify employee</param>
        /// <returns>A <see cref="Task<EmployeeDto>"/> Employee </returns>
        public Task<EmployeeDto> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// Method to add employee 
        /// </summary>
        /// <param name="EmployeeDto">Employee entity</param>
        /// <returns>A <see cref="Task<Boolean>"/> True or False value</returns>
        public Task<Boolean> AddEmployeeAsync(EmployeeDto employeeDto);

        /// <summary>
        /// Method to update employee by ID
        /// </summary>
        /// <param name="employeeDto">Employee entity</param>
        /// <returns>A <see cref="Task<Boolean>"/> True or False value </returns>
        public Task<Boolean> UpdateEmployeeAsync(EmployeeDto employeeDto);

        /// <summary>
        /// Method to update employee by ID
        /// </summary>
        /// <param name="id">EmployeeId to identify employee</param>
        /// <returns>A <see cref="Task<Boolean>"/> True or False value </returns>
        public Task<Boolean> DeleteEmployeeByIdAsync(int id);

    }
}
