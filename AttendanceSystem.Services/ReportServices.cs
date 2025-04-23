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
   public class ReportServices : IReportServices
    {
        IReportBusiness _business;

        public ReportServices(IReportBusiness business)
        {
            _business = business;
        }

        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync()
        {
            return await _business.GetAttendanceHistoryAsync();
        }

        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year)
        {
            return await _business.GetAttendanceHistoryAsync(year);
        }

        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month)
        {
            return await _business.GetAttendanceHistoryAsync(year, month);
        }

        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month, int employeeId)
        {
            return await _business.GetAttendanceHistoryAsync(year, month, employeeId);
        }

    }
}
