# MagicMapper.Collection

[![NuGet](https://img.shields.io/nuget/v/MagicMapper.Collection.svg)](https://www.nuget.org/packages/MagicMapper.Collection/)

Adds collection mapping support to [MagicMapper](https://www.nuget.org/packages/MagicMapper/). Maps collections to existing collections without recreating the collection object — items are added, updated, or removed based on a user-defined equivalency expression.

Based on [AutoMapper.Collection](https://github.com/AutoMapper/AutoMapper.Collection).

## Installation

```
dotnet add package MagicMapper.Collection
```

## Setup

Call `AddCollectionMappers()` when configuring your mapper:

```csharp
var config = new MapperConfiguration(cfg =>
{
    cfg.AddCollectionMappers();
    cfg.CreateMap<OrderItemDto, OrderItem>()
       .EqualityComparison((dto, item) => dto.Id == item.Id);
});

IMapper mapper = config.CreateMapper();
```

## How it works

Given a source and destination collection, MagicMapper.Collection compares items using the equivalency expression you define:

- **Matching items** — maps the source item onto the existing destination item (update)
- **Source item with no match** — adds a new destination item to the collection
- **Destination item with no match in source** — removes it from the collection

```csharp
// orders.Items is an existing collection — it is updated in-place
mapper.Map(orderDto, order);
```

This is particularly useful with ORMs, which track collection identity and do not respond well to replacing the entire collection object.

## Defining equivalency

```csharp
cfg.CreateMap<OrderItemDto, OrderItem>()
   .EqualityComparison((dto, item) => dto.Id == item.Id);
```

## License

MIT

AutoMapper is Copyright © 2009 [Jimmy Bogard](https://jimmybogard.com/) and other contributors under the MIT license.
