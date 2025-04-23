using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Shared.DTOs;

namespace AttendanceSystem.Services.Interfaces
{
    public interface IReportServices
    {
        /// <summary>
        ///  Method to get all data
        /// </summary>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync();

        /// <summary>
        /// Method to get data on year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year);

        /// <summary>
        /// Method to get data on year,month
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month);

        /// <summary>
        /// Method to get data on year,month, employeeId
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="employeeId"></param>
        /// <returns>A <see cref="Task<List<YearAttendanceDto>>"/> List of YearAttendanceDto</returns>
        public Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month, int employeeId);
    }
}
