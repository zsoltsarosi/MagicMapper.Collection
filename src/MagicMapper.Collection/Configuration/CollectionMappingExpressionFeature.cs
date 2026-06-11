using System;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Features;
using MagicMapper.Collection.Runtime;
using MagicMapper.EquivalencyExpression;

namespace MagicMapper.Collection.Configuration
{
    public class CollectionMappingExpressionFeature<TSource, TDestination> : IMappingFeature
    {
        private readonly Expression<Func<TSource, TDestination, bool>> _expression;

        public CollectionMappingExpressionFeature(Expression<Func<TSource, TDestination, bool>> expression)
        {
            _expression = expression;
        }

        public void Configure(TypeMap typeMap)
        {
            var equivalentExpression = new EquivalentExpression<TSource, TDestination>(_expression);
            typeMap.Features.Set(new CollectionMappingFeature(equivalentExpression));
        }

        public IMappingFeature Reverse()
        {
            var reverseExpression = Expression.Lambda<Func<TDestination, TSource, bool>>(_expression.Body, _expression.Parameters[1], _expression.Parameters[0]);
            return new CollectionMappingExpressionFeature<TDestination, TSource>(reverseExpression);
        }
    }
}
