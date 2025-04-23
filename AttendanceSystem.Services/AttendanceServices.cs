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
    public class AttendanceServices : IAttendanceServices
    {
        IAttendanceBusiness _business;

        public AttendanceServices(IAttendanceBusiness business)
        {
            _business = business;
        }
        public async Task<Boolean> AddAttendanceStatusAsync(AttendanceDto attendanceDto)
        {
            return await _business.AddAttendanceStatusAsync(attendanceDto);
        }

        public async Task<List<AttendanceDto>> getAllAttendanceStatusAsync()
        {
            return await _business.getAllAttendanceStatusAsync();
        }
    }
}
