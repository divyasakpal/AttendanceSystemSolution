using AttendanceSystem.Services.Interfaces;
using AttendanceSystem.Shared;
using AttendanceSystem.Shared.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.API.Controllers
{
    public class ReportController : BaseController
    {
        IReportServices _services;
       // IValidator<AttendanceDto> _validator;
        ILogger<AttendanceController> _logger;
        public ReportController(ILogger<AttendanceController> logger, IReportServices services
           //, IValidator<AttendanceDto> validator
            )
        {
            _logger = logger;
            _services = services;
            //_validator = validator;
        }

        /// <summary>
        /// Gets the attendance history for all years.
        /// </summary>
        /// <returns>History</returns>
        [HttpGet]
        public  async Task<IActionResult> GetAsync()
        {
            try
            {
                var AttendanceList =
                    await _services.GetAttendanceHistoryAsync();
                if (!AttendanceList.Any())
                   return NotFound(Constants.ErrorNoRequestedData);
                return Ok(AttendanceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Gets the attendance history based on year.
        /// </summary>
        /// <returns>History</returns>
        [HttpGet("{year:int}")]
        public async Task<IActionResult> GetAsync(int year)
        {
            try
            {
                var AttendanceList =
                    await _services.GetAttendanceHistoryAsync(year);
                if (!AttendanceList.Any())
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(AttendanceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Gets the attendance history based on year and particular month for all employees.
        /// </summary>
        /// <returns>History</returns>
        [HttpGet("{year:int}/{month:int}")]
        public async Task<IActionResult> GetAsync(int year, int month)
        {
            try
            {
                var AttendanceList =
                    await _services.GetAttendanceHistoryAsync(year, month);
                if (!AttendanceList.Any())
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(AttendanceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

        /// <summary>
        /// Gets the attendance history based on employee.
        /// </summary>
        /// <returns>History</returns>
        [HttpGet("{year:int}/{month:int}/{employeeId:int}")]
        public async Task<IActionResult> GetAsync(int year, int month, int employeeId)
        {
            try
            {
                var AttendanceList =
                    await _services.GetAttendanceHistoryAsync(year, month);
                if (!AttendanceList.Any())
                    return NotFound(Constants.ErrorNoRequestedData);
                return Ok(AttendanceList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.ErrorOccured);
                return BadRequest(Constants.ErrorOccuredCheckLogs);
            }
        }

    }
}
