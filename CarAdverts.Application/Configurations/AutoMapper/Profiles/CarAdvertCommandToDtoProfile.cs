using AutoMapper;

namespace CarAdverts.Application.Configurations.AutoMapper.Profiles
{
    internal class CarAdvertCommandToDtoProfile : Profile
    {
        public CarAdvertCommandToDtoProfile()
        {
            CreateMap<CarAdvert.Commands.CreateCommand, Domain.CarAdvert.CarAdvertDto>();
            CreateMap<CarAdvert.Commands.UpdateCommand, Domain.CarAdvert.CarAdvertDto>();
        }
    }
}
