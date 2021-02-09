using AutoMapper;
using gesteco.api.src.ExternalServices.ThinkWhere.Domains;
using gesteco.api.src.gesteco.WebApi.OutputModels;

namespace gesteco.api.src.ExternalServices.ThinkWhere.Mappings {
    public class TokenRequetProfile : Profile
    {
        public TokenRequetProfile()
        {
            CreateMap<LoginDTO, TokenRequestDto>()
                .ForMember(x => x.API_CORRELATION_ID, opt => opt.MapFrom(src => src.ApplicationId))
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Identifiant))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
