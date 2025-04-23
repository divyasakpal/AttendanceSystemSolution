using AttendanceSystem.Data.DomainEntities;
using AttendanceSystem.Shared.DTOs;
using AutoMapper;

namespace AttendanceSystem.Business.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ForMember(dto => dto.ClockTime, map => map.MapFrom(db => db.UtcDateTime.ToLocalTime()))
                                                  .ForMember(dto => dto.Status, map => map.MapFrom(db => db.status == true ? 1 : 0))
                                                  .ForMember(dto => dto.Id, map => map.MapFrom(db=>db.AttendanceId));
            CreateMap<AttendanceDto, Attendance>().ForMember(db => db.UtcDateTime, map => map.MapFrom(dto => dto.ClockTime.ToUniversalTime()))
                                                 .ForMember(db => db.status, map => map.MapFrom(dto => Convert.ToBoolean((int)dto.Status)))
                                                 .ForMember(db => db.AttendanceId, map => map.MapFrom(dto => dto.Id));
        }
    }
}

