using AutoMapper;
using CarAdverts.Application.Configurations.AutoMapper.Profiles;

namespace CarAdverts.Application.Configurations.AutoMapper
{
    internal static class Configurator
    {
        public static void RegisterMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile(new CarAdvertDomainToDtoProfile());
            cfg.AddProfile(new CarAdvertCommandToDtoProfile());
        }
    }
}
