using MagicMapper.EquivalencyExpression;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace MagicMapper.Collection
{
    public class MapCollectionWithEqualityThreadSafetyTests
    {
        public async Task Should_Work_When_Initialized_Concurrently()
        {
            Action act = () =>
            {
                new MapperConfiguration(cfg =>
                {
                    cfg.AddCollectionMappers();
                }, new NullLoggerFactory());
            };
            var tasks = new List<Task>();
            for (var i = 0; i < 5; i++)
            {
                tasks.Add(Task.Run(act));
            }

            await Task.WhenAll(tasks.ToArray());
        }
    }
}
