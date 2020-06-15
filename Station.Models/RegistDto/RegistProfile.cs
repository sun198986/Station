using AutoMapper;
using Station.Entity.DB2Admin;

namespace Station.Models.RegistDto
{
    public class RegistProfile:Profile
    {
        public RegistProfile()
        {
            CreateMap<Regist, RegistDto>();

            //.ForMember(dest => dest.CompanyName,
            //opt => opt.MapFrom(src => src.Name));
        }
    }
}