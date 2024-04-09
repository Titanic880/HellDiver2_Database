###Setup
project is tested and run against `https://hub.docker.com/_/microsoft-mssql-server` SQL Server and
utilizes the `https://github.com/helldivers-2/api` community API to interface with AHGS servers.

`HellDiver2_EntityFramework_DB` can be run standalone or compiled into a docker container.

Prior to properly running the project you will have to run the project and update the `Config.json` with:
Database information,
Discord Username (used in header as a blame)


`dotnet ef database update` to push the EF migrations to your selected database 
(This generates all tables and information required for the database to work)
> [!WARNING]
> This Currently needs you to manually enter information into `EntFramework/DB_Context.cs -> OnConfiguring()` to properly build the database.

