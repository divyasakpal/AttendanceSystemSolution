using System.ComponentModel.DataAnnotations;
using System.Text;
using AttendanceSystem.Services.Interfaces;
using AttendanceSystem.Shared;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AttendanceSystem.API.Controllers
{
    public class AttendanceController : BaseController
    {
        IAttendanceServices _services;
        IValidator<AttendanceDto> _validator;
        ILogger<AttendanceController> _logger;
        public AttendanceController(ILogger<AttendanceController> logger,IAttendanceServices services, IValidator<AttendanceDto> validator)
        {
            _logger = logger;
            _services = services;
            _validator = validator;
        }

        /// <summary>
        /// Gets the list of all employees.
        /// </summary>
        /// <returns>The list of marked Attendace for all employees.</returns>
        // GET: Attendance
        [HttpGet]     
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                List<AttendanceDto> attendanceDtoList = await _services.getAllAttendanceStatusAsync();
                if (!attendanceDtoList.Any())
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(attendanceDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Add log-in /log-out time for employee to the attendance system.
        /// </summary>
        /// <returns>True/False.</returns>
        [HttpPost]
       public async Task<IActionResult> AddAsync([FromBody] AttendanceDto attendanceDto)
        {
            try
            {
                ValidationResult result = await _validator.ValidateAsync(attendanceDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                Boolean isAdded = await _services.AddAttendanceStatusAsync(attendanceDto);
                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }
    }
}
