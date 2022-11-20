AdaskoTheBeAsT.Dapper.NodaTime  [![NuGet Version](https://img.shields.io/nuget/v/AdaskoTheBeAsT.Dapper.NodaTime.svg?style=flat)](https://www.nuget.org/packages/AdaskoTheBeAsT.Dapper.NodaTime/) 
===============

Noda Time support for Dapper - unofficial fork of Dapper-NodaTime project which development stopped some time ago.
Refreshed for currently available .NET versions.


```
PM> Install-Package AdaskoTheBeAsT.Dapper.NodaTime
```

In your project startup sequence somewhere, call:
```csharp
DapperNodaTimeSetup.Register();
```

That registers all of the type handlers.  Alternatively, you can register each type handler separately if you wish.
For example:
```csharp
SqlMapper.AddTypeHandler(LocalDateTimeHandler.Default);
```

Work in progress.  Currently supports the following types:

- Instant
- LocalDateTime
- LocalDate
- LocalTime
- OffsetDateTime


Does not yet support:

- Duration
- Period
- Offset
- ZonedDateTime
- DateTimeZone

See also:  https://github.com/StackExchange/dapper-dot-net/issues/198
