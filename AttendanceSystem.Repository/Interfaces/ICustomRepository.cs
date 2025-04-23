using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Data.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Repository.Interfaces
{
   public interface ICustomRepository
    {
        public Task<List<Employee>> GetAllEmployeeDeatailsAsync();
        public Task<List<Attendance>> GetAttendanceHistoryAsync();
        public Task<List<Attendance>> GetAttendanceHistoryAsync(int year);
        public Task<List<Attendance>> GetAttendanceHistoryAsync(int year, int month);
        public Task<List<Attendance>> GetAttendanceHistoryAsync(int year, int month, int employeeId);
    }
}
