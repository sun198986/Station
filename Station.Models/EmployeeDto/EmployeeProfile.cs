using AutoMapper;
using Station.Entity.DB2Admin;
using Station.Models.EmployeeDto;

namespace Station.Models.RegistDto
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeDto.EmployeeDto>();

            //CreateMap<Regist, RegistDto>().ForAllMembers(opt => opt.Condition(srs => srs!=null));
        }
    }
}