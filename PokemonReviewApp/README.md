# CLI
Install dotnet-ef to handle migrations
$ dotnet tool install --global dotnet-ef

Init migrations (Create migrations to reference the models)
$ dotnet ef migrations add InitialCreate

Update database (Populate the database in this case)
$ dotnet ef database update

Install packages
$ dotnet add package <name>

Uninstall packages
$ dotnet remove package <name>

Run seed and execute api
$ dotnet run seeddata