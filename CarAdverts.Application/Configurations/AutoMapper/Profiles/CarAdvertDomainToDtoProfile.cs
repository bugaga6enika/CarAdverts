using AutoMapper;

namespace CarAdverts.Application.Configurations.AutoMapper.Profiles
{
    public class CarAdvertDomainToDtoProfile : Profile
    {
        public CarAdvertDomainToDtoProfile()
        {
            CreateMap<Domain.CarAdvert.CarAdvert, CarAdvert.Dtos.CarAdvertDto>()
                .ForMember(dest => dest.FirstRegistration, opt => opt.MapFrom(src => src.FirstRegistration.ToString()));

            CreateMap<Domain.CarAdvert.CarAdvert, CarAdvert.Dtos.CarAdvertListDto>()
                .ForMember(dest => dest.FirstRegistration, opt => opt.MapFrom(src => src.FirstRegistration.ToString()));
        }
    }
}
