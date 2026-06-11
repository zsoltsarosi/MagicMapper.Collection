<img src="https://s3.amazonaws.com/automapper/logo.png" alt="MagicMapper"> 

### Based on [AutoMapper.Collection](https://github.com/AutoMapper/AutoMapper.Collection/tree/5301edbc689a9696bdd7c396452669434e75989a)

# MagicMapper.Collection
Adds ability to map collections to existing collections without re-creating the collection object.

Will Add/Update/Delete items from a preexisting collection object based on user defined equivalency between the collection's generic item type from the source collection and the destination collection.

[![NuGet](http://img.shields.io/nuget/v/MagicMapper.Collection.svg)](https://www.nuget.org/packages/MagicMapper.Collection/)

## How to add to MagicMapper?
Call AddCollectionMappers when configuring
```
Mapper.Initialize(cfg =>
{
    cfg.AddCollectionMappers();
    // Configuration code
});
```
Will add new IObjectMapper objects into the master mapping list.

## Adding equivalency between two classes
Adding equivalence to objects is done with EqualityComparison extended from the IMappingExpression class.
```
cfg.CreateMap<OrderItemDTO, OrderItem>().EqualityComparison((odto, o) => odto.ID == o.ID);
```
Mapping OrderDTO back to Order will compare Order items list based on if their ID's match
```
Mapper.Map<List<OrderDTO>,List<Order>>(orderDtos, orders);
```
If ID's match, then MagicMapper will map OrderDTO to Order

If OrderDTO exists and Order doesn't, then MagicMapper will add a new Order mapped from OrderDTO to the collection

If Order exists and OrderDTO doesn't, then MagicMapper will remove Order from collection

## Why update collection? Just recreate it 
ORMs don't like setting the collection, so you need to add and remove from preexisting one.

This automates the process by just specifying what is equal to each other.

## Can it just figure out the ID equivalency for me in Entity Framework?
`MagicMapper.Collection.EntityFramework` or `MagicMapper.Collection.EntityFrameworkCore` can do that for you.

```
Mapper.Initialize(cfg =>
{
    cfg.AddCollectionMappers();
// entity framework
    cfg.SetGeneratePropertyMaps<GenerateEntityFrameworkPrimaryKeyPropertyMaps<DB>>();
// entity framework core
cfg.SetGeneratePropertyMaps<GenerateEntityFrameworkCorePrimaryKeyPropertyMaps<DB>>();
    // Configuration code
});
```
User defined equality expressions will overwrite primary key expressions.

## What about comparing to a single existing Entity for updating?
MagicMapper.Collection.EntityFramework does that as well through extension method from of DbSet<TEntity>.

Translate equality between dto and EF object to an expression of just the EF using the dto's values as constants.
```
dbContext.Orders.Persist().InsertOrUpdate<OrderDTO>(newOrderDto);
dbContext.Orders.Persist().InsertOrUpdate<OrderDTO>(existingOrderDto);
dbContext.Orders.Persist().Remove<OrderDTO>(deletedOrderDto);
dbContext.SubmitChanges();
```
**Note:** This is done by converting the OrderDTO to Expression<Func<Order,bool>> and using that to find matching type in the database.  You can also map objects to expressions as well.

Persist doesn't call submit changes automatically

## Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [MagicMapper.Collection](https://www.nuget.org/packages/MagicMapper.Collection/) from the package manager console:
```
PM> Install-Package MagicMapper.Collection
```

### Additional packages

#### MagicMapper Collection for Entity Framework
```
PM> Install-Package MagicMapper.Collection.EntityFramework
```

#### MagicMapper Collection for Entity Framework Core
```
PM> Install-Package MagicMapper.Collection.EntityFrameworkCore
```

#### MagicMapper Collection for LinqToSQL
```
PM> Install-Package MagicMapper.Collection.LinqToSQL
```
