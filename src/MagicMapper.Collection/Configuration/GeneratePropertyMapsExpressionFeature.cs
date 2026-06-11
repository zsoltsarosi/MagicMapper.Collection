using System;
using System.Collections.Generic;
using System.Linq;
using MagicMapper.Collection.Runtime;
using MagicMapper.EquivalencyExpression;
using AutoMapper.Features;
using AutoMapper.Internal;
using MagicMapper.Mappers;

namespace MagicMapper.Collection.Configuration
{
    public class GeneratePropertyMapsExpressionFeature : IGlobalFeature
    {
        private readonly ObjectToEquivalencyExpressionByEquivalencyExistingMapper _mapper;
        private readonly List<Func<Func<Type, object>, IGeneratePropertyMaps>> _generators = new List<Func<Func<Type, object>, IGeneratePropertyMaps>>();

        public GeneratePropertyMapsExpressionFeature(ObjectToEquivalencyExpressionByEquivalencyExistingMapper mapper)
        {
            _mapper = mapper;
        }

        public void Add(Func<Func<Type, object>, IGeneratePropertyMaps> creator)
        {
            _generators.Add(creator);
        }

        void IGlobalFeature.Configure(IGlobalConfiguration configurationProvider)
        {
            _mapper.Configuration = configurationProvider;
            var generators = _generators
                .Select(x => x.Invoke(configurationProvider.ServiceCtor))
                .ToList();
            configurationProvider.Features.Set(new GeneratePropertyMapsFeature(generators));
        }
    }
}
