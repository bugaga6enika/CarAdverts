using CarAdverts.Application.Configurations.AutoMapper;

namespace CarAdverts.Application.UnitTests.Mappings
{
    public abstract class MappingsBase
    {
        protected readonly IMapper Mapper;

        public MappingsBase()
        {
            var config = new MapperConfiguration(Configurator.RegisterMappings);
            Mapper = config.CreateMapper();
        }
    }
}