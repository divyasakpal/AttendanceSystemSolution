using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Business.Interfaces
{
    public interface IAttendanceBusiness
    {
        /// <summary>
        /// Save clock in/out status of employee 
        /// </summary>
        /// <param name="attendanceDto"></param>
        /// <returns>A <see cref="Task<Boolean></Boolean>"/> True or False </returns>
        Task<Boolean> AddAttendanceStatusAsync(AttendanceDto attendanceDto);

        /// <summary>
        /// Fetch attendance data  of all employee 
        /// </summary>
        /// <returns>A <see cref="Task<List<AttendanceDto>>"/> Attendance list </returns>
        Task<List<AttendanceDto>> getAllAttendanceStatusAsync();
    }
}
