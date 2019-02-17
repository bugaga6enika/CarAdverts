using AutoMapper;

namespace CarAdverts.Application.Configuratoins.AutoMapper.Profiles
{
    public class CarAdvertDomainToDtoProfile : Profile
    {
        public CarAdvertDomainToDtoProfile()
        {
            CreateMap<Domain.CarAdvert.CarAdvert, CarAdvert.Dtos.CarAdvertDto>()
                .ForMember(dest => dest.FirstRegistrationDate, opt => opt.MapFrom(src => src.FirstRegistration.ToString()));

            CreateMap<Domain.CarAdvert.CarAdvert, CarAdvert.Dtos.CarAdvertListDto>()
                .ForMember(dest => dest.FirstRegistrationDate, opt => opt.MapFrom(src => src.FirstRegistration.ToString()));
        }
    }
}
