using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystem.Business.Interfaces;
using AttendanceSystem.Data.DomainEntities;
using AttendanceSystem.Repository.Interfaces;
using AttendanceSystem.Shared.DTOs;
using AutoMapper;

namespace AttendanceSystem.Business
{
    public class AttendanceBusiness : IAttendanceBusiness

    {
        readonly IRepository<Attendance> _repository;
        readonly IMapper _mapper;
        public AttendanceBusiness(IRepository<Attendance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Boolean> AddAttendanceStatusAsync(AttendanceDto attendanceDto)
        {
            Attendance attendance = _mapper.Map<Attendance>(attendanceDto);
            await _repository.AddAsync(attendance);
            return true;
        }

        public async Task<List<AttendanceDto>> getAllAttendanceStatusAsync()
        {
            var attendanceList = await _repository.GetAllAsync();
            
           List< AttendanceDto>  attendanceDtos= _mapper.Map<List<AttendanceDto>>(attendanceList);
            return attendanceDtos;
        }
    }
}
