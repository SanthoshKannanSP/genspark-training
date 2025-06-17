# Day 17 - May 27, 2025

- ORM
- EntityFrameworkCore
- Migrations
- Npgsql

## Object Relational Mapping

[Reference](https://stackoverflow.com/questions/1279613/what-is-an-orm-how-does-it-work-and-how-should-i-use-one)

- Query and manipulate data in a database usign object-oriented paradigm
- Interact with the database using code and not SQL

| Database entity | OOP entity            |
| --------------- | --------------------- |
| Tables          | Classes               |
| Rows            | Objects               |
| Columns         | Attributes of objects |

- **Advantages:** Faster development, Database abstraction(easier to switch database)
- **Disadvantags:** Less efficient for complex queries

## EntityFrameworkCore

[Reference](https://learn.microsoft.com/en-us/ef/core/)

- ORM library developed by Microsoft

```bash
dotnet add package Microsoft.EntityFrameworkCore
```

- Create model for each table

```cs
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
```

- Create DbContext for the database

```cs
public class TwitterContext : DbContext
{
    public TwitterContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> UsersDb { get; set; }
}
```

- Add DbContext to injections

```cs
builder.Services.AddDbContext<TwitterContext>(opts => { });
```

## Migrations

[Reference](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

- **Code-first approach:** database schema is created/updated from C# code (entity classes and DbContext)
- **Version control for schema**
- Installing EF Core CLI tools

```bash
dotnet tool install --global dotnet-ef
```

- To use the tools on a specific project, you need `Microsoft.EntityFrameworkCore.Design` package

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

- Create migrations (based on differences between current model and snapshot)

```bash
dotnet ef migrations add <migrationName>
```

- Update database to the lastest migration

```bash
dotnet ef database update
```

## Npgsql

[Reference](https://www.npgsql.org/efcore/?tabs=onconfiguring)

- Provider for connecting PostgreSQL server to EF Core
- Installing Npgsql

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

- Connecting DbContext to PostgreSQL

```cs
builder.Services.AddDbContext<TwitterContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```

- Add connection string to `appsettings.json`

```json
"ConnectionStrings": {
    "DefaultConnection": "<connection_string>"
}
```
