### Setup
project is tested and run against `https://hub.docker.com/_/microsoft-mssql-server` SQL Server &  
utilizes the `https://github.com/helldivers-2/api` community API to interface with AHGS servers.

`HellDiver2_EntityFramework_DB` can be run standalone or compiled into a docker container.

Prior to properly running the project you will have to run the project and update the `Config.json` with:     
SQL_IP -> location of database (`localhost` is valid if running locally)   
SQL_DB -> Name of the database within the engine    
SQL_ID -> the login username you want the application to use   
SQL_PW -> Plaintext password of the provided ID (NEEDS TO BE MOVED TO ENV)
API_Contact -> Discord Username or other means to reach out to you (required by the api)
FirstRun -> Set to false if manually updating database with migrations
[!WARNING]
Sleep timers can be adjusted but API restrictions will be enforced as needed

### Is not required but recommended
Use `dotnet ef database update` to push the EF migrations to your database   
(This generates all tables and information required for the database to work)   
And manually set FirstRun to false in the config.json

### Release
A release build is avaliable at:   
https://hub.docker.com/repository/docker/titanic880/hd2efdatabase/general   
Please be aware that while this is a tested build, it can and probably will still contain some bugs.