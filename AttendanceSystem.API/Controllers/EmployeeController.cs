using System;
using AttendanceSystem.Services.Interfaces;
using AttendanceSystem.Shared;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.API.Controllers
{

    /// <summary>
    /// only authorize user can access.
    /// </summary>
    public class EmployeeController : BaseController
    {
        IEmployeeServices _services;
        IValidator<EmployeeDto> _validator;
        ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeServices services, IValidator<EmployeeDto> validator)
        {
            _logger = logger;
            _services = services;
            _validator = validator;
        }

        /// <summary>
        /// Gets the list of all Employees.
        /// </summary>
        /// <returns>The list of all employee.</returns>
        [Authorize(Roles = "Reader")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                List<EmployeeDto> employeeList = await _services.GetAllEmployeesAsync();
                if (!employeeList.Any() )
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }


        /// <summary>
        /// Gets the Employee by Employee ID.
        /// </summary>
        /// <returns>single employee.</returns>
        [Authorize(Roles = "Reader")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                EmployeeDto employeeDto = await _services.GetEmployeeByIdAsync(id);
                if(employeeDto==null)
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Add new Employee in system.
        /// </summary>
        /// <Parameter>Employee details without ID</Parameter>
        /// <returns>True/False</returns>
        [Authorize(Roles = "Writter")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(employeeDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                Boolean isAdded = await _services.AddEmployeeAsync(employeeDto);
                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Update existing employee.
        /// </summary>
        /// <Parameter>Existing employee details</Parameter>
        /// <returns>True/False</returns>
        [Authorize(Roles = "Writter")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(employeeDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                Boolean isUpdated = await _services.UpdateEmployeeAsync(employeeDto);
                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Delete Employee by providing Employee ID.
        /// </summary>
        /// <returns>True/False</returns>
        [Authorize(Roles = "Writter")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
                try
                {
                    Boolean isRemoved = await _services.DeleteEmployeeByIdAsync(id);
                    return Ok(isRemoved);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, Constants.ErrorOccured);
                    return BadRequest(Constants.ErrorOccuredCheckLogs);
                }
            }
    }
}

