using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Business.Interfaces;
using AttendanceSystem.Data.DomainEntities;
using AttendanceSystem.Repository;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Business
{
    public class ReportBusiness : IReportBusiness
    {
        readonly ICustomRepository _repository;
        public ReportBusiness(ICustomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync()
        {
            var Data = await _repository.GetAttendanceHistoryAsync();

           return await GetDetailsAsync(Data);
        }
        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year)
        {
            var yearData = await _repository.GetAttendanceHistoryAsync(year);
            return await GetDetailsAsync(yearData);
        }
        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month)
        {
            var monthData = await _repository.GetAttendanceHistoryAsync(year, month);
            return await GetDetailsAsync(monthData);
        }
        public async Task<List<YearAttendanceDto>> GetAttendanceHistoryAsync(int year, int month, int employeeId)
        {
            var dayData = await _repository.GetAttendanceHistoryAsync(year, month, employeeId);
            return await GetDetailsAsync(dayData);
        }

        public  async Task<List<YearAttendanceDto>> GetDetailsAsync (List<Attendance> attendances)
        {
            var employees = await _repository.GetAllEmployeeDeatailsAsync();

            var EmployeeDayRecord = attendances.GroupBy(x =>
           new { x.UtcDateTime.Date, x.EmployeeId }).Select(grp =>
           new
           {
               EmployeeId = grp.Key.EmployeeId,
               EmployeeName = employees.First(x=>x.Id== grp.Key.EmployeeId).Name,
               Date = grp.Key.Date,
               WeekDay = grp.Key.Date.DayOfWeek,
               Day = grp.Key.Date.Day,
               Month = grp.Key.Date.Month,
               Year = grp.Key.Date.Year,
               LogInTime = GetLogTime(grp.ToList(), Convert.ToBoolean((int)Clock.In)),
               LogOutTime = GetLogTime(grp.ToList(), Convert.ToBoolean((int)Clock.Out))
           }).OrderBy(x => x.Date).OrderBy(x => x.EmployeeId).ToList();

           var GrpByData=  EmployeeDayRecord.GroupBy(x => x.Year).Select(yeargrp =>
            new YearAttendanceDto
            {
                year = yeargrp.Key,
                MonthAttendance = yeargrp.GroupBy(x => x.Month).Select(monthgrp =>
            new MonthAttendanceDto
            {
                Month = monthgrp.Key,
                DayAttendance = monthgrp.GroupBy(x => new { x.Day, x.WeekDay }).Select(daygrp =>
            new DayAttendanceDto
            {
                Day = daygrp.Key.Day,
                DayOfWeek = daygrp.Key.WeekDay.ToString(),
                EmployeeAttendance = daygrp.Select(x => new EmployeeAttendanceDto { EmployeeId = x.EmployeeId, EmployeeName = x.EmployeeName, LogInTime = x.LogInTime, LogOutTime = x.LogOutTime }).ToList()
            }).ToList()
            }).ToList()
            }).ToList();

            return GrpByData;
        }
        
        TimeSpan? GetLogTime(List<Attendance> attendances, Boolean Status)
        {
            var n = attendances.Where(x => x.status == Status);
            if (n.Any())
            {
                if (Status)
                    return n.Min(x => x.UtcDateTime).TimeOfDay;
                else
                    return n.Max(x => x.UtcDateTime).TimeOfDay;
            }
            return TimeSpan.Zero;
        }
    }
}
