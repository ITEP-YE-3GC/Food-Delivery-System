## Database Setup Instructions

### SQL Server

1. **Install SQL Server:** Download and install SQL Server from the official Microsoft website. Choose the edition that best suits your needs (Express edition is free and suitable for development).

2. **Connection String:** Add the connection string to your `appsettings.json` file:

    ```json
    "ConnectionStrings": {
        "SqlServerConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
    }
    ```

3. **Register in `Program.cs`:** Use the following code to register SQL Server in your `Program.cs` file:

    ```csharp
    builder.Services.AddDbContext<YourDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
    ```

4. **Add Migration:** Navigate to your project directory and use the Entity Framework Core tools to add a migration:

    ```
    dotnet ef migrations add InitialCreate
    ```

5. **Update Database:** Apply the migration to update the database schema:

    ```
    dotnet ef database update
    ```

### PostgreSQL

1. **Install PostgreSQL:** Download and install PostgreSQL from the official PostgreSQL website.

2. **Connection String:** Add the connection string to your `appsettings.json` file:

    ```json
    "ConnectionStrings": {
        "PostgreSqlConnection": "Host=your_host;Database=your_db;Username=your_user;Password=your_password;"
    }
    ```

3. **Register in `Program.cs`:** Register PostgreSQL in your `Program.cs` file:

    ```csharp
    builder.Services.AddDbContext<YourDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
    ```

4. **Add Migration:** Use the same command as for SQL Server to add a migration for PostgreSQL.

5. **Update Database:** Apply the migration to update the PostgreSQL database schema with the same `dotnet ef database update` command.
