# ModularMonolith Books
    
This is a learning project aimed at understanding the principles and implementation of **Modular Monoliths** in .NET 9. 

## Technologies

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview)
- [FastEndpoints](https://fast-endpoints.com/): An alternative to minimal APIs
- [Scalar](https://scalar.com/): An alternative to Swagger
- [xUnit](https://xunit.net/): A testing framework for .NET

## Modules

### Books

To create the `Initial` migration go to the `Vini.ModularMonolith.Example.Web` directory and this command:

```bash
dotnet ef migrations add Initial -c BookDbContext -p ..\Vini.ModularMonolith.Example.Books\Vini.ModularMonolith.Example.Books.csproj -s .\Vini.ModularMonolith.Example.Web.csproj -o Data/Migrations
```

To run the migrations

```bash
dotnet ef database update --context BookDbContext
```

### Books.Tests

To run migrations for the `appsettings.Testing.json` file, run this command:

```bash
dotnet ef database update -c BookDbContext -p Vini.ModularMonolith.Example.Web/Vini.ModularMonolith.Example.Web.csproj -- --environment Testing
```

### Users

To create the `Initial` migration go to the `Vini.ModularMonolith.Example.Web` directory and this command:

```bash
dotnet ef migrations add Initial -c UsersDbContext -p ..\Vini.ModularMonolith.Example.Users\Vini.ModularMonolith.Example.Users.csproj -s .\Vini.ModularMonolith.Example.Web.csproj -o Data/Migrations
```

To run the migrations

```bash
dotnet ef database update --context UserDbContext
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
