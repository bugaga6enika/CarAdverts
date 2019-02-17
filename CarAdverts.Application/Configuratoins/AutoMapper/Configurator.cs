using AutoMapper;
using CarAdverts.Application.Configuratoins.AutoMapper.Profiles;

namespace CarAdverts.Application.Configuratoins.AutoMapper
{
    internal static class Configurator
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarAdvertDomainToDtoProfile());
            });
        }
    }
}
