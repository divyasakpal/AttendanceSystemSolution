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
    public class EmployeeBusiness : IEmployeeBusiness
    {
        IRepository<Employee> _repository;
        readonly IMapper _mapper;
        public EmployeeBusiness(IRepository<Employee> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Boolean> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _repository.AddAsync(employee);
            return true;
        }

        public async Task<Boolean> DeleteEmployeeByIdAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employeelist = await _repository.GetAllAsync();
            List<EmployeeDto> employeeDtoList = _mapper.Map<List<EmployeeDto>>(employeelist);    
            return employeeDtoList;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<Boolean> UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _repository.UpdateAsync(employee);
            return true;
        }
    }
}
