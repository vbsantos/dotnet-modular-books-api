# ModularMonolith Books
    
This is a learning project aimed at understanding the principles and implementation of **Modular Monoliths** in .NET 9. 

## Technologies & Packages

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses): A library for guard clauses
- [Ardalis.Result](https://github.com/ardalis/Result): A library for handling operation results
- [Dapper](https://github.com/DapperLib/Dapper): A simple ORM and EFCore alternative
- [FastEndpoints](https://fast-endpoints.com/): An alternative to minimal APIs
- [MediatR](https://github.com/jbogard/MediatR): Simple mediator pattern implementation
- [MimeKit](https://github.com/jstedfast/MimeKit): To Send Emails - Multipurpose Internet Mail Extension (MIME)
- [Scalar](https://scalar.com/): An alternative to Swagger
- [Serilog](https://serilog.net/): A diagnostic logging library
- [xUnit](https://xunit.net/): A testing framework
- [FluentAssertions](https://fluentassertions.com/): A fluent assertion library
- [ArchUnitNET](https://github.com/TNG/ArchUnitNET): An architecture testing library

## External Services

- [MongoDB](https://www.mongodb.com/): A NoSQL database - Used in the Outbox Pattern
- [MSSQL](https://www.microsoft.com/en-us/sql-server): A relational database- Used for the modules main databases
    - During the development of this project, I used the local MSSQL instance (localdb) for database operations.
- [Papercut](https://www.papercut-smtp.com/): A Simple Desktop Email Server - Used for testing
- [Redis](https://redis.io/): An in-memory data structure store - Used for the Materialized View Pattern

## Modules

### Books

To create the `Initial` migration go to the `Vini.ModularMonolith.Example.Web` directory and this command:

```bash
dotnet ef migrations add Initial -c BooksDbContext -p ..\Vini.ModularMonolith.Example.Books\Vini.ModularMonolith.Example.Books.csproj -s .\Vini.ModularMonolith.Example.Web.csproj -o Data/Migrations
```

To run the migrations

```bash
dotnet ef database update --context BooksDbContext
```

### Books.Tests

To run migrations for the `appsettings.Testing.json` file, run this command:

```bash
dotnet ef database update -c BooksDbContext -p Vini.ModularMonolith.Example.Web/Vini.ModularMonolith.Example.Web.csproj -- --environment Testing
```

### Users

To create the `Initial` migration go to the `Vini.ModularMonolith.Example.Web` directory and this command:

```bash
dotnet ef migrations add Initial -c UsersDbContext -p ..\Vini.ModularMonolith.Example.Users\Vini.ModularMonolith.Example.Users.csproj -s .\Vini.ModularMonolith.Example.Web.csproj -o Data/Migrations
```

To run the migrations

```bash
dotnet ef database update --context UsersDbContext
```

### OrderProcessing

To create the `Initial` migration go to the `Vini.ModularMonolith.Example.Web` directory and this command:

```bash
dotnet ef migrations add Initial -c OrderProcessingDbContext -p ..\Vini.ModularMonolith.Example.OrderProcessing\Vini.ModularMonolith.Example.OrderProcessing.csproj -s .\Vini.ModularMonolith.Example.Web.csproj -o Data/Migrations
```

To run the migrations

```bash
dotnet ef database update --context OrderProcessingDbContext
```

### OrderProcessing.Tests

Has some architecture tests.

### EmailSending

To run papercut as a container, run this command:

```bash
docker run --name modular-monolith-papercut -d -p 25:25 -p 37408:37408 jijiechen/papercut:latest
```

To run MongoDB as a container, run this command:

```bash
docker run --name modular-monolith-mongo -d -p 27017:27017 mongo
```

### Reporting

This module has an event ingestion system that saves the necessary report data in a dedicated database.
