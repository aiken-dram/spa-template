# Backend webapi application

dotnet webapi backend based on [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

Nuget package of [spa-backend-shared](https://github.com/it-opfr-krd/spa-backend-shared) library should be available in local nuget source. Check repository for additional information.

Run `dotnet restore` after downloading repository

## Development build

For running development build:

```
 cd WebApi
 dotnet watch run
```

## Publishing

In `backend/webapi` run `dotnet publish --configuration release`

## Configuration

Application configuration is located in `webapi/appsettings.json` file:

```json
"Logging": {
    "SaveToFile": false
},
"AuthSettings": {
    "Lock": "10",
    "Timeout": "3"
},
```

Logging.SaveToFile - save application logs to `/Logs/` for debug and database optimization
AuthSettings.Lock - number of wrong password errors before user is locked permanently
AuthSettings.Timeout - number of wrong password errors before user is temporarily locked

## Tasks

`update shared` - updates spa-backend-shared package in all projects by running `.scripts/update-shared.cmd` script

## Scaffolding database

Initialize dotnet ef for core 3.1 sdk:

```sh
 dotnet tool install --global dotnet-ef
```

Or upgrade dotnet ef from old version:

```sh
dotnet tool update --global dotnet-ef
```

Scaffolding:

In `scaffold` directory run:

```sh
 dotnet ef dbcontext scaffold "server=localhost:50000;uid=db2admin;pwd=db2admin;database=SPA" IBM.EntityFrameworkCore -o Context --schema DB2ACCOUNT --table USERS -f
```

Note: scaffolding will not work for DB2 version 9.7 or lower

After scaffolding you can use [tools-ConsoleEFConfiguration](https://github.com/it-opfr-krd/tools-ConsoleEFConfiguration) to split entity configuration into separate files

## Workload

Enabling configuration `Logging.SafeToFile` will create logs in `Logs\` folder of application

Running [tools-ConsoleFormatWorkload](https://github.com/it-opfr-krd/tools-ConsoleFormatWorkload) in `Logs\` directory will process logs into a plain text file with database SQL queries, which you can process in IBM DB2 `Design advisor` to automatically create necessary indexes.
