### Setup
project is tested and run against `https://hub.docker.com/_/microsoft-mssql-server` SQL Server &  
utilizes the `https://github.com/helldivers-2/api` community API to interface with AHGS servers.

`HellDiver2_EntityFramework_DB` can be run standalone or compiled into a docker container.

Prior to properly running the project you will have to run the project and update the `Config.json` with:  
Database information,  
Discord Username (used in header as a blame)  

Then use `dotnet ef database update` to push the EF migrations to your database   
(This generates all tables and information required for the database to work)

### Release
A release build is avaliable at:   
https://hub.docker.com/repository/docker/titanic880/hd2efdatabase/general   
Please be aware that while this is a tested build, it can and probably will still contain some bugs.