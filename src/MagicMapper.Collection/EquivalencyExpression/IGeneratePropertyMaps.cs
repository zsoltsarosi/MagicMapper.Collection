using System.Collections.Generic;
using AutoMapper;

namespace MagicMapper.EquivalencyExpression
{
    public interface IGeneratePropertyMaps
    {
        IEnumerable<PropertyMap> GeneratePropertyMaps(TypeMap typeMap);
    }
}