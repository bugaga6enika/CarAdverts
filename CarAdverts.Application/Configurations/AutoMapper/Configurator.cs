using AutoMapper;
using CarAdverts.Application.Configurations.AutoMapper.Profiles;

namespace CarAdverts.Application.Configurations.AutoMapper
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
