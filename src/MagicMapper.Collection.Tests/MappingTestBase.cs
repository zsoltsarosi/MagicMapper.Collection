using System;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace MagicMapper.Collection
{
    public abstract class MappingTestBase
    {
        protected IMapper CreateMapper(Action<IMapperConfigurationExpression> cfg)
        {
            var map = new MapperConfiguration(cfg, new NullLoggerFactory());
            map.CompileMappings();

            var mapper = map.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return mapper;
        }
    }
}
