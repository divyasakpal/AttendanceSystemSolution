using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Data.DomainEntities;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Business.Interfaces
{
    public interface IReportBusiness
    {
        /// <summary>
        /// Fetch all Attendance data 
        /// </summary>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync();

        /// <summary>
        /// Fetch Attendance data with filter year,month, employeeId
        /// </summary>
        /// <param name="year"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year);

        /// <summary>
        /// Fetch Attendance data with filter year,month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month);

        /// <summary>
        /// Fetch Attendance data with filter  year,month, employeeId
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="employeeId"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month, int employeeId);
    }
}
