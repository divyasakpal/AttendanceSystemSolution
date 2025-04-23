using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Shared.DTOs
{
    public class YearAttendanceDto
    {
        public int year { get; set; }
        public List<MonthAttendanceDto> MonthAttendance { get; set; }
    }

    public class MonthAttendanceDto
    {
        public int Month { get; set; }

        public List<DayAttendanceDto> DayAttendance { get; set; }
    }

    public class DayAttendanceDto
    {
        public int Day { get; set; }
        public string DayOfWeek { get; set; }
        public List<EmployeeAttendanceDto> EmployeeAttendance { get; set; }
    }

    public class EmployeeAttendanceDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public TimeSpan? LogInTime { get; set; }
        public TimeSpan? LogOutTime { get; set; }
    }

}
