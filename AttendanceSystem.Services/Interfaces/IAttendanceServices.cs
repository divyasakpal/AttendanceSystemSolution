using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IAttendanceServices
    {
        /// <summary>
        /// Method to save clock in/out status of employee 
        /// </summary>
        /// <param name="AttendanceDto"></param>
        /// <returns>A <see cref="Task<Boolean>"/> True or False </returns>
        Task<Boolean> AddAttendanceStatusAsync(AttendanceDto attendanceDto);

        /// <summary>
        /// Method to attendance data clock in/out status of all employee 
        /// </summary>
        /// <returns>A <see cref="Task<List<AttendanceDto>>"/> Attendance </returns>
        Task<List<AttendanceDto>> getAllAttendanceStatusAsync();
    }
}
