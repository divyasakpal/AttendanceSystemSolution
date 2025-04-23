using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Data;
using AttendanceSystem.Data.DomainEntities;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace AttendanceSystem.Repository
{
    public class CustomRepository : ICustomRepository
    {
        readonly DataContext _dbContext;
        public CustomRepository(DataContext context)
        {
            _dbContext = context;
        }
        public async Task<List<Employee>> GetAllEmployeeDeatailsAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<List<Attendance>> GetAttendanceHistoryAsync()
        {
            return  await _dbContext.Attendances.ToListAsync();  
        }
        public async Task<List<Attendance>> GetAttendanceHistoryAsync(int year)
        {
            return await _dbContext.Attendances.Where(x=>x.UtcDateTime.Year== year).ToListAsync();
        }
        public async Task<List<Attendance>> GetAttendanceHistoryAsync(int year,int month)
        {
            return await _dbContext.Attendances.Where(x => x.UtcDateTime.Year == year&& x.UtcDateTime.Month== month).ToListAsync();
        }
        public async Task<List<Attendance>>  GetAttendanceHistoryAsync(int year, int month, int employeeId)
        {
            return await _dbContext.Attendances.Where(x =>x.EmployeeId==employeeId &&  x.UtcDateTime.Year == year && x.UtcDateTime.Month == month ).ToListAsync();
        }
    }
}