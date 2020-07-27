using AutoMapper;
using Station.Entity.DB2Admin;

namespace Station.Models.EmployeeDto
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeDto>();
            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<EmployeeSearchDto, Employee>();
            //CreateMap<Regist, RegistDto>().ForAllMembers(opt => opt.Condition(srs => srs!=null));
        }
    }
}