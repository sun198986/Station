using AutoMapper;
using Station.Entity.DB2Admin;

namespace Station.Models.RegistDto
{
    public class RegistProfile:Profile
    {
        public RegistProfile()
        {
            CreateMap<Regist, RegistDto>()
            .ForMember(dest => dest.TelPhone,
            opt => opt.MapFrom(src => src.Phone));

            CreateMap<RegistAddDto, Regist>()
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.TelPhone));

            CreateMap<RegistUpdateDto, Regist>()
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.TelPhone));

            //CreateMap<Regist, RegistDto>().ForAllMembers(opt => opt.Condition(srs => srs!=null));
        }
    }
}